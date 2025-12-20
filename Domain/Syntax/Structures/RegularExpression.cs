using Nt.Parsing.Structures;
using System.Text;

namespace Nt.Syntax.Structures
{
    public class RegularExpression(SymbolsList nonterminals)
    {
        public NonTerminal? Token { get; private set; }
        public string Pattern { get; private set; } = "";

        /// <summary>
        /// Sets a non terminal that would derive into word matching the regular expression
        /// </summary>
        /// <param name="index">Index of the non terminal</param>
        /// <param name="line">Line the symbol has been parsed</param>
        public void SetToken(int index, int line)
        {
            Token = new NonTerminal(index, line);
        }
        /// <summary>
        /// Adds a sequence of symbol to the regular expression
        /// </summary>
        /// <param name="symbols">Sequence of symbols to add</param>
        public void AddSymbols(string symbols)
        {
            Pattern += symbols;
        }

        /// <summary>
        /// Gets a string representation of this regular expression
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Token != null) sb.Append(nonterminals[Token.Index].Name);
            else sb.Append("<<undefined>>");

            sb.Append(" = ").Append(Pattern.Equals("") ? "<<empty>>" : Pattern);
            return sb.ToString();
        }
    }
}
