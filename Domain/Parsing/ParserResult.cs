using Nt.Parsing.Structures;
using System.Text;

namespace Nt.Parsing
{
    /// <summary>
    /// Contains results of a successive parsing. 
    /// </summary>
    public class ParserResult
    {
        /// <summary>
        /// List of unique symbols that the parser read. These words are referenced by parsed tokens.
        /// </summary>
        public SymbolsList Symbols { get; } = [];

        /// <summary>
        /// List of tokens that have been parsed. Value of a parsed token refers to the index in tokens list.
        /// </summary>
        public ParsedList Parsed { get; }

        public ParserResult()
        {
            Parsed = new(Symbols);
        }

        public string ToShortString()
        {
            var sb = new StringBuilder().Append('{');
            for (int i = 0; i < Parsed.Count - 1; i++)
            {
                var token = Symbols[Parsed[i].TokenIndex].Name;
                sb.Append($"{token}, ");
            }
            if (Parsed.Count > 0)
            {
                var token = Symbols[Parsed[Parsed.Count - 1].TokenIndex].Name;
                sb.Append($"{token}");
            }
            sb.Append('}');
            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder().Append("Symbols = {");

            for (int i = 0; i < Symbols.Count - 1; i++)
            {
                sb.Append($"'{Symbols[i]}', ");
            }
            if (Symbols.Count > 0)
            {
                sb.Append($"'{Symbols[Symbols.Count - 1]}'");
            }
            sb.Append("}, Tokens = {");
            for (int i = 0; i < Parsed.Count - 1; i++)
            {
                if (i > 0) sb.Append(", ");
                var token = Parsed[i];
                sb.Append($"(Line: {token.Line}, Value: '{Symbols[token.TokenIndex]}')");
            }
            if (Parsed.Count > 0)
            {
                var token = Parsed[Parsed.Count - 1];
                sb.Append($"(Line: {token.Line}, Value: '{Symbols[token.TokenIndex]}')");
            }

            return sb.ToString();
        }

    }
}
