using Nt.Parser.Symbols;
using Nt.Syntax;
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


    }
}
