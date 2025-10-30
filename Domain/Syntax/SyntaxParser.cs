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

            Parser parser = new([' ', '\0', '\n', '\t'], ["import", "IMPORT", "addtopath", "ADDTOPATH", ";"]);
            ParserResult parsed = parser.Parse(content);

            GeneratePreAutomaton(parsed.Tokens);
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

            Parser parser = new([' ', '\0', '\n', '\t'], [":", ",", "=", "{", "}", ";", "-", ">", "+", "*"]);
            ParserResult parsed = parser.Parse(content);

            GenerateAutomaton(parsed.Tokens);
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

        private void GeneratePreAutomaton(TokensList tokens)
        {
            var initial = new State(); initial.SetDefault(initial);

            PreAutomaton = new Automaton(tokens, initial);

            State addToPathState = new State().SetDefault(initial, new AddImportPathAction(tokens, AutomatonContext.ImportPath));
            State importState = new State().SetDefault(initial, new ImportFileAction(tokens, AutomatonContext.ImportPath));

            initial.AddTransition("import", importState);
            initial.AddTransition("IMPORT", importState);
            initial.AddTransition("addtopath", addToPathState);
            initial.AddTransition("ADDTOPATH", addToPathState);
        }

        /// <summary>
        /// Generates an automaton that can read a grammar file
        /// </summary>
        /// <param name="tokens">List of tokens that can be read by the automaton</param>
        private void GenerateAutomaton(TokensList tokens)
        {
            var errorAction = new ErrorAction(tokens);

            var initial = new State(); initial.SetDefault(initial, errorAction);
            State error = new State(errorAction).SetDefault(initial);

            Automaton = new Automaton(tokens, initial);
            AutomatonEndAction = () =>
            {
                if (Automaton.CurrentState != initial) throw new Exceptions.EndOfStringException();
            };

            GenerateTerminalsStates(tokens, initial, error);
            GenerateNonTerminalStates(tokens, initial, error);
            GenerateAxiomStates(tokens, initial, error);
            GenerateNewRuleStates(tokens, initial, error);
            GenerateRegExStates(tokens, initial, error);
        }

        private void GenerateTerminalsStates(TokensList tokens, State initial, State error)
        {
            State terminalState = new State().SetDefault(error);
            State affectationState = new State().SetDefault(error);
            State endState = new State().SetDefault(error);
            State newState = new State().SetDefault(endState, new AddTerminalAction(Grammar, tokens));

            initial.AddTransition("T", terminalState);
            terminalState.AddTransition("=", affectationState);
            affectationState.AddTransition("{", newState);
            endState.AddTransition("}", initial);
            endState.AddTransition(",", newState);
        }

        private void GenerateNonTerminalStates(TokensList tokens, State initial, State error)
        {
            State nonTerminalState = new State().SetDefault(error);
            State affectationState = new State().SetDefault(error);
            State endState = new State().SetDefault(error);
            State newState = new State().SetDefault(endState, new AddNonTerminalAction(Grammar, tokens));

            initial.AddTransition("N", nonTerminalState);
            nonTerminalState.AddTransition("=", affectationState);
            affectationState.AddTransition("{", newState);
            endState.AddTransition("}", initial);
            endState.AddTransition(",", newState);
        }

        private void GenerateAxiomStates(TokensList tokens, State initial, State error)
        {
            State axiomState = new State().SetDefault(error);
            State affectationState = new State().SetDefault(initial, new SetAxiomAction(Grammar, tokens));

            initial.AddTransition("S", axiomState);
            axiomState.AddTransition("=", affectationState);

        }

        private void GenerateNewRuleStates(TokensList tokens, State initial, State error)
        {
            State newRuleState = new State().SetDefault(error);
            State arrowState = new State().SetDefault(error);
            State symbolState = new State().SetDefault(arrowState, new AddNewRuleAction(Grammar, tokens));
            var derivationState = new State(); derivationState.SetDefault(derivationState, new AddRuleDerivationAction(Grammar, tokens));


            initial.AddTransition("R", newRuleState);
            newRuleState.AddTransition(":", symbolState);
            arrowState.AddTransition("-", arrowState);
            arrowState.AddTransition(">", derivationState);
            derivationState.AddTransition(";", initial);
            derivationState.AddTransition("|", derivationState, new AddSameRuleAction(Grammar));
        }

        private void GenerateRegExStates(TokensList tokens, State initial, State error)
        {
            State newRegExState = new State().SetDefault(error);
            State equalState = new State().SetDefault(error);
            State symbolState = new State().SetDefault(equalState, new AddNewRegExAction(Grammar, tokens));
            var readState = new State(); readState.SetDefault(readState, new AddRegExSymbolsAction(tokens));

            initial.AddTransition("E", newRegExState);
            newRegExState.AddTransition(":", symbolState);
            equalState.AddTransition("=", readState);
            readState.AddTransition(";", initial);
        }

    }
}
