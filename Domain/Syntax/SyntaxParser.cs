using System.Text;
using Nt.Parser;
using Nt.Parser.Structures;
using Nt.Syntax.Actions;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

namespace Nt.Syntax
{

    public class SyntaxParser
    {

        #region Private

        private Grammar Grammar { get; set; } = new();
        private Automaton? PreAutomaton { get; set; }
        private Automaton? Automaton { get; set; }
        private AutomatonContext AutomatonContext { get; } = new AutomatonContext();
        private System.Action? AutomatonEndAction { get; set; }
        private List<string> ParserSymbols { get; } = [":", ",", "=", "{", "}", ";", "-", ">", "+", "*"];

        private string PreParseString(string content, SymbolsParser parser)
        {
            ParserResult parsed = parser.Parse(content);
            StringBuilder sb = new();

            foreach (var token in parsed.GetParsed())
            {
                PreAutomaton?.Read(token, AutomatonContext);
                if (AutomatonContext.ImportedString != null)
                {
                    sb.Append(AutomatonContext.ImportedString);
                    AutomatonContext.ImportedString = null;
                }
            }

            var imported = false;
            var contentReader = new StringReader(content);
            string? line;
            while ((line = contentReader.ReadLine()) != null)
            {
                if (line.StartsWith("import", StringComparison.CurrentCultureIgnoreCase)) { imported = true; continue; }
                if (line.StartsWith("addtopath", StringComparison.CurrentCultureIgnoreCase)) continue;
                if (line.StartsWith("escape", StringComparison.CurrentCultureIgnoreCase)) continue;
                sb.AppendLine(line);
            }

            var new_content = sb.ToString();
            if (imported)
            {
                return PreParseString(new_content, parser);
            }
            return new_content;
        }

        /// <summary>
        /// Initializes the pre-automaton structure used for parsing pre-parsing instructions.
        /// </summary>
        /// <exception cref="EndOfStringException">The pre-automaton might end on a state different from the initial state</exception>
        private void GeneratePreAutomaton()
        {
            var initial = new State(); initial.SetDefault(initial);

            PreAutomaton = new Automaton(initial);
            AutomatonEndAction = () =>
            {
                if (PreAutomaton.CurrentState != initial) throw new EndOfStringException();
            };

            State addToPathState = new();
            State importState = new();
            State escapeState = new State().SetDefault(initial, new SetEscapeCharAction(Grammar));

            initial.AddTransition("import", importState);
            initial.AddTransition("IMPORT", importState);
            importState.SetDefault(importState, new AppendToCurrentImportFileAction(AutomatonContext));
            importState.AddTransition(";", initial, new ImportFileAction(AutomatonContext));

            initial.AddTransition("addtopath", addToPathState);
            initial.AddTransition("ADDTOPATH", addToPathState);
            addToPathState.SetDefault(addToPathState, new AppendToCurrentImportPathAction(AutomatonContext));
            addToPathState.AddTransition(";", initial, new AddImportPathAction(AutomatonContext));

            initial.AddTransition("ESCAPE", escapeState);
            initial.AddTransition("escape", escapeState);
        }

        /// <summary>
        /// Initializes an automaton that can read a grammar file
        /// </summary>
        /// <exception cref="EndOfStringException">The automaton might end on a state different from the initial state</exception>
        private void GenerateAutomaton()
        {
            Grammar = new Grammar();
            AutomatonContext.Reset();

            var errorAction = new ErrorAction();

            var initial = new State(); initial.SetDefault(initial, errorAction);
            State error = new State(errorAction).SetDefault(initial);

            Automaton = new Automaton(initial);
            AutomatonEndAction = () =>
            {
                if (Automaton.CurrentState != initial) throw new EndOfStringException();
            };

            GenerateTerminalsStates(initial, error);
            GenerateNonTerminalStates(initial, error);
            GenerateAxiomStates(initial, error);
            GenerateNewRuleStates(initial, error);
            GenerateRegExStates(initial, error);
        }

