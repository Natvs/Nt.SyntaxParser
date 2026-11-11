using Nt.Automaton.Actions;
using Nt.Automaton.States;
using Nt.Automaton.Tokens;
using Nt.Automaton.Transitions;
using Nt.Parser.Symbols;
using Nt.Syntax;
using Nt.Syntax.Automaton;
using Nt.Syntax.Branching;
using Nt.Syntax.Builders;
using Nt.Syntax.Structures;

namespace Nt.Tests.Syntax.Branching
{
    internal class TestDataFactory : IDataFactory
    {
        public IData Create()
        {
            return new TestData("test");
        }
    }

    internal class TestData : IData
    {
        public TestData() { }
        public TestData(string message)
        {
            Message = message;
        }

        public string Message { get; } = "";
    }

    public class DataManagerTest
    {

        // Type of the data

        [Fact]
        public void DataManager_CreateRuleData_TypeShouldBeCorrect()
        {
            var grammar = new Grammar();
            var rule = new Rule(grammar);

            rule.CreateData(new TestData());
            var data = rule.GetData<TestData>();

            Assert.IsType<TestData>(data);
        }

        [Fact]
        public void DataManager_CreateRegexData_TypeShouldBeCorrect()
        {
            var grammar = new Grammar();
            var regex = new RegularExpression(grammar);

            regex.CreateData(new TestData());
            var data = regex.GetData<TestData>();

            Assert.IsType<TestData>(data);
        }

        [Fact]
        public void DataManager_CreateTerminalData_TypeShouldBeCorrect()
        {
            var terminal = new Terminal(new Symbol("test"), 0);

            terminal.CreateData(new TestData());
            var data = terminal.GetData<TestData>();

            Assert.IsType<TestData>(data);
        }

        [Fact]
        public void DataManager_CreateNonTerminalData_TypeShouldBeCorrect()
        {
            var nonterminal = new NonTerminal(new Symbol("test"), 0);

            nonterminal.CreateData(new TestData());
            var data = nonterminal.GetData<TestData>();

            Assert.IsType<TestData>(data);
        }

        
        // Persistence of the data content

        [Fact]
        public void DataManager_CreatRuleData_MessageShouldPersist()
        {
            var grammar = new Grammar();
            var rule = new Rule(grammar);

            rule.CreateData(new TestData("test"));
            var data = rule.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }

        [Fact]
        public void DataManager_CreateRegexData_MessageShouldPersist()
        {
            var grammar = new Grammar();
            var regex = new RegularExpression(grammar);

            regex.CreateData(new TestData("test"));
            var data = regex.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }

        [Fact]
        public void DataManager_CreateTerminalData_MessageShouldPersist()
        {
            var terminal = new Terminal(new Symbol("test"), 0);

            terminal.CreateData(new TestData("test"));
            var data = terminal.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }

        [Fact]
        public void DataManager_CreateNonTerminalData_MessageShouldPersist()
        {
            var nonterminal = new NonTerminal(new Symbol("test"), 0);

            nonterminal.CreateData(new TestData("test"));
            var data = nonterminal.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }


        // Persistence of the data after modifications

        [Fact]
        public void DataManager_CreateRuleData_MessageShouldPersistAfterModification()
        {
            var grammar = new Grammar();
            var rule = new Rule(grammar);

            rule.CreateData(new TestData("test"));
            grammar.GetBuilder().AddNonTerminal("A");
            rule.GetBuilder().SetToken(new NonTerminal(grammar.NonTerminals.Get((t) => t.Symbol.Name == "A").Symbol, 0));

            var data = rule.GetData<TestData>();
            Assert.Equal("test", data.Message);
        }

        [Fact]
        public void DataManager_CreateRegexData_MessageShouldPersistAfterModification()
        {
            var grammar = new Grammar();
            var regex = new RegularExpression(grammar);

            regex.CreateData(new TestData("test"));
            grammar.GetBuilder().AddNonTerminal("A");
            regex.GetBuilder().SetToken(new NonTerminal(grammar.NonTerminals.Get((t) => t.Symbol.Name == "A").Symbol, 0));

            var data = regex.GetData<TestData>();
            Assert.Equal("test", data.Message);
        }


        // Creation of the data with a factory

        [Fact]
        public void DataManager_GetRuleData_ShouldCreateDataWithFactory()
        {
            var grammar = new Grammar();
            var rule = new Rule(grammar);

            DataManager.SetRuleFactory(new TestDataFactory());
            var data = rule.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }

        [Fact]
        public void DataManager_GetRegexData_ShouldCreateDataWithFactory()
        {
            var grammar = new Grammar();
            var regex = new RegularExpression(grammar);

            DataManager.SetRegularExpressionFactory(new TestDataFactory());
            var data = regex.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }

        [Fact]
        public void DataManager_GetTerminalData_ShouldCreateDataWithFactory()
        {
            var terminal = new Terminal(new Symbol("test"), 0);

            DataManager.SetTerminalFactory(new TestDataFactory());
            var data = terminal.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }

        [Fact]
        public void DataManager_GetNonTerminalData_ShouldCreateDataWithFactory()
        {
            var nonterminal = new NonTerminal(new Symbol("test"), 0);

            DataManager.SetNonTerminalFactory(new TestDataFactory());
            var data = nonterminal.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }



        // Parsing tests
        
