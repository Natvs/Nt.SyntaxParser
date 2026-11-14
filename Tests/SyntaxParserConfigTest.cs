using Nt.Parser.Symbols;
using Nt.Syntax;

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
        public void SyntaxParserConfig_SetFactory_ShouldCreateCorrectSymbols()
        {
            var factory = new CustomSymbolFactory();

            var instance = SyntaxParserConfig.GetConfig();
            instance.SetSymbolFactory(factory);

            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N = {A} T = {a}");

            Assert.IsType<CustomSymbol>(grammar.NonTerminals[0].Symbol);
            Assert.IsType<CustomSymbol>(grammar.Terminals[0].Symbol);
        }


    }
}
