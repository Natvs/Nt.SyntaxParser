using GrammarReader.Domain.Parser.Structures;

namespace GrammarReader.Domain.Parser
{
    /// <summary>
    /// Contains results of a successive parsing. 
    /// </summary>
    public class ParserResult
    {
        /// <summary>
        /// List of unique symbols that the parser read. These words are referenced by parsed tokens.
        /// </summary>
        public TokensList Tokens { get; } = [];

        /// <summary>
        /// List of tokens that have been parsed. Value of a parsed token refers to the index in tokens list.
        /// </summary>
        public ParsedList Parsed { get; }

        public ParserResult()
        {
            Parsed = new(Tokens);
        }

    }
}
