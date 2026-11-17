using Nt.Automaton.Transitions.Strings;
using Nt.Syntax.Actions;
using Nt.Syntax.Exceptions;
using System.Runtime.InteropServices;
using State = Nt.Automaton.States.State<string>;
using StateAutomaton = Nt.Automaton.Automatons.StateAutomaton<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;

namespace Nt.Syntax.Automaton
{

    internal class ParserAutomaton : BaseAutomaton
    {
        private Action? EndAction { get; set; }

        public static string TerminalPattern { get; } = $"[^|;()]*";
        public static string NonTerminalPattern { get; } = $"^<?[A-Za-z0-9_]+>?$";
        public static string TokenPattern { get; } = $"[^|;()]*";

        // Generation
        protected override void Build()
        {
            GenerateTerminals();
            GenerateNonTerminals();
            GenerateAxiom();
            GenerateRules();
            GenerateRegEx();

            EndAction = () =>
            {
                if (Automaton?.CurrentState != InitialState) throw new EndOfStringException();
            };
        }

        private void GenerateTerminals()
        {
            var block = CreateBlock("TERMINALS");
            
            block.OpenState
                .SetDefault(
                    new StringTransition(block.CloseState)
                    .SetAction(new ExpectedAction("a terminal"))
                )
                .AddTransition(
                    new StringTransition(block.CloseState)
                    .AddPattern(TerminalPattern)
                    .SetAction(new AddTerminalAction(Grammar, Context))
                );
            block.CloseState
                .AddTransition(
                    new StringTransition(block.CloseState)
                    .AddPattern(TerminalPattern)
                    .SetAction(new AddTerminalAction(Grammar, Context))
                );      
        }

        private void GenerateNonTerminals()
        {
            var block = CreateBlock("NON TERMINALS");
            block.OpenState
                .SetDefault(
                    new StringTransition(block.CloseState)
                    .SetAction(new ExpectedAction("a non terminal"))
                )
                .AddTransition(
                    new StringTransition(block.CloseState)
                    .AddPattern(NonTerminalPattern)
                    .SetAction(new AddNonTerminalAction(Grammar, Context))
                );
            block.CloseState
                .AddTransition(
                    new StringTransition(block.CloseState)
                    .AddPattern(NonTerminalPattern)
                    .SetAction(new AddNonTerminalAction(Grammar, Context))
                );
        }

        private void GenerateAxiom()
        {
            var block = CreateBlock("AXIOM");
            block.OpenState
                .SetDefault(
                    new StringTransition(block.CloseState)
                    .SetAction(new ExpectedAction("a non terminal"))
                )
                .SetDefault(
                    new StringTransition(block.CloseState)
                    .AddPattern(NonTerminalPattern)
                    .SetAction(new SetAxiomAction(Grammar))
                );
        }

        private void GenerateRules()
        {
            var block = CreateBlock("RULES");
            var arrowState = new State();
            var derivationState = new State();

            block.OpenState
                .SetDefault(
                    new StringTransition(arrowState)
                    .SetAction(new ExpectedAction("a non terminal"))
                )
                .AddTransition(
                    new StringTransition(arrowState)
                    .AddPattern(NonTerminalPattern)
                    .SetAction(new CreateRuleAction(Grammar, Context))
                );
            arrowState
                .SetDefault(
                    new StringTransition(block.CloseState)
                    .SetAction(new ExpectedAction("an arrow like :=> or :->"))
                )
                .AddTransition(
                    new StringTransition(derivationState)
                    .AddPattern(":*(-+|=+)>+")
                );
            derivationState
                .SetDefault(
                    new StringTransition(block.CloseState)
                    .SetAction(new ExpectedAction("a terminal, non terminal, | or ;"))
                )
                .AddTransition(
                    new StringTransition(derivationState)
                    .AddPattern(TokenPattern)
                    .SetAction(new AddRuleDerivationAction(Grammar, Context))
                )
                .AddTransition(
                    new StringTransition(derivationState)
                    .Include("|")
                    .SetAction(new CreateSameRuleAction(Grammar, Context))
                )
                .AddTransition(
                    new StringTransition(block.CloseState)
                    .Include(";")
                );
            block.CloseState
                .AddTransition(
                    new StringTransition(arrowState)
                    .AddPattern(NonTerminalPattern)
                    .SetAction(new CreateRuleAction(Grammar, Context))
                );
        }

        private void GenerateRegEx()
        {
            var block = CreateBlock("REGULAR EXPRESSIONS");
            var equalState = new State();
            var readState = new State();

            block.OpenState
                .SetDefault(
                    new Transition(block.CloseState)
                    .SetAction(new ExpectedAction("a non terminal"))
                )
                .AddTransition(
                    new StringTransition(equalState)
                    .AddPattern(NonTerminalPattern)
                    .SetAction(new CreateRegexAction(Grammar, Context))
                );
            equalState
                .SetDefault(
                    new Transition(block.CloseState)
                    .SetAction(new ExpectedAction("an assignment operator like := or ="))
                )
                .AddTransition(
                    new StringTransition(readState)
                    .AddPattern(":*=+")
                );
            readState
                .SetDefault(
                    new Transition(block.CloseState)
                    .SetAction(new ExpectedAction("a regex pattern"))
                )
                .AddTransition(
                    new StringTransition(block.CloseState)
                    .AddPattern(TokenPattern)
                    .SetAction(new SetRegexPatternAction(Grammar, Context))
                );
            block.CloseState
                .AddTransition(
                    new StringTransition(equalState)
                    .AddPattern(NonTerminalPattern)
                    .SetAction(new CreateRegexAction(Grammar, Context))
                );
        }


        // Internal methods
        internal void Stop()
        {
            EndAction?.Invoke();
        }


    }
}
