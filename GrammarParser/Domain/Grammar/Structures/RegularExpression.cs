using GrammarParser.Domain.Parser.Structures;
using System.Text;

namespace GrammarParser.Domain.Grammar.Structures
{
    public class RegularExpression(TokensList nonterminals)
    {
        public NonTerminal? Token { get; private set; }
        public string RegExString { get; private set; } = "";

        /// <summary>
        /// Sets a non terminal that would derive into word matching the regular expression
        /// </summary>
        /// <param name="index">Index of the non terminal</param>
        /// <param name="line">Line the symbol has been parsed</param>
        internal void SetToken(int index, int line)
        {
            Token = new NonTerminal(index, line);
        }
        /// <summary>
        /// Adds a sequence of symbol to the regular expression
        /// </summary>
        /// <param name="symbols">Sequence of symbols to add</param>
        internal void AddSymbols(string symbols)
        {
            RegExString += symbols;
        }

        /// <summary>
        /// Gets a string representation of this regular expression
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Token != null) sb.Append(nonterminals[Token.Index].Name);
            sb.Append(" = ").Append(RegExString);
            return sb.ToString();
        }
    }
}
