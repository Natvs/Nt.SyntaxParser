using Nt.Parser.Structures;
using System.Text;

namespace Nt.Syntax.Structures
{
    /// <summary>
    /// Represents a rule derivation of tokens
    /// </summary>
    /// <param name="terminals">Terminal tokens of the rule</param>
    /// <param name="nonterminals">Non terminal tokens of the rule</param>
    public class Derivation : List<GrammarToken>
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
                    sb.Append(this[i].Symbol.Name).Append(' ');
                }
                sb.Append(this[Count - 1].Symbol.Name);
            }
            else
            {
                sb.Append("<<undefined>>");
            }
            return sb.ToString();
        }

    }
}
