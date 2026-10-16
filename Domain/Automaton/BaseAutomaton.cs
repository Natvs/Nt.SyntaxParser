using Nt.Automaton.States;
using Nt.Syntax.Branching;
using Nt.Syntax.Structures;
using StateAutomaton = Nt.Automaton.Automatons.StateAutomaton<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;

namespace Nt.Syntax.Automaton
{
    internal abstract class BaseAutomaton
    {
        public AutomatonContext Context { get; protected set; } = new AutomatonContext();
        protected StateAutomaton? Automaton {  get; set; }
        protected Grammar Grammar { get; private set; } = new Grammar();

        private HashSet<BranchStart> BranchStarts { get; set; } = [];
        private HashSet<BranchEnd> BranchEnds { get; set; } = [];

        public BaseAutomaton() { }

        public void SetGrammar(Grammar grammar)
        {
            this.Grammar = grammar;
            Build();
            AddBranching();
        }

        protected abstract void Build();
        private void AddBranching()
        {
            if (Automaton == null) return;
            foreach (var branch in BranchStarts)
            {
                Automaton.CurrentState.AddTransition(new Transition(branch.Token, branch.Target));
            }
            foreach (var branch in BranchEnds)
            {
                branch.Origin.AddTransition(new Transition(branch.Token, Automaton.CurrentState));
            }
        }

        internal void Read(AutomatonToken token)
        {
            Automaton?.Read(token);
        }
        internal void StartBranch(string token, IState<string> state)
        {
            BranchStarts.Add(new(token, state));
        }
        internal void EndBranch(IState<string> state, string token)
        {
            BranchEnds.Add(new(state, token));
        }
    }
}
