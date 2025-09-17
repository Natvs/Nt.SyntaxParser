using GrammarReader.Code.Grammar.Actions;
using GrammarReader.Code.Grammar.Exceptions;
using GrammarReader.Code.Grammar.Structures;
using GrammarReader.Code.Parser;
using GrammarReader.Code.Parser.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Grammar
{

    public class GrammarParser
    {

        #region Properties

        private Structures.Grammar Grammar { get; set; } = new();
        private Automaton? PreAutomaton { get; set; }
        private Automaton? Automaton { get; set; }
        private AutomatonContext AutomatonContext { get; } = new AutomatonContext();
        private Rule? CurrentRule { get; set; } = null;
        private RegularExpression? CurrentRegex { get; set; } = null;

        #endregion

        public string PreParse(string content)
        {
            var sb = new StringBuilder();

            Parser.Parser parser = new([' ', '\0', '\n', '\t'], ["import", "IMPORT", ";"]);
            var parsed = parser.Parse(content);

            GeneratePreAutomaton(parsed.Tokens);
            foreach(var token in parsed.Parsed)
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
                if (line.StartsWith("import") || line.StartsWith("IMPORT")) continue;
                sb.AppendLine(line);
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// Reads a string and generates a grammar structure from it
        /// </summary>
        /// <param name="content">String to read</param>
        /// <returns>A grammar data structure from the given string</returns>
        public Structures.Grammar Generate(string content)
        {
            content = PreParse(content);
            Console.WriteLine("Pre parsed grammar string\n" + content);

            Parser.Parser parser = new([' ', '\0', '\n', '\t'], [":", ",", "=", "{", "}", ";", "-", ">", "+", "*"]);
            var parsed = parser.Parse(content);

            GenerateAutomaton(parsed.Tokens);
            foreach (var token in parsed.Parsed)
            {
                try 
                {
                    Automaton?.Read(token, AutomatonContext);
                    CurrentRule = AutomatonContext.Rule;
                    CurrentRegex = AutomatonContext.RegularExpression;
                }
                catch (Exception e) { Console.Error.WriteLine(e.Message); }
            }

            return Grammar;
        }

        private void GeneratePreAutomaton(TokensList tokens)
        {
            var initial = new State(); initial.SetDefault(initial);

            PreAutomaton = new Automaton(tokens, initial);

            var importState = new State().SetDefault(initial, new ImportFileAction(tokens));

            initial.AddTransition("import", importState);
            initial.AddTransition("IMPORT", importState);
        }

        /// <summary>
        /// Generates an automaton that can read a grammar file
        /// </summary>
        /// <param name="tokens">List of tokens that can be read by the automaton</param>
        private void GenerateAutomaton(TokensList tokens)
        {
            var errorAction = new ErrorAction(Grammar, tokens);

            var initial = new State(); initial.SetDefault(initial, errorAction);
            var error = new State(errorAction).SetDefault(initial);

            Automaton = new Automaton(tokens, initial);

            GenerateTerminalsStates(tokens, initial, error);
            GenerateNonTerminalStates(tokens, initial, error);
            GenerateAxiomStates(tokens, initial, error);
            GenerateNewRuleStates(tokens, initial, error);
            GenerateRegExStates(tokens, initial, error);
        }

        private void GenerateTerminalsStates(TokensList tokens, State initial, State error)
        {
            var terminalState = new State().SetDefault(error);
            var affectationState = new State().SetDefault(error);
            var endState = new State().SetDefault(error);
            var newState = new State().SetDefault(endState, new AddTerminalAction(Grammar, tokens));
            
            initial.AddTransition("T", terminalState);
            terminalState.AddTransition("=", affectationState);
            affectationState.AddTransition("{", newState);
            endState.AddTransition("}", initial);
            endState.AddTransition(",", newState);
        }

        private void GenerateNonTerminalStates(TokensList tokens, State initial, State error)
        {
            var nonTerminalState = new State().SetDefault(error);
            var affectationState = new State().SetDefault(error);
            var endState = new State().SetDefault(error);
            var newState = new State().SetDefault(endState, new AddNonTerminalAction(Grammar, tokens));

            initial.AddTransition("N", nonTerminalState);
            nonTerminalState.AddTransition("=", affectationState);
            affectationState.AddTransition ("{", newState);
            endState.AddTransition("}", initial);
            endState.AddTransition(",", newState);
        }

        private void GenerateAxiomStates(TokensList tokens, State initial, State error)
        {
            var axiomState = new State().SetDefault(error);
            var affectationState = new State().SetDefault(initial, new SetAxiomAction(Grammar, tokens));

            initial.AddTransition("S", axiomState);
            axiomState.AddTransition("=", affectationState);
            
        }

        private void GenerateNewRuleStates(TokensList tokens, State initial, State error)
        {
            var newRuleState = new State().SetDefault(error);     
            var arrowState = new State().SetDefault(error);
            var symbolState = new State().SetDefault(arrowState, new AddNewRuleAction(Grammar, tokens));
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
            var newRegExState = new State().SetDefault(error);
            var equalState = new State().SetDefault(error);
            var symbolState = new State().SetDefault(equalState, new AddNewRegExAction(Grammar, tokens));
            var readState = new State(); readState.SetDefault(readState, new AddRegExSymbolsAction(Grammar, tokens));

            initial.AddTransition("E", newRegExState);
            newRegExState.AddTransition(":", symbolState);
            equalState.AddTransition("=", readState);
            readState.AddTransition(";", initial);
        }

    }
}
