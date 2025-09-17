using GrammarReader.Domain.Grammar.Actions;
using GrammarReader.Domain.Parser;
using GrammarReader.Domain.Parser.Structures;
using System.Text;

namespace GrammarReader.Domain.Grammar
{

    public class GrammarParser
    {

        #region Properties

        private Structures.Grammar Grammar { get; set; } = new();
        private Automaton? PreAutomaton { get; set; }
        private Automaton? Automaton { get; set; }
        private AutomatonContext AutomatonContext { get; } = new AutomatonContext();

        #endregion

        /// <summary>
        /// Applies the pre-parser on a given grammar string
        /// </summary>
        /// <param name="content">String to pre-parse</param>
        /// <returns>A pre-parsed string of the grammar</returns>
        public string PreParse(string content)
        {
            var sb = new StringBuilder();

            Parser parser = new([' ', '\0', '\n', '\t'], ["import", "IMPORT", "addtopath", "ADDTOPATH", ";"]);
            ParserResult parsed = parser.Parse(content);

            GeneratePreAutomaton(parsed.Tokens);
            foreach (ParsedToken token in parsed.Parsed)
            {
                try
                {
                    PreAutomaton?.Read(token, AutomatonContext);
                    if (AutomatonContext.ImportedString != null)
                    {
                        sb.Append(AutomatonContext.ImportedString);
                        AutomatonContext.ImportedString = null;
                    }
                }
                catch (Exception e) { Console.Error.WriteLine(e.Message); }
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
        /// Reads a string and generates a grammar structure from it
        /// </summary>
        /// <param name="content">String to read</param>
        /// <returns>A grammar data structure from the given string</returns>
        public Structures.Grammar Parse(string content)
        {
            content = PreParse(content);
            Console.WriteLine("Pre parsed grammar string\n" + content);

            Parser parser = new([' ', '\0', '\n', '\t'], [":", ",", "=", "{", "}", ";", "-", ">", "+", "*"]);
            ParserResult parsed = parser.Parse(content);

            GenerateAutomaton(parsed.Tokens);
            foreach (ParsedToken token in parsed.Parsed)
            {
                try
                {
                    Automaton?.Read(token, AutomatonContext);
                }
                catch (Exception e) { Console.Error.WriteLine(e.Message); }
            }

            return Grammar;
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
