using Nt.Syntax;
using Nt.Syntax.Branching;
using State = Nt.Automaton.States.State<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;

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
        public void SyntaxParser_SimpleBranching_ShouldFailOnInvalidInput()
        {
            var initialState = new State();
            var parser = new SyntaxParser();
            parser.StartBranch("start", initialState);
            parser.EndBranch(initialState, "end");

            Assert.Throws<Exception>(() => parser.ParseString("start invalid end"));
        }

        [Fact]
        public void SyntaxParser_TransitiveBranching_ShouldBeValid()
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
        public void SyntaxParser_MutipleBranching_ShouldBeValid()
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

        [Fact]
        public void SyntaxParser_BranchingWithStateAction_ShouldTriggerAction()
        {
            var parser = new SyntaxParser();
            var triggered = false;
            var state = new State().SetAction(parser, (context) => { triggered = true; });

            parser.StartBranch("start", state);
            parser.EndBranch(state, "end");
            parser.ParseString("start end");

            Assert.True(triggered);
        }

        [Fact]
        public void SyntaxParser_BranchingWithTransitionAction_ShouldTriggerAction()
        {
            var parser = new SyntaxParser();
            var triggered = false;
            var read = "";
            var state1 = new State();
            var state2 = new State();

            state1.AddTransition(new Transition("token", state2).SetAction(parser, (context, token) => 
                { 
                    triggered = true;
                    read = token.Value;
                }
            ));     
            parser.StartBranch("start", state1);
            parser.EndBranch(state2, "end");
            parser.ParseString("start token end");

            Assert.True(triggered);
            Assert.Equal("token", read);
        }

        [Fact]
        public void SyntaxParser_BranchingWithStateAction_ShouldRetrieveLastRule()
        {
            var parser = new SyntaxParser();
            var read = "";
            var state = new State().SetAction(parser, (context) =>
            {
                read = context.Rule?.ToString() ?? "";
            });

            parser.StartBranch("start", state);
            parser.EndBranch(state, "end");
            parser.ParseFile("../../../Resources/Branching/branching_state_rule.txt");

            Assert.Equal("A -> a b c", read);
        }

        [Fact]
        public void SyntaxParser_BranchingWithStateAction_ShouldRetrieveLastRegex()
        {
            var parser = new SyntaxParser();
            var read = "";
            var state = new State().SetAction(parser, (context) =>
            {
                read = context.Regex?.ToString() ?? "";
            });

            parser.StartBranch("start", state);
            parser.EndBranch(state, "end");
            parser.ParseFile("../../../Resources/Branching/branching_state_regex.txt");

            Assert.Equal("A = abc", read);
        }

        [Fact]
        public void SyntaxParser_BranchingWithTransitionAction_ShouldRetrieveLastRule()
        {
            var parser = new SyntaxParser();
            var read = "";
            var state = new State();
            var state2 = new State();

            state.AddTransition(new Transition("token", state2).SetAction(parser, (context, token) =>
            {
                read = context.Rule?.ToString() ?? "";
            }));

            parser.StartBranch("start", state);
            parser.EndBranch(state2, "end");
            parser.ParseFile("../../../Resources/Branching/branching_transition_rule.txt");

            Assert.Equal("A -> a b c", read);
        }

        [Fact]
        public void SyntaxParser_BranchingWithTransitionAction_ShouldRetrieveLastRegex()
        {
            var parser = new SyntaxParser();
            var read = "";
            var state = new State();
            var state2 = new State();

            state.AddTransition(new Transition("token", state2).SetAction(parser, (context, token) =>
            {
                read = context.Regex?.ToString() ?? "";
            }));

            parser.StartBranch("start", state);
            parser.EndBranch(state2, "end");
            parser.ParseFile("../../../Resources/Branching/branching_transition_regex.txt");

            Assert.Equal("A = abc", read);
        }

        [Fact]
        public void SyntaxParser_BranchingWithTransitionAction_ShouldRetrieveTokenRead()
        {
            var parser = new SyntaxParser();
            var read = "";
            var state = new State();
            var state2 = new State();

            state.AddTransition(new Transition("token", state2).SetAction(parser, (context, token) =>
            {
                read = token.Value;
            }));

            parser.StartBranch("start", state);
            parser.EndBranch(state2, "end");
            parser.ParseString("start token end");

            Assert.Equal("token", read);
        }

    }
}
