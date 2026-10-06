using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax.Actions;
using Nt.Syntax.Branching;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using State = Nt.Automaton.States.State<string>;
using StateAutomaton = Nt.Automaton.Automatons.StateAutomaton<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;

namespace Nt.Syntax.Automaton
{
    internal abstract class BaseAutomaton
    {
        protected AutomatonContext Context { get; set; } = new AutomatonContext();
        protected StateAutomaton? Automaton {  get; set; }
        protected Grammar Grammar { get; private set; } = new Grammar();

        private HashSet<BranchStart> BranchStarts { get; set; } = [];
        private HashSet<BranchEnd> BranchEnds { get; set; } = [];

        public BaseAutomaton()
        {
            
        }

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
                Automaton.InitialState.AddTransition(new Transition(branch.Token, branch.Target));
            }
            foreach (var branch in BranchEnds)
            {
                branch.Origin.AddTransition(new Transition(branch.Token, Automaton.InitialState));
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
