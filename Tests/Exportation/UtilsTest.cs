using Nt.Parser.Symbols;
using Nt.Syntax.Exportation;
using Nt.Syntax.Structures;
using Nt.Syntax.Builders;
using static Nt.Syntax.Exportation.ExportationMethods;
using Nt.Parser.Structures;

namespace Nt.Tests.Syntax.Exportation
{
    public class UtilsTest
    {
        readonly SymbolFactory factory = new SymbolFactory();


        // Symbols

        [Fact]
        public void Symbols_Reorder_ShouldReturnOriginalOrder()
        {
            var grammar = new Grammar();
            grammar.GetBuilder().AddNonTerminals(["B", "A", "C"]);

            var result = Reorder([.. grammar.NonTerminals], ExportationMode.Original);

            Assert.Equal(3, result.Count);
            Assert.Equal(grammar.NonTerminals.Get(0), result[0]);
            Assert.Equal(grammar.NonTerminals.Get(1), result[1]);
            Assert.Equal(grammar.NonTerminals.Get(2), result[2]);
        }

        [Fact]
        public void Symbols_Reorder_ShouldReturnAlphabeticalOrder()
        {
            var grammar = new Grammar();
            grammar.GetBuilder().AddNonTerminals(["B", "A", "C"]);

            var result = Reorder([.. grammar.NonTerminals], ExportationMode.Alphabetical);

            Assert.Equal(3, result.Count);
            Assert.Equal(grammar.NonTerminals.Get(1), result[0]);
            Assert.Equal(grammar.NonTerminals.Get(0), result[1]);
            Assert.Equal(grammar.NonTerminals.Get(2), result[2]);
        }

        [Fact]
        public void Symbols_Reorder_ShouldGroupSymbols()
        {
            var grammar = new Grammar();
            grammar.GetBuilder().AddNonTerminals(["A", "B", "BA", "AB"]);

            var result = Reorder([.. grammar.NonTerminals], ExportationMode.Grouped);

            Assert.Equal(4, result.Count);
            Assert.Equal(grammar.NonTerminals.Get(0), result[0]);
            Assert.Equal(grammar.NonTerminals.Get(3), result[1]);
            Assert.Equal(grammar.NonTerminals.Get(1), result[2]);
            Assert.Equal(grammar.NonTerminals.Get(2), result[3]);
        }

        [Fact]
        public void Symbols_Reorder_ShouldNotReorderIfNoCommonRoot()
        {
            var grammar = new Grammar();
            grammar.GetBuilder().AddNonTerminals(["AB", "AC"]);

            var result = Reorder([.. grammar.NonTerminals], ExportationMode.Grouped);

            Assert.Equal(2, result.Count);
            Assert.Equal(grammar.NonTerminals.Get(0), result[0]);
            Assert.Equal(grammar.NonTerminals.Get(1), result[1]);
        }

        [Fact]
        public void Symbols_Reorder_ShouldGroupSymbolsButConserveOrder()
        {
            var grammar = new Grammar();
            grammar.GetBuilder().AddNonTerminals(["A", "B", "A1", "A2", "B1", "B2"]);

            var result = Reorder([.. grammar.NonTerminals], ExportationMode.Grouped);

            Assert.Equal(6, result.Count);
            Assert.Equal(grammar.NonTerminals.Get(0), result[0]);
            Assert.Equal(grammar.NonTerminals.Get(2), result[1]);
            Assert.Equal(grammar.NonTerminals.Get(3), result[2]);
            Assert.Equal(grammar.NonTerminals.Get(1), result[3]);
            Assert.Equal(grammar.NonTerminals.Get(4), result[4]);
            Assert.Equal(grammar.NonTerminals.Get(5), result[5]);
        }

        // Rules

        [Fact]
        public void Rules_Reorder_ShouldReturnOriginalOrder()
        {
            var grammar = new Grammar();
            grammar.GetBuilder()
                .Add(new Rule(grammar))
                .Add(new Rule(grammar))
                .Add(new Rule(grammar));

            var result = Reorder(grammar.Rules.ToList(), ExportationMode.Original);

            Assert.Equal(grammar.Rules.Get(0), result[0]);
            Assert.Equal(grammar.Rules.Get(1), result[1]);
            Assert.Equal(grammar.Rules.Get(2), result[2]);
        }

