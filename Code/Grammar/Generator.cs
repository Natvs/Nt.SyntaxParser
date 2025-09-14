using GrammarReader.Code.Class;
using GrammarReader.Code.Grammar.Actions;
using GrammarReader.Code.Grammar.Exceptions;
using GrammarReader.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Grammar
{

    public class Generator
    {

        #region Properties

        private Class.Grammar Grammar { get; set; } = new();
        private Automaton? Automaton { get; set; }
        private Rule? CurrentRule { get; set; } = null;

        #endregion

        /// <summary>
        /// Reads a string and generates a grammar structure from it
        /// </summary>
        /// <param name="content">String to read</param>
        /// <returns>A grammar data structure from the given string</returns>
        public Class.Grammar Generate(string content)
        {
            Parser.Parser parser = new([' '], [",", "=", "==", "+", "++", "-", "--", "*", "/", "{", "}", ":", ";", "<", ">"]);
            var parsed = parser.Parse(content);
            Console.WriteLine("Parsed text: " + parsed.Parsed.ToString());

            GenerateAutomaton(parsed.Tokens);

            foreach (var token in parsed.Parsed)
            {
                Rule? newRule = null;
                try 
                {
                    Automaton?.Read(token, CurrentRule, out newRule);
                    if (newRule != null) CurrentRule = newRule;
                }
                catch (Exception e) { Console.Error.WriteLine(e.Message); }
            }

            return Grammar;
        }


        /// <summary>
        /// Generates an automaton that can read a grammar file
        /// </summary>
        /// <param name="tokens">List of tokens that can be read by the automaton</param>
        private void GenerateAutomaton(TokensList tokens)
        {
            var ErrorAction = new ErrorAction(Grammar, tokens);

            var initial = new State(); initial.SetDefault(initial, ErrorAction);
            var error = new State(ErrorAction).SetDefault(initial);

            Automaton = new Automaton(tokens, initial);

            GenerateTerminalsStates(tokens, initial, error);
            GenerateNonTerminalStates(tokens, initial, error);
            GenerateAxiomStates(tokens, initial, error);
            GenerateNewRuleStates(tokens, initial, error);
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
            var AddRuleAction = new AddNewRuleAction(Grammar, tokens);

            var newRuleState = new State().SetDefault(error);     
            var arrowState = new State().SetDefault(error);
            var symbolState = new State().SetDefault(arrowState, AddRuleAction);
            var derivationState = new State(); derivationState.SetDefault(derivationState, new AddRuleDerivationAction(Grammar, tokens));
            

            initial.AddTransition("R", newRuleState);
            newRuleState.AddTransition(":", symbolState);
            arrowState.AddTransition("-", arrowState);
            arrowState.AddTransition(">", derivationState);
            derivationState.AddTransition(";", initial);
            derivationState.AddTransition("|", derivationState, new AddSameRuleAction(Grammar));
        }

    }
}
