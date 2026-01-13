using System.Text;
using Nt.Parsing;
using Nt.Parsing.Structures;
using Nt.Syntax.Actions;
using Nt.Syntax.Structures;

namespace Nt.Syntax
{

    public class SyntaxParser
    {

        #region Properties

        private Grammar Grammar { get; set; } = new();
        private Automaton? PreAutomaton { get; set; }
        private Automaton? Automaton { get; set; }
        private AutomatonContext AutomatonContext { get; } = new AutomatonContext();
        private System.Action? AutomatonEndAction { get; set; }
        private List<string> ParserSymbols { get; } = [":", ",", "=", "{", "}", ";", "-", ">", "+", "*"];

        #endregion

        private void Reset()
        {
            Grammar = new Grammar();
            PreAutomaton = null;
            Automaton = null;
            AutomatonContext.Reset();
            AutomatonEndAction = null;
        }

        /// <summary>
        /// Applies the pre-parser on a given grammar string
        /// </summary>
        /// <param name="content">String to pre-parse</param>
        /// <returns>A pre-parsed string of the grammar</returns>
        public string PreParseString(string content)
        {
            Reset();
            var sb = new StringBuilder();

            Parser parser = new([' ', '\0', '\n', '\t'], ["import", "IMPORT", "addtopath", "ADDTOPATH", "escape", "ESCAPE", ";"]);
            ParserResult parsed = parser.Parse(content);

            GeneratePreAutomaton(parsed.Symbols);
            foreach (ParsedToken token in parsed.Parsed)
            {
                PreAutomaton?.Read(token, AutomatonContext);
                if (AutomatonContext.ImportedString != null)
                {
                    sb.Append(AutomatonContext.ImportedString);
                    AutomatonContext.ImportedString = null;
                }
            }

            var contentReader = new StringReader(content);
            string? line;
            while ((line = contentReader.ReadLine()) != null)
            {
                if (line.StartsWith("import", StringComparison.CurrentCultureIgnoreCase)) continue;
                if (line.StartsWith("addtopath", StringComparison.CurrentCultureIgnoreCase)) continue;
                if (line.StartsWith("escape", StringComparison.CurrentCultureIgnoreCase)) continue;
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Reads a string and generates a grammar structure from it. Also applies pre-parsing on it.
        /// </summary>
        /// <param name="content">String to read</param>
        /// <returns>Grammar data structure from the given string</returns>
        public Grammar ParseString(string content)
        {
            content = PreParseString(content);

            Parser parser = new([' ', '\0', '\n', '\t'], ParserSymbols);
            ParserResult parsed = parser.Parse(content);

            GenerateAutomaton(parsed.Symbols);
            foreach (ParsedToken token in parsed.Parsed)
            {
                Automaton?.Read(token, AutomatonContext);
            }
            AutomatonEndAction?.Invoke();

            return Grammar;
        }

        /// <summary>
        /// Reads a file and generates a grammar structure from it. Also applies pre-parsing on it.
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>Grammar structure from content of the given file</returns>
        public Grammar ParseFile(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"Cannot parse {path}. The file cannot be found.");
            string content = File.ReadAllText(path);
            return ParseString(content);
        }

        private void GeneratePreAutomaton(SymbolsList symbols)
        {
            var initial = new State(); initial.SetDefault(initial);

            PreAutomaton = new Automaton(symbols, initial);

            State addToPathState = new State().SetDefault(initial, new AddImportPathAction(symbols, AutomatonContext.ImportPath));
            State importState = new State().SetDefault(initial, new ImportFileAction(symbols, AutomatonContext.ImportPath));
            State escapeState = new State().SetDefault(initial, new SetEscapeCharAction(Grammar, symbols));

            initial.AddTransition("import", importState);
            initial.AddTransition("IMPORT", importState);
            initial.AddTransition("addtopath", addToPathState);
            initial.AddTransition("ADDTOPATH", addToPathState);
            initial.AddTransition("ESCAPE", escapeState);
            initial.AddTransition("escape", escapeState);
        }

        /// <summary>
        /// Generates an automaton that can read a grammar file
        /// </summary>
        /// <param name="symbols">List of tokens that can be read by the automaton</param>
        private void GenerateAutomaton(SymbolsList symbols)
        {
            var errorAction = new ErrorAction(symbols);

            var initial = new State(); initial.SetDefault(initial, errorAction);
            State error = new State(errorAction).SetDefault(initial);

            Automaton = new Automaton(symbols, initial);
            AutomatonEndAction = () =>
            {
                if (Automaton.CurrentState != initial) throw new Exceptions.EndOfStringException();
            };

            GenerateTerminalsStates(symbols, initial, error);
            GenerateNonTerminalStates(symbols, initial, error);
            GenerateAxiomStates(symbols, initial, error);
            GenerateNewRuleStates(symbols, initial, error);
            GenerateRegExStates(symbols, initial, error);
        }

        private void GenerateTerminalsStates(SymbolsList symbols, State initial, State error)
        {
            State terminalState = new State().SetDefault(error);
            State affectationState = new State().SetDefault(error);
            State newState = new();

            initial.AddTransition("T", terminalState);
            terminalState.AddTransition("=", affectationState);
            affectationState.AddTransition("{", newState);
            newState.SetDefault(newState, new AppendToCurrentTerminalAction(symbols, AutomatonContext));
            newState.AddTransition(",", newState, new AddTerminalAction(Grammar, AutomatonContext));
            newState.AddTransition("}", initial, new AddTerminalAction(Grammar, AutomatonContext));
        }

        private void GenerateNonTerminalStates(SymbolsList symbols, State initial, State error)
        {
            State nonTerminalState = new State().SetDefault(error);
            State affectationState = new State().SetDefault(error);
            State newState = new();

            initial.AddTransition("N", nonTerminalState);
            nonTerminalState.AddTransition("=", affectationState);
            affectationState.AddTransition("{", newState);
            newState.SetDefault(newState, new AppendToCurrentNonTerminalAction(symbols, AutomatonContext));
            newState.AddTransition(",", newState, new AddNonTerminalAction(Grammar, AutomatonContext));
            newState.AddTransition("}", initial, new AddNonTerminalAction(Grammar, AutomatonContext));
        }

        private void GenerateAxiomStates(SymbolsList symbols, State initial, State error)
        {
            State axiomState = new State().SetDefault(error);
            State affectationState = new State().SetDefault(initial, new SetAxiomAction(Grammar, symbols));

            initial.AddTransition("S", axiomState);
            axiomState.AddTransition("=", affectationState);

        }

        private void GenerateNewRuleStates(SymbolsList symbols, State initial, State error)
        {
            State newRuleState = new State().SetDefault(error);
            State arrowState = new State().SetDefault(error);
            State symbolState = new State().SetDefault(arrowState, new AddNewRuleAction(Grammar, symbols));
            State derivationState = new();

            initial.AddTransition("R", newRuleState);
            newRuleState.AddTransition(":", symbolState);
            arrowState.AddTransition("-", arrowState);
            arrowState.AddTransition(">", derivationState);
            derivationState.SetDefault(derivationState, new AddRuleDerivationAction(Grammar, symbols));
            derivationState.AddTransition(";", initial);
            derivationState.AddTransition("|", derivationState, new AddSameRuleAction(Grammar));
        }

        private void GenerateRegExStates(SymbolsList symbols, State initial, State error)
        {
            State newRegExState = new State().SetDefault(error);
            State equalState = new State().SetDefault(error);
            State symbolState = new State().SetDefault(equalState, new AddNewRegExAction(Grammar, symbols));
            var readState = new State(); readState.SetDefault(readState, new AddRegExSymbolsAction(symbols));

            initial.AddTransition("E", newRegExState);
            newRegExState.AddTransition(":", symbolState);
            equalState.AddTransition("=", readState);
            readState.AddTransition(";", initial);
        }

    }
}
