using Nt.Syntax.Actions;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using State = Nt.Automaton.States.State<string>;
using StateAutomaton = Nt.Automaton.Automatons.StateAutomaton<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;

namespace Nt.Syntax.Automaton
{

    internal class ParserAutomaton(Grammar grammar) : BaseAutomaton(grammar)
    {
        private Action? EndAction { get; set; }

        // Generation
        protected override void Build()
        {
            Context.Reset();

            var errorAction = new ErrorAction();
            var initial = new State(); initial.SetDefault(initial, errorAction);

            Automaton = new StateAutomaton(initial); 
            EndAction = () =>
            {
                if (Automaton.CurrentState != initial) throw new EndOfStringException();
            };


            // Old style
            GenerateTerminalsStatesOldStyle(initial, errorAction);
            GenerateNonTerminalStatesOldStyle(initial, errorAction);
            GenerateAxiomStates(initial, errorAction);
            GenerateNewRuleStatesOldStyle(initial, errorAction);
            GenerateRegExStatesOldStyle(initial, errorAction);

            // New style
            GenerateTerminalsStatesNewStyle(initial, errorAction);
            GenerateNonTerminalsStatesNewStyle(initial, errorAction);
            GenerateNewRuleStatesNewStyle(initial, errorAction);
            GenerateRegExStatesNewStyle(initial, errorAction);
        }

        // Old style generation
        private void GenerateTerminalsStatesOldStyle(State initial, ErrorAction errorAction)
        {
            // Old style:
            // T = { a, b, c }
            State terminalState = new State().SetDefault(initial, errorAction);
            State affectationState = new State().SetDefault(initial, errorAction);
            State newState = new();

            initial.AddTransition(new Transition("T", terminalState));
            terminalState.AddTransition(new Transition("=", affectationState));
            affectationState.AddTransition(new Transition("{", newState));
            newState.SetDefault(newState, new AppendToCurrentTerminalAction(Context));
            newState.AddTransition(new Transition(",", newState, new AddTerminalAction(Grammar, Context)));
            newState.AddTransition(new Transition("}", initial, new AddTerminalAction(Grammar, Context)));
        }

        private void GenerateTerminalsStatesNewStyle(State initial, ErrorAction errorAction)
        {
            // New style:
            // Terminals: a, b, c;
            State terminalState = new State().SetDefault(initial, errorAction);
            State newState = new();

            initial.AddTransition(new Transition("TERMINALS", terminalState));
            initial.AddTransition(new Transition("terminals", terminalState));
            initial.AddTransition(new Transition("Terminals", terminalState));
            terminalState.AddTransition(new Transition(":", newState));
            newState.SetDefault(newState, new AppendToCurrentTerminalAction(Context));
            newState.AddTransition(new Transition(",", newState, new AddTerminalAction(Grammar, Context)));
            newState.AddTransition(new Transition(";", initial, new AddTerminalAction(Grammar, Context)));
        }

        private void GenerateNonTerminalStatesOldStyle(State initial, ErrorAction errorAction)
        {
            State nonTerminalState = new State().SetDefault(initial, errorAction);
            State affectationState = new State().SetDefault(initial, errorAction);
            State newState = new();

            initial.AddTransition(new Transition("N", nonTerminalState));
            nonTerminalState.AddTransition(new Transition("=", affectationState));
            affectationState.AddTransition(new Transition("{", newState));
            newState.SetDefault(newState, new AppendToCurrentNonTerminalAction(Context));
            newState.AddTransition(new Transition(",", newState, new AddNonTerminalAction(Grammar, Context)));
            newState.AddTransition(new Transition("}", initial, new AddNonTerminalAction(Grammar, Context)));
        }

        private void GenerateNonTerminalsStatesNewStyle(State initial, ErrorAction errorAction)
        {
            // New style:
            // Terminals: a, b, c;
            State nonTerminalsState = new State().SetDefault(initial, errorAction);
            State nonTerminalsState2 = new State().SetDefault(initial, errorAction);
            State newState = new();

            initial.AddTransition(new Transition("NON", nonTerminalsState));
            initial.AddTransition(new Transition("non", nonTerminalsState));
            initial.AddTransition(new Transition("Non", nonTerminalsState));
            nonTerminalsState.AddTransition(new Transition("TERMINALS", nonTerminalsState2));
            nonTerminalsState.AddTransition(new Transition("terminals", nonTerminalsState2));
            nonTerminalsState.AddTransition(new Transition("Terminals", nonTerminalsState2));
            nonTerminalsState2.AddTransition(new Transition(":", newState));
            newState.SetDefault(newState, new AppendToCurrentNonTerminalAction(Context));
            newState.AddTransition(new Transition(",", newState, new AddNonTerminalAction(Grammar, Context)));
            newState.AddTransition(new Transition(";", initial, new AddNonTerminalAction(Grammar, Context)));
        }