        private static void SetBranch(SyntaxParser parser, string start, Action<AutomatonContext, IAutomatonToken<string>> action)
        {
            var valueState = new State<string>();
            var equalState = new State<string>()
                .SetDefault(
                    new Transition<string>(valueState)
                    .SetAction(parser, action)
                );
            var tagState = new State<string>()
                .AddTransition(new Transition<string>("=", equalState));
            parser.StartBranch(start, tagState);
            parser.EndBranch(valueState, ";");
        }

        private static void SetTerminalBranching(SyntaxParser parser, string start)
        {
            SetBranch(parser, start, (context, token) =>
            {
                context.Terminal?.CreateData(new TestData(token.Value));
            });
        }

        private static void SetNonTerminalBranching(SyntaxParser parser, string start)
        {
            SetBranch(parser, start, (context, token) =>
            {
                context.NonTerminal?.CreateData(new TestData(token.Value));
            });
        }

        private static void SetRuleBranching(SyntaxParser parser, string start)
        {
            SetBranch(parser, start, (context, token) =>
            {
                context.Rule?.CreateData(new TestData(token.Value));
            });
        }

        private static void SetRegexBranching(SyntaxParser parser, string start)
        {
            SetBranch(parser, start, (context, token) =>
            {
                context.Regex?.CreateData(new TestData(token.Value));
            });
        }

        [Fact]
        public void DataManager_ParseTerminal_ShouldAssociateDataWithTerminal()
        {
            var parser = new SyntaxParser();
            var text = "T = {a} tag = test;";

            // Set up the branching states for parsing
            SetTerminalBranching(parser, "tag");

            // Parse the grammar and retrieve the terminal data
            var grammar = parser.ParseString(text);
            var terminal = grammar.Terminals.Get(0);
            var data = terminal.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }

        [Fact]
        public void DataManager_ParseNonTerminal_ShouldAssociateDataWithNonTerminal()
        {
            var parser = new SyntaxParser();
            var text = "N = {A} tag = test;";

            // Set up the branching states for parsing
            SetNonTerminalBranching(parser, "tag");

            // Parse the grammar and retrieve the non terminal data
            var grammar = parser.ParseString(text);
            var nonTerminal = grammar.NonTerminals.Get(0);
            var data = nonTerminal.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }

        [Fact]
        public void DataManager_ParseRule_ShouldTransferDataToRuleAxiom()
        {
            var parser = new SyntaxParser();
            var text = "N = {A} tag = test; T = {a} R: A -> a;";

            // Set up the branching states for parsing
            SetNonTerminalBranching(parser, "tag");

            // Parse the grammar and retrieve the non terminal data
            var grammar = parser.ParseString(text);
            var nonTerminal = grammar.Rules.Get(0).Token;
            var data = nonTerminal?.GetData<TestData>();

            Assert.Equal("test", data?.Message);
        }

        [Fact]
        public void DataManager_ParseRule_ShouldTransferDataToRuleDerivation()
        {
            var parser = new SyntaxParser();
            var text = "N = {S} N = {A} tag1 = test1; T = {a} tag2 = test2; R: S -> A a;";

            // Set up the branching states for parsing
            SetNonTerminalBranching(parser, "tag1");
            SetTerminalBranching(parser, "tag2");

            // Parse the grammar and retrieve the data
            var grammar = parser.ParseString(text);
            var nonTerminal = (NonTerminal)grammar.Rules.Get(0).Derivation.Get(0);
            var terminal = (Terminal)grammar.Rules.Get(0).Derivation.Get(1);
            var ntData = nonTerminal?.GetData<TestData>();
            var tData = terminal?.GetData<TestData>();

            Assert.Equal("test1", ntData?.Message);
            Assert.Equal("test2", tData?.Message);
        }

        [Fact]
        public void DataManager_ParseRule_ShouldAssociateDataWithRule()
        {
            var parser = new SyntaxParser();
            var text = "N = {A} T = {a} R: A -> a; tag = test;";

            // Set up the branching states for parsing
            SetRuleBranching(parser, "tag");

            // Parse the grammar and retrieve the non terminal data
            var grammar = parser.ParseString(text);
            var rule = grammar.Rules.Get(0);
            var data = rule.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }

        [Fact]
        public void DataManager_ParseRegex_ShouldAssociateDataWithRegex()
        {
            var parser = new SyntaxParser();
            var text = "N = {A} E: A = abc; tag = test;";

            // Set up the branching states for parsing
            SetRegexBranching(parser, "tag");

            // Parse the grammar and retrieve the non terminal data
            var grammar = parser.ParseString(text);
            var regex = grammar.RegularExpressions.Get(0);
            var data = regex.GetData<TestData>();

            Assert.Equal("test", data.Message);
        }

        [Fact]
        public void DataManager_ParserRegex_ShouldTransferDataToRegexAxiom()
        {
            var parser = new SyntaxParser();
            var text = "N = {A} tag = test; E: A = abc;";

            // Set up the branching states for parsing
            SetNonTerminalBranching(parser, "tag");

            // Parse the grammar and retrieve the non terminal data
            var grammar = parser.ParseString(text);
            var nonterminal = grammar.RegularExpressions.Get(0).Token;
            var data = nonterminal?.GetData<TestData>();

            Assert.Equal("test", data?.Message);
        }

    }
}
