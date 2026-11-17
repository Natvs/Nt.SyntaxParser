using Nt.Automaton.States;
using Nt.Syntax.Actions;
using Nt.Syntax.Structures;
using StateAutomaton = Nt.Automaton.Automatons.StateAutomaton<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;

namespace Nt.Syntax.Automaton
{
    internal abstract class BaseAutomaton
    {
        public AutomatonContext Context { get; protected set; } = new AutomatonContext();
        protected StateAutomaton? Automaton {  get; set; }
        protected State<string> InitialState { get; set; } = new State<string>();
        protected Grammar Grammar { get; private set; } = new Grammar();
        protected List<SyntaxBlock> SyntaxBlocks { get; } = [];

        public BaseAutomaton() {
            Automaton = new StateAutomaton(InitialState);
            InitialState
            .SetDefault(
                new Transition(InitialState)
                .SetAction(new ExpectedAction("a new block"))
            );
        }

        internal void BuildNodes(Grammar grammar)
        {
            Grammar = grammar;
            Context.Reset();
            SyntaxBlocks.Clear();

            // Builds the automaton with the provided grammar
            Build();

            // Add the predefined blocks in the automaton
            foreach (var block in SyntaxBlocks)
            {
                block.Link(InitialState, InitialState);
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
            SyntaxBlocks.Add(syntaxBlock);

            return syntaxBlock;
        }
    }
}
