using Nt.Syntax.Actions;
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
        protected Grammar Grammar { get; private set; }

        public BaseAutomaton(Grammar grammar)
        {
            Grammar = grammar;
            Build();
        }

        protected abstract void Build();

        internal void Read(AutomatonToken token)
        {
            Automaton?.Read(token);
        }
    }
}