        [Fact]
        public void Rules_Reorder_ShouldReturnAlphabeticalOrder()
        {
            var grammar = new Grammar()
                .GetBuilder()
                .AddNonTerminals(["B", "A", "C"])
                .Build();
            grammar.GetBuilder()
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("B"), -1)).Build())
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("A"), -1)).Build())
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("C"), -1)).Build());

            var result = Reorder(grammar.Rules.ToList(), ExportationMode.Alphabetical);

            Assert.Equal(grammar.Rules.Get(1), result[0]);
            Assert.Equal(grammar.Rules.Get(0), result[1]);
            Assert.Equal(grammar.Rules.Get(2), result[2]);
        }

        [Fact]
        public void Rules_Reorder_ShouldGroupRules()
        {
            var grammar = new Grammar();
            grammar.GetBuilder()
                .AddNonTerminals(["A", "B", "BA", "AB"])
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("A"), -1)).Build())
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("B"), -1)).Build())
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("BA"), -1)).Build())
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("AB"), -1)).Build());

            var result = Reorder(grammar.Rules.ToList(), ExportationMode.Grouped);

            Assert.Equal(grammar.Rules.Get(0), result[0]);
            Assert.Equal(grammar.Rules.Get(3), result[1]);
            Assert.Equal(grammar.Rules.Get(1), result[2]);
            Assert.Equal(grammar.Rules.Get(2), result[3]);
        }

        [Fact]
        public void Rules_Reorder_ShouldNotReorderIfNoCommonRoot()
        {
            var grammar = new Grammar();
            grammar.GetBuilder()
                .AddNonTerminals(["AB", "AC"])
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("AB"), -1)).Build())
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("AC"), -1)).Build());

            var result = Reorder(grammar.Rules.ToList(), ExportationMode.Grouped);

            Assert.Equal(grammar.Rules.Get(0), result[0]);
            Assert.Equal(grammar.Rules.Get(1), result[1]);
        }

        [Fact]
        public void Rule_Reorder_ShouldGroupRulesButConserveOrder()
        {
            var grammar = new Grammar();
            grammar.GetBuilder()
                .AddNonTerminals(["A", "A1", "A2", "B", "B1", "B2"])
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("A"), -1)).Build())
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("B"), -1)).Build())
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("A1"), -1)).Build())
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("A2"), -1)).Build())
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("B1"), -1)).Build())
                .Add(new Rule(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("B2"), -1)).Build());

            var result = Reorder(grammar.Rules.ToList(), ExportationMode.Grouped);

            Assert.Equal(6, result.Count);
            Assert.Equal(grammar.Rules.Get(0), result[0]);
            Assert.Equal(grammar.Rules.Get(2), result[1]);
            Assert.Equal(grammar.Rules.Get(3), result[2]);
            Assert.Equal(grammar.Rules.Get(1), result[3]);
            Assert.Equal(grammar.Rules.Get(4), result[4]);
            Assert.Equal(grammar.Rules.Get(5), result[5]);
        }



        // Regular Expressions

        [Fact]
        public void RegularExpressions_Reorder_ShouldReturnOriginalOrder()
        {
            var grammar = new Grammar();
            grammar.GetBuilder()
                .Add(new RegularExpression(grammar))
                .Add(new RegularExpression(grammar))
                .Add(new RegularExpression(grammar));

            var result = Reorder(grammar.RegularExpressions.ToList(), ExportationMode.Original);

            Assert.Equal(grammar.RegularExpressions.Get(0), result[0]);
            Assert.Equal(grammar.RegularExpressions.Get(1), result[1]);
            Assert.Equal(grammar.RegularExpressions.Get(2), result[2]);
        }

        [Fact]
        public void RegularExpressions_Reorder_ShouldReturnAlphabeticalOrder()
        {
            var grammar = new Grammar();
            grammar.GetBuilder()
                .AddNonTerminals(["B", "A", "C"])
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("B"), -1)).Build())
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("A"), -1)).Build())
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("C"), -1)).Build());

            var result = Reorder(grammar.RegularExpressions.ToList(), ExportationMode.Alphabetical);

            Assert.Equal(grammar.RegularExpressions.Get(1), result[0]);
            Assert.Equal(grammar.RegularExpressions.Get(0), result[1]);
            Assert.Equal(grammar.RegularExpressions.Get(2), result[2]);
        }

        [Fact]
        public void RegularExpressions_Reorder_ShouldGroupRegularExpressions()
        {
            var grammar = new Grammar();
            grammar.GetBuilder()
                .AddNonTerminals(["A", "B", "BA", "AB"])
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("A"), -1)).Build())
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("B"), -1)).Build())
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("BA"), -1)).Build())
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("AB"), -1)).Build());

            var result = Reorder(grammar.RegularExpressions.ToList(), ExportationMode.Grouped);

            Assert.Equal(grammar.RegularExpressions.Get(0), result[0]);
            Assert.Equal(grammar.RegularExpressions.Get(3), result[1]);
            Assert.Equal(grammar.RegularExpressions.Get(1), result[2]);
            Assert.Equal(grammar.RegularExpressions.Get(2), result[3]);
        }

        [Fact]
        public void RegularExpressions_Reorder_ShouldNotReorderIfNoCommonRoot()
        {
            var grammar = new Grammar();
            grammar.GetBuilder()
                .AddNonTerminals(["AB", "AC"])
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("AB"), -1)).Build())
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("AC"), -1)).Build());
            var result = Reorder(grammar.RegularExpressions.ToList(), ExportationMode.Grouped);
            Assert.Equal(grammar.RegularExpressions.Get(0), result[0]);
            Assert.Equal(grammar.RegularExpressions.Get(1), result[1]);
        }

        [Fact]
        public void RegularExpressions_Reorder_ShouldGroupRegularExpressionsButConserveOrder()
        {
            var grammar = new Grammar();
            grammar.GetBuilder()
                .AddNonTerminals(["A", "A1", "A2", "B", "B1", "B2"])
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("A"), -1)).Build())
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("B"), -1)).Build())
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("A1"), -1)).Build())
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("A2"), -1)).Build())
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("B1"), -1)).Build())
                .Add(new RegularExpression(grammar).GetBuilder().SetToken(new NonTerminal(new Symbol("B2"), -1)).Build());

            var result = Reorder(grammar.RegularExpressions.ToList(), ExportationMode.Grouped);

            Assert.Equal(6, result.Count);
            Assert.Equal(grammar.RegularExpressions.Get(0), result[0]);
            Assert.Equal(grammar.RegularExpressions.Get(2), result[1]);
            Assert.Equal(grammar.RegularExpressions.Get(3), result[2]);
            Assert.Equal(grammar.RegularExpressions.Get(1), result[3]);
            Assert.Equal(grammar.RegularExpressions.Get(4), result[4]);
            Assert.Equal(grammar.RegularExpressions.Get(5), result[5]);
        }


    }
}
