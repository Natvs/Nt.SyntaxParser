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

        private Class.Grammar Grammar { get; set; }
        private Automaton? Automaton { get; set; }

        #endregion

        public Generator() { Grammar = new(); }

        public Class.Grammar Generate(string content)
        {
            Parser.Parser parser = new([' '], [",", "=", "{", "}"]);
            var parsed = parser.Parse(content);
            Console.WriteLine("Parsed text: " + parsed.Parsed.ToString());

            GenerateAutomaton(parsed.Tokens);
            try
            {
                foreach (var token in parsed.Parsed) Automaton?.Read(token);
            }
            catch (SyntaxError error)
            {
                Console.WriteLine(error.Message);
            }

            return Grammar;
        }


        private void GenerateAutomaton(TokensList tokens)
        {
            var initial = new State(); initial.SetDefault(initial);
            var error = new State(new ErrorAction(Grammar, tokens)).SetDefault(initial);
            Automaton = new Automaton(tokens, initial);

            GenerateTerminalsStates(tokens, initial, error);
            GenerateNonTerminalStates(tokens, initial, error);
            GenerateAxiomStates(tokens, initial, error);
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
    }
}
