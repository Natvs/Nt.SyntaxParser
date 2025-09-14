using GrammarReader.Code.Class;

namespace GrammarReader.Code.Parser
{
    public class ParserResult
    {
        public TokensList Tokens { get; }
        public ParsedList Parsed { get; }

        public ParserResult()
        {
            Tokens = [];
            Parsed = new(Tokens);
        }

    }
}
