using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax;
using Nt.Syntax.Branching;
using State = Nt.Automaton.States.State<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;
using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax.Branching
{
    public class BranchingExtensionsTest
    {

        [Fact]
        public void SyntaxParser_SimpleBranching_ShouldBeValid()
        {
            var initialState = new State();
            var parser = new SyntaxParser();

            parser.StartBranch("start", initialState);
            parser.EndBranch(initialState, "end");

            parser.ParseString("start end");
        }

        [Fact]
        public void SyntaxParser_TransitiveBranching_ShouldKeepInternalTransitions()
        {
            var initialState = new State();
            var state1 = new State();
            var state2 = new State();
            var parser = new SyntaxParser();

            initialState.AddTransition(new Transition("1", state1));
            state1.AddTransition(new Transition("2", state2));

            parser.StartBranch("start", initialState);
            parser.EndBranch(state2, "end");

            parser.ParseString("start 1 2 end");
        }

        [Fact]
        public void SyntaxParser_MutipleBranching_ShouldStartAnyBranch()
        {
            var state1 = new State();
            var state2 = new State();
            var state3 = new State();
            var parser = new SyntaxParser();

            parser.StartBranch("start1", state1);
            parser.StartBranch("start2", state2);
            parser.StartBranch("start3", state3);
            parser.EndBranch(state1, "end");
            parser.EndBranch(state2, "end");
            parser.EndBranch(state3, "end");

            parser.ParseString("start3 end start1 end start2 end");
        }

        

    }
}
