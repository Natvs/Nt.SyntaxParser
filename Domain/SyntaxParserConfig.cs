using Nt.Parser.Symbols;


namespace Nt.Syntax
{
    public class SyntaxParserConfig
    {
        public ISymbolFactory SymbolFactory { get; private set; } = new SymbolFactory();

        public void SetSymbolFactory(ISymbolFactory factory)
        {
            SymbolFactory = factory;
        }

        private static SyntaxParserConfig? _instance = null;

        public static SyntaxParserConfig GetConfig()
        {
            _instance ??= new SyntaxParserConfig();
            return _instance;
        }
    }
}
