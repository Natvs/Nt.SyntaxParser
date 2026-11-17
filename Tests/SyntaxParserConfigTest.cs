using Nt.Parser.Symbols;
using Nt.Syntax;
using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax
{
    class CustomSymbolFactory : ISymbolFactory
    {
        public ISymbol Create(string value)
        {
            return new CustomSymbol(value);
        }
    }

    class CustomSymbol(string value) : ISymbol
    {
        public string Name { get; } = value;
    }

    public class SyntaxParserConfigTest
    {
        [Fact]
        public void SyntaxParserConfig_GetInstance_ShouldReturnSameInstance()
        {
            var instance1 = SyntaxParserConfig.GetConfig();
            var instance2 = SyntaxParserConfig.GetConfig();
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void SyntaxParserConfig_SetFactory_ShouldSetFactory()
        {
            var factory = new SymbolFactory();
            var instance = SyntaxParserConfig.GetConfig();
            instance.SetSymbolFactory(factory);
            Assert.Same(factory, instance.SymbolFactory);
        }

        [Fact]
        public void SyntaxParserConfig_SetFactory_ShouldCreateCorrectTerminal()
        {
            var factory = new CustomSymbolFactory();
            var instance = SyntaxParserConfig.GetConfig();
            instance.SetSymbolFactory(factory);

            var grammar = ParseTest(TestType.Syntax, "SingleTerminal");

            Assert.IsType<CustomSymbol>(grammar.Terminals[0].Symbol);
        }

        [Fact]
        public void SyntaxParserConfig_SetFactory_ShouldCreateCorrectNonTerminal()
        {
            var factory = new CustomSymbolFactory();
            var instance = SyntaxParserConfig.GetConfig();
            instance.SetSymbolFactory(factory);

            var grammar = ParseTest(TestType.Syntax, "SingleNonTerminal");

            Assert.IsType<CustomSymbol>(grammar.NonTerminals[0].Symbol);
        }

        [Fact]
        public void SyntaxParserConfig_SetFactory_ShouldCreateCorrectAxiom()
        {
            var factory = new CustomSymbolFactory();
            var instance = SyntaxParserConfig.GetConfig();
            instance.SetSymbolFactory(factory);

            var grammar = ParseTest(TestType.Syntax, "Axiom");

            Assert.IsType<CustomSymbol>(grammar.Axiom?.Symbol);
        }

        [Fact]
        public void SyntaxParserConfig_SetFactory_ShouldCreateCorrectRule()
        {
            var factory = new CustomSymbolFactory();
            var instance = SyntaxParserConfig.GetConfig();
            instance.SetSymbolFactory(factory);

            var grammar = ParseTest(TestType.Syntax, "SingleRule");

            Assert.IsType<CustomSymbol>(grammar.Rules[0].Axiom?.Symbol);
            foreach (var symbol in grammar.Rules[0].Derivation)
            {
                Assert.IsType<CustomSymbol>(symbol.Symbol);
            }
        }

        [Fact]
        public void SyntaxParserConfig_SetFactory_ShouldCreateCorrectRegex()
        {
            var factory = new CustomSymbolFactory();
            var instance = SyntaxParserConfig.GetConfig();
            instance.SetSymbolFactory(factory);

            var grammar = ParseTest(TestType.Syntax, "SingleRegex");

            Assert.IsType<CustomSymbol>(grammar.RegularExpressions[0].Axiom?.Symbol);
        }

    }
}
