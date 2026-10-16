using Nt.Syntax.Exceptions;
using System.Text;

namespace Nt.Syntax.Structures
{
    public class RegularExpression(Grammar grammar)
    {
        internal Grammar Grammar { get; } = grammar;
        public NonTerminal? Token { get; internal set; }
        public string Pattern { get; private set; } = "";

        /// <summary>
        /// Clears the current pattern of the regular expression.
        /// </summary>
        internal void Clear()
        {
            Pattern = "";
        }

        internal void SetPattern(string pattern)
        {
            Pattern = pattern;
        }

        internal void AddPattern(string pattern)
        {
            Pattern += pattern;
        }

        /// <summary>
        /// Gets a string representation of this regular expression
        /// </summary>
        /// <returns>A string representing the regular expression</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Token != null) sb.Append(Token.Symbol.Name);
            else sb.Append("<<undefined>>");

            sb.Append(" = ").Append(Pattern.Equals("") ? "<<empty>>" : Pattern);
            return sb.ToString();
        }
    }
}
