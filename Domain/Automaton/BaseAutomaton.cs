using Nt.Automaton.States;
using Nt.Syntax.Actions;
using Nt.Syntax.Structures;
using StateAutomaton = Nt.Automaton.Automatons.StateAutomaton<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;

namespace Nt.Syntax.Automaton
{
    internal abstract class BaseAutomaton
    {
        internal AutomatonContext Context { get; set; } = new AutomatonContext();
        protected StateAutomaton? Automaton {  get; set; }
        protected State<string> InitialState { get; set; } = new State<string>();
        protected Grammar Grammar { get; private set; } = new Grammar();
        private bool Built { get; set; } = false;

        public BaseAutomaton() {
            Automaton = new StateAutomaton(InitialState);
            InitialState
            .SetDefault(
                new Transition(InitialState)
                .SetAction(new ExpectedAction("a new block"))
            );
        }

        internal void Build(Grammar grammar)
        {
            Context.Reset();
            Grammar = grammar;

            // Build the automaton if it hasn't been built yet
            if (!Built)
            {
                Built = true;
                Build();
            }
        }

        protected abstract void Build();

        internal void Read(AutomatonToken token)
        {
            Automaton?.Read(token);
        }
        internal SyntaxBlock CreateBlock(string blockName)
        {
            var syntaxBlock = new SyntaxBlock(blockName);
            syntaxBlock.Link(InitialState, InitialState);
            return syntaxBlock;
        }
    }
}
