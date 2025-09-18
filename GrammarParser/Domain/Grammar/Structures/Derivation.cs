using GrammarParser.Domain.Parser.Structures;
using System.Text;

namespace GrammarParser.Domain.Grammar.Structures
{
    /// <summary>
    /// Represents a rule derivation of tokens
    /// </summary>
    /// <param name="terminals">Terminal tokens of the rule</param>
    /// <param name="nonterminals">Non terminal tokens of the rule</param>
    public class Derivation(TokensList terminals, TokensList nonterminals) : List<GrammarToken>
    {
        /// <summary>
        /// Gets a string representing the list of tokens in derivation
        /// </summary>
        /// <returns>String representing the derivation</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Count > 0)
            {
                for (int i = 0; i < Count - 1; i++)
                {
                    if (this[i] is Terminal) sb.Append(terminals[this[i].Index].Name).Append(' ');
                    else sb.Append(nonterminals[this[i].Index].Name).Append(' ');
                }
                if (this[Count - 1] is Terminal) sb.Append(terminals[this[Count - 1].Index].Name).Append(' ');
                else sb.Append(nonterminals[this[Count - 1].Index].Name).Append(' ');
            }
            else
            {
                sb.Append("<<undefined>>");
            }
            return sb.ToString();
        }

    }
}
