using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using Nt.Syntax.Actions;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;
using static Nt.Tests.Syntax.SyntaxTestUtils;
using Nt.Parser.Symbols;

namespace Nt.Tests.Syntax.Actions
{
    public class AddRegExSymbolActionTest
    {
        private SymbolFactory SymbolFactory = new SymbolFactory();

        [Fact]
        public void AddRegExSymbolAction_AddSymbolTest1()
        {
            var symbols = new SymbolsList(SymbolFactory, ["S", "ab*"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var newaction = new AddNewRegExAction(grammar, context);
            newaction.Perform(new AutomatonToken(symbols.Get(0), 0));

            var action = new AddRegExSymbolsAction(grammar, context);
            action.Perform(new AutomatonToken(symbols.Get(1), 0));

            AssertRegex(context.Regex, "S", "ab*");;
        }

        [Fact]
        public void AddRegExSymbolAction_AddSymbolTest2()
        {
            var symbols = new SymbolsList(SymbolFactory, ["S", "a", "+", "(", "bc", ")", "*"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var newaction = new AddNewRegExAction(grammar, context);
            newaction.Perform(new AutomatonToken(symbols.Get(0), 0));

            var action = new AddRegExSymbolsAction(grammar, context);
            for (int i = 1; i < symbols.GetCount(); i++)
            {
                action.Perform(new AutomatonToken(symbols.Get(i), 0));
            }

            AssertRegex(context.Regex, "S", "a+(bc)*");
        }

        [Fact]
        public void AddRegExSymbolAction_NullRegexTest()
        {
            var symbols = new SymbolsList(SymbolFactory, ["S", "(ab)+"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();

            var action = new AddRegExSymbolsAction(grammar, context);
            Assert.Throws<NullRegexException>(() => action.Perform(new AutomatonToken(symbols.Get(1), 0)));
        }

        [Fact]
        public void AddRegExSymbolAction_EscapeTest1()
        {
            var symbols = new SymbolsList(SymbolFactory, ["S", "'a"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var newaction = new AddNewRegExAction(grammar, context);
            newaction.Perform(new AutomatonToken(symbols.Get(0), 0));

            var action = new AddRegExSymbolsAction(grammar, context);
            action.Perform(new AutomatonToken(symbols.Get(1), 0));

            AssertRegex(context.Regex, "S", "a");
        }

        [Fact]
        public void AddRegExSymbolAction_EscapeTest2()
        {
            var symbols = new SymbolsList(SymbolFactory, ["S", "a'b"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var newaction = new AddNewRegExAction(grammar, context);
            newaction.Perform(new AutomatonToken(symbols.Get(0), 0));

            var action = new AddRegExSymbolsAction(grammar, context);
            action.Perform(new AutomatonToken(symbols.Get(1), 0));

            AssertRegex(context.Regex, "S", "ab");
        }
    }
}
