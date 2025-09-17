namespace GrammarReader.Infrastructure.Parser.Structures
{
    /// <summary>
    /// Represents a parsed token identified by its index in list of tokens
    /// </summary>
    /// <param name="value">Index in tokens list</param>
    /// <param name="line">Line the token have been parsed</param>
    public class ParsedToken(int value, int line)
    {
        /// <summary>
        /// Index of this parsed token in the list of tokens
        /// </summary>
        public int TokenIndex { get; } = value;
        /// <summary>
        /// Line the token have been parsed
        /// </summary>
        public int Line { get; } = line;
    }
}