        private void GenerateAxiomStates(State initial, ErrorAction errorAction)
        {
            State axiomState = new State().SetDefault(initial, errorAction);
            State affectationState = new State().SetDefault(initial, new SetAxiomAction(Grammar));

            initial.AddTransition(new Transition("S", axiomState));
            axiomState.AddTransition(new Transition("=", affectationState));

        }

        // New style generation
        private void GenerateNewRuleStatesOldStyle(State initial, ErrorAction errorAction)
        {
            State newRuleState = new State().SetDefault(initial, errorAction);
            State arrowState = new State().SetDefault(initial, errorAction);
            State symbolState = new State().SetDefault(arrowState, new AddNewRuleAction(Grammar, Context));
            State derivationState = new();

            initial.AddTransition(new Transition("R", newRuleState));
            newRuleState.AddTransition(new Transition(":", symbolState));
            arrowState.AddTransition(new Transition("-", arrowState));
            arrowState.AddTransition(new Transition(">", derivationState));
            derivationState.SetDefault(derivationState, new AddRuleDerivationAction(Grammar, Context));
            derivationState.AddTransition(new Transition(";", initial));
            derivationState.AddTransition(new Transition("|", derivationState, new AddSameRuleAction(Grammar, Context)));
        }

        private void GenerateNewRuleStatesNewStyle(State initial, ErrorAction errorAction)
        {
            // New style:
            // Rules: A -> a B | b, B -> c;
            State newRuleState = new State().SetDefault(initial, errorAction);
            State arrowState = new State().SetDefault(initial, errorAction);
            State symbolState = new State().SetDefault(arrowState, new AddNewRuleAction(Grammar, Context));
            State derivationState = new();

            initial.AddTransition(new Transition("RULES", newRuleState));
            initial.AddTransition(new Transition("Rules", newRuleState));
            initial.AddTransition(new Transition("rules", newRuleState));
            newRuleState.AddTransition(new Transition(":", symbolState));
            arrowState.AddTransition(new Transition("-", arrowState));
            arrowState.AddTransition(new Transition(">", derivationState));
            derivationState.SetDefault(derivationState, new AddRuleDerivationAction(Grammar, Context));
            derivationState.AddTransition(new Transition(",", symbolState));
            derivationState.AddTransition(new Transition(";", initial));
            derivationState.AddTransition(new Transition("|", derivationState, new AddSameRuleAction(Grammar, Context)));
        }

        private void GenerateRegExStatesOldStyle(State initial, ErrorAction errorAction)
        {
            State newRegExState = new State().SetDefault(initial, errorAction);
            State equalState = new State().SetDefault(initial, errorAction);
            State symbolState = new State().SetDefault(equalState, new AddNewRegExAction(Grammar, Context));
            var readState = new State(); readState.SetDefault(readState, new AddRegExSymbolsAction(Grammar, Context));

            initial.AddTransition(new Transition("E", newRegExState));
            newRegExState.AddTransition(new Transition(":", symbolState));
            equalState.AddTransition(new Transition("=", readState));
            readState.AddTransition(new Transition(";", initial));
        }

        private void GenerateRegExStatesNewStyle(State initial, ErrorAction errorAction)
        {
            State newRegExState = new State().SetDefault(initial, errorAction);
            State newRegExState2 = new State().SetDefault(initial, errorAction);
            State equalState = new State().SetDefault(initial, errorAction);
            State symbolState = new State().SetDefault(equalState, new AddNewRegExAction(Grammar, Context));
            var readState = new State(); readState.SetDefault(readState, new AddRegExSymbolsAction(Grammar, Context));

            initial.AddTransition(new Transition("REGULAR", newRegExState));
            initial.AddTransition(new Transition("Regular", newRegExState));
            initial.AddTransition(new Transition("regular", newRegExState));
            newRegExState.AddTransition(new Transition("EXPRESSIONS", newRegExState2));
            newRegExState.AddTransition(new Transition("Expressions", newRegExState2));
            newRegExState.AddTransition(new Transition("expressions", newRegExState2));
            newRegExState2.AddTransition(new Transition(":", symbolState));
            equalState.AddTransition(new Transition("=", readState));
            readState.AddTransition(new Transition(",", symbolState));
            readState.AddTransition(new Transition(";", initial));
        }

        // Internal methods
        internal void Stop()
        {
            EndAction?.Invoke();
        }


    }
}