        private void GenerateTerminalsStates(State initial, State error)
        {
            State terminalState = new State().SetDefault(error);
            State affectationState = new State().SetDefault(error);
            State newState = new();

            initial.AddTransition("T", terminalState);
            terminalState.AddTransition("=", affectationState);
            affectationState.AddTransition("{", newState);
            newState.SetDefault(newState, new AppendToCurrentTerminalAction(AutomatonContext));
            newState.AddTransition(",", newState, new AddTerminalAction(Grammar, AutomatonContext));
            newState.AddTransition("}", initial, new AddTerminalAction(Grammar, AutomatonContext));
        }

        private void GenerateNonTerminalStates(State initial, State error)
        {
            State nonTerminalState = new State().SetDefault(error);
            State affectationState = new State().SetDefault(error);
            State newState = new();

            initial.AddTransition("N", nonTerminalState);
            nonTerminalState.AddTransition("=", affectationState);
            affectationState.AddTransition("{", newState);
            newState.SetDefault(newState, new AppendToCurrentNonTerminalAction(AutomatonContext));
            newState.AddTransition(",", newState, new AddNonTerminalAction(Grammar, AutomatonContext));
            newState.AddTransition("}", initial, new AddNonTerminalAction(Grammar, AutomatonContext));
        }

        private void GenerateAxiomStates(State initial, State error)
        {
            State axiomState = new State().SetDefault(error);
            State affectationState = new State().SetDefault(initial, new SetAxiomAction(Grammar));

            initial.AddTransition("S", axiomState);
            axiomState.AddTransition("=", affectationState);

        }

        private void GenerateNewRuleStates(State initial, State error)
        {
            State newRuleState = new State().SetDefault(error);
            State arrowState = new State().SetDefault(error);
            State symbolState = new State().SetDefault(arrowState, new AddNewRuleAction(Grammar));
            State derivationState = new();

            initial.AddTransition("R", newRuleState);
            newRuleState.AddTransition(":", symbolState);
            arrowState.AddTransition("-", arrowState);
            arrowState.AddTransition(">", derivationState);
            derivationState.SetDefault(derivationState, new AddRuleDerivationAction(Grammar));
            derivationState.AddTransition(";", initial);
            derivationState.AddTransition("|", derivationState, new AddSameRuleAction(Grammar));
        }

        private void GenerateRegExStates(State initial, State error)
        {
            State newRegExState = new State().SetDefault(error);
            State equalState = new State().SetDefault(error);
            State symbolState = new State().SetDefault(equalState, new AddNewRegExAction(Grammar));
            var readState = new State(); readState.SetDefault(readState, new AddRegExSymbolsAction(Grammar));

            initial.AddTransition("E", newRegExState);
            newRegExState.AddTransition(":", symbolState);
            equalState.AddTransition("=", readState);
            readState.AddTransition(";", initial);
        }

        #endregion

        #region Public

        /// <summary>
        /// Applies the pre-parser on a given grammar string
        /// </summary>
        /// <param name="content">String to pre-parse</param>
        /// <returns>A pre-parsed string of the grammar</returns>
        public string PreParseString(string content)
        {
            GeneratePreAutomaton();
            SymbolsParser parser = new([' ', '\0', '\n', '\t'], ["import", "IMPORT", "addtopath", "ADDTOPATH", "escape", "ESCAPE", ";"]);
            return PreParseString(content, parser);
        }

        /// <summary>
        /// Reads a string and generates a grammar structure from it. Also applies pre-parsing on it.
        /// </summary>
        /// <param name="content">String to read</param>
        /// <returns>Grammar data structure from the given string</returns>
        public Grammar ParseString(string content)
        {
            content = PreParseString(content);

            SymbolsParser parser = new([' ', '\0', '\n', '\t'], ParserSymbols);
            ParserResult parsed = parser.Parse(content);

            GenerateAutomaton();
            foreach (ParsedToken token in parsed.GetParsed())
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

        #endregion

    }
}
