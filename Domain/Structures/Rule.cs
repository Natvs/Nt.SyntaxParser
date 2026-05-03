using Nt.Syntax.Exceptions;
using System.Text;

namespace Nt.Syntax.Structures
{

    /// <summary>
    /// Represents a grammar rule
    /// </summary>
    /// <param name="terminals">Terminals tokens of the grammar</param>
    /// <param name="nonterminals">Non terminals tokens of the grammar</param>
    public class Rule(Grammar grammar)
    {
        internal Grammar Grammar { get; } = grammar;
        /// <summary>
        /// Token that would derive into derivation
        /// </summary>
        public NonTerminal? Token { get; internal set; }
        /// <summary>
        /// List of tokens that represent derivation
        /// </summary>
        public Derivation Derivation { get; } = new();

        /// <summary>
        /// Gets a string representing this rule
        /// </summary>
        /// <returns>String with the derivated symbol and derivation of the rule</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Token == null ? "<<undefined>>" : Token.Name);
            sb.Append(" -> "); sb.Append(Derivation.ToString());
            return sb.ToString();
        }
    }
}
