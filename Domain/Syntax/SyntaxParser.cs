using System.Text;
using Nt.Automaton.States;
using Nt.Parser;
using Nt.Parser.Symbols;
using Nt.Syntax.Actions;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

using StateAutomaton = Nt.Automaton.StateAutomaton<string>;
using State = Nt.Automaton.States.State<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;
using Nt.Syntax.Automaton;


namespace Nt.Syntax
{

    public class SyntaxParserConfig
    {
        public ISymbolFactory SymbolFactory { get; private set; } = new SymbolFactory();

        public void SetSymbolFactory(ISymbolFactory factory)
        {
            SymbolFactory = factory;
        }

        private static SyntaxParserConfig? _instance = null;

        public static SyntaxParserConfig GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SyntaxParserConfig();
            }
            return _instance;
        }
    }

    public class SyntaxParser
    {

        #region Private

        private Grammar Grammar { get; set; } = new();
        private StateAutomaton? PreAutomaton { get; set; }
        private StateAutomaton? Automaton { get; set; }
        private AutomatonContext AutomatonContext { get; } = new AutomatonContext();
        private System.Action? AutomatonEndAction { get; set; }
        private List<string> ParserSymbols { get; } = [":", ",", "=", "{", "}", ";", "-", ">", "+", "*"];

        private string PreParseString(string content, SymbolsParser parser)
        {
            ParserResult parsed = parser.Parse(content);
            StringBuilder sb = new();

            foreach (var token in parsed.GetParsed())
            {
                PreAutomaton?.Read(new AutomatonToken(token));
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
            AutomatonContext.Reset();

            PreAutomaton = new StateAutomaton(initial);
            AutomatonEndAction = () =>
            {
                if (PreAutomaton.CurrentState != initial) throw new EndOfStringException();
            };

            State<string> addToPathState = new();
            State<string> importState = new();
            State<string> escapeState = new State().SetDefault(initial, new SetEscapeCharAction(Grammar));

            initial.AddTransition(new Transition("import", importState));
            initial.AddTransition(new Transition("IMPORT", importState));
            importState.SetDefault(importState, new AppendToCurrentImportFileAction(AutomatonContext));
            importState.AddTransition(new Transition(";", initial, new ImportFileAction(AutomatonContext)));

            initial.AddTransition(new Transition("addtopath", addToPathState));
            initial.AddTransition(new Transition("ADDTOPATH", addToPathState));
            addToPathState.SetDefault(addToPathState, new AppendToCurrentImportPathAction(AutomatonContext));
            addToPathState.AddTransition(new Transition(";", initial, new AddImportPathAction(AutomatonContext)));

            initial.AddTransition(new Transition("ESCAPE", escapeState));
            initial.AddTransition(new Transition("escape", escapeState));
        }

        /// <summary>
        /// Initializes an automaton that can read a grammar file
        /// </summary>
        /// <exception cref="EndOfStringException">The automaton might end on a state different from the initial state</exception>
        private void GenerateAutomaton()
        {
            AutomatonContext.Reset();

            var errorAction = new ErrorAction();

            var initial = new State(); initial.SetDefault(initial, errorAction);
            State error = new State(errorAction).SetDefault(initial);

            Automaton = new StateAutomaton(initial);
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

            initial.AddTransition(new Transition("T", terminalState));
            terminalState.AddTransition(new Transition("=", affectationState));
            affectationState.AddTransition(new Transition("{", newState));
            newState.SetDefault(newState, new AppendToCurrentTerminalAction(AutomatonContext));
            newState.AddTransition(new Transition(",", newState, new AddTerminalAction(Grammar, AutomatonContext)));
            newState.AddTransition(new Transition("}", initial, new AddTerminalAction(Grammar, AutomatonContext)));
        }

        private void GenerateNonTerminalStates(State initial, State error)
        {
            State nonTerminalState = new State().SetDefault(error);
            State affectationState = new State().SetDefault(error);
            State newState = new();

            initial.AddTransition(new Transition("N", nonTerminalState));
            nonTerminalState.AddTransition(new Transition("=", affectationState));
            affectationState.AddTransition(new Transition("{", newState));
            newState.SetDefault(newState, new AppendToCurrentNonTerminalAction(AutomatonContext));
            newState.AddTransition(new Transition(",", newState, new AddNonTerminalAction(Grammar, AutomatonContext)));
            newState.AddTransition(new Transition("}", initial, new AddNonTerminalAction(Grammar, AutomatonContext)));
        }

        private void GenerateAxiomStates(State initial, State error)
        {
            State axiomState = new State().SetDefault(error);
            State affectationState = new State().SetDefault(initial, new SetAxiomAction(Grammar));

            initial.AddTransition(new Transition("S", axiomState));
            axiomState.AddTransition(new Transition("=", affectationState));

        }

        private void GenerateNewRuleStates(State initial, State error)
        {
            State newRuleState = new State().SetDefault(error);
            State arrowState = new State().SetDefault(error);
            State symbolState = new State().SetDefault(arrowState, new AddNewRuleAction(Grammar, AutomatonContext));
            State derivationState = new();

            initial.AddTransition(new Transition("R", newRuleState));
            newRuleState.AddTransition(new Transition(":", symbolState));
            arrowState.AddTransition(new Transition("-", arrowState));
            arrowState.AddTransition(new Transition(">", derivationState));
            derivationState.SetDefault(derivationState, new AddRuleDerivationAction(Grammar, AutomatonContext));
            derivationState.AddTransition(new Transition(";", initial));
            derivationState.AddTransition(new Transition("|", derivationState, new AddSameRuleAction(Grammar, AutomatonContext)));
        }

        private void GenerateRegExStates(State initial, State error)
        {
            State newRegExState = new State().SetDefault(error);
            State equalState = new State().SetDefault(error);
            State symbolState = new State().SetDefault(equalState, new AddNewRegExAction(Grammar, AutomatonContext));
            var readState = new State(); readState.SetDefault(readState, new AddRegExSymbolsAction(Grammar, AutomatonContext));

            initial.AddTransition(new Transition("E", newRegExState));
            newRegExState.AddTransition(new Transition(":", symbolState));
            equalState.AddTransition(new Transition("=", readState));
            readState.AddTransition(new Transition(";", initial));
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
            try
            {
                GeneratePreAutomaton();

                var configuration = SyntaxParserConfig.GetInstance();
                Nt.Parser.SymbolsParser parser = new(configuration.SymbolFactory, [' ', '\0', '\n', '\t'], ["import", "IMPORT", "addtopath", "ADDTOPATH", "escape", "ESCAPE", ";"]);
                return PreParseString(content, parser);
            }
            catch
            {
                throw new Exception("An error occurred while trying to pre-parse the string.");
            }
        }

        /// <summary>
        /// Reads a string and generates a grammar structure from it. Also applies pre-parsing on it.
        /// </summary>
        /// <param name="content">String to read</param>
        /// <returns>Grammar data structure from the given string</returns>
        public Grammar ParseString(string content)
        {
            try
            {
                Grammar = new();
                content = PreParseString(content);
                GenerateAutomaton();

                var configuration = SyntaxParserConfig.GetInstance();
                SymbolsParser parser = new(configuration.SymbolFactory, [' ', '\0', '\n', '\t'], ParserSymbols);
                ParserResult parsed = parser.Parse(content);

                foreach (var token in parsed.GetParsed())
                {
                    Automaton?.Read(new AutomatonToken(token));
                }
                AutomatonEndAction?.Invoke();

                return Grammar;
            }
            catch
            {
                throw new Exception("An error occurred while trying to parse the string.");
            }
        }

        /// <summary>
        /// Reads a file and generates a grammar structure from it. Also applies pre-parsing on it.
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>Grammar structure from content of the given file</returns>
        public Grammar ParseFile(string path)
        {
            try { 
                if (!File.Exists(path)) throw new FileNotFoundException($"Cannot parse {path}. The file cannot be found.");
                string content = File.ReadAllText(path);
                return ParseString(content);
            }
            catch
            {
                throw new Exception($"An error occurred while trying to parse the file at {path}.");
            }
        }

        #endregion

    }
}
