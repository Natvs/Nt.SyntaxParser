using GrammarParser.Domain.Grammar;
using GrammarParser.Domain.Parser.Structures;

namespace Tests.Domain.Grammar
{
    public class AutomatonTest
    {

        [Fact]
        public void AutomatonTransition_Test1()
        {
            var tokens = new TokensList(["a"]);
            State initial = new(), state1 = new();
            StateSequence(initial, [(state1, "a")]);

            var automaton = new Automaton(tokens, initial);
            Read(automaton, ["a"]);

            Assert.Equal(state1, automaton.CurrentState);
        }

        [Fact]
        public void AutomatonTransition_Test2()
        {
            var tokens = new TokensList(["a", "b", "c", "d"]);
            State initial = new(), state1 = new(), state2 = new(), state3 = new(), state4 = new();
            StateSequence(initial, [(state1, "a"), (state2, "b"), (state3, "c"), (state4, "d")]);

            var automaton = new Automaton(tokens, initial);
            Read(automaton, ["a", "b", "c", "d"]);

            Assert.Equal(state4, automaton.CurrentState);
        }

        [Fact]
        public void AutomatonDefaultTransition_Test()
        {
            var tokens = new TokensList(["a", "b", "c", "d", "e", "f", "g"]);
            State initial = new(), state1 = new(), state2 = new(), state3 = new(), state4 = new();
            StateSequence(initial, [(state1, "a"), (state2, "b"), (state3, "c"), (state4, "d"), (state1, "e"), (state2, "f"), (state3, "g")]);
            
            var automaton = new Automaton(tokens, initial);
            Read(automaton, ["a", "b", "c", "d", "f", "g", "e"]);

            Assert.Equal(initial, automaton.CurrentState);
        }

        private void StateSequence(State initial, List<(State, string)> states)
        {
            initial.SetDefault(initial);
            var lastState = initial;
            foreach (var (state, word) in states)
            {
                state.SetDefault(initial);
                lastState.AddTransition(word, state);
                lastState = state;
            }
        }

        private void Read(Automaton automaton, List<string> words)
        {
            var context = new AutomatonContext();
            foreach (var word in words) automaton.Read(new(automaton.Tokens.IndexOf(word), 0), context);
        }
    }
}
