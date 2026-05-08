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
            var symbols = new SymbolsList(factory);
            symbols.AddRange(["B", "A", "C"]);

            var result = Reorder(symbols.GetSymbols(), ExportationMode.Original);

            Assert.Equal(3, result.Count);
            Assert.Equal(symbols.Get(0), result[0]);
            Assert.Equal(symbols.Get(1), result[1]);
            Assert.Equal(symbols.Get(2), result[2]);
        }

        [Fact]
        public void Symbols_Reorder_ShouldReturnAlphabeticalOrder()
        {
            var symbols = new SymbolsList(factory);
            symbols.AddRange(["B", "A", "C"]);

            var result = Reorder(symbols.GetSymbols(), ExportationMode.Alphabetical);

            Assert.Equal(3, result.Count);
            Assert.Equal(symbols.Get(1), result[0]);
            Assert.Equal(symbols.Get(0), result[1]);
            Assert.Equal(symbols.Get(2), result[2]);
        }

        [Fact]
        public void Symbols_Reorder_ShouldGroupSymbols()
        {
            var symbols = new SymbolsList(factory);
            symbols.AddRange(["A", "B", "BA", "AB"]);

            var result = Reorder(symbols.GetSymbols(), ExportationMode.Grouped);

            Assert.Equal(4, result.Count);
            Assert.Equal(symbols.Get(0), result[0]);
            Assert.Equal(symbols.Get(3), result[1]);
            Assert.Equal(symbols.Get(1), result[2]);
            Assert.Equal(symbols.Get(2), result[3]);
        }

        [Fact]
        public void Symbols_Reorder_ShouldNotReorderIfNoCommonRoot()
        {
            var symbols = new SymbolsList(factory);
            symbols.AddRange(["AB", "AC"]);

            var result = Reorder(symbols.GetSymbols(), ExportationMode.Grouped);

            Assert.Equal(2, result.Count);
            Assert.Equal(symbols.Get(0), result[0]);
            Assert.Equal(symbols.Get(1), result[1]);
        }

        [Fact]
        public void Symbols_Reorder_ShouldGroupSymbolsButConserveOrder()
        {
            var symbolA1 = new Symbol("A");
            var symbolA2 = new Symbol("A1");
            var symbolA3 = new Symbol("A2");
            var symbolB1 = new Symbol("B");
            var symbolB2 = new Symbol("B1");
            var symbolB3 = new Symbol("B2");
            var symbols = new List<ISymbol> { symbolA1, symbolB1, symbolA2, symbolA3, symbolB2, symbolB3 };

            var result = Reorder(symbols, ExportationMode.Grouped);

            Assert.Equal(6, result.Count);
            Assert.Equal(symbolA1, result[0]);
            Assert.Equal(symbolA2, result[1]);
            Assert.Equal(symbolA3, result[2]);
            Assert.Equal(symbolB1, result[3]);
            Assert.Equal(symbolB2, result[4]);
            Assert.Equal(symbolB3, result[5]);
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
