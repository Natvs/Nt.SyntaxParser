using GrammarReader.Infrastructure.Parser.Structures;
using System.Text;

namespace GrammarReader.Domain.Grammar.Structures
{

    /// <summary>
    /// Represents a grammar rule
    /// </summary>
    /// <param name="terminals">Terminals tokens of the grammar</param>
    /// <param name="nonterminals">Non terminals tokens of the grammar</param>
    public class Rule(TokensList terminals, TokensList nonterminals)
    {
        /// <summary>
        /// Token that would derive into derivation
        /// </summary>
        public NonTerminal? Token { get; private set; }
        /// <summary>
        /// List of tokens that represent derivation
        /// </summary>
        private Derivation Derivation { get; } = new(terminals, nonterminals);


        /// <summary>
        /// Sets the token to derive
        /// </summary>
        /// <param name="index">Index of the token in non terminals list</param>
        /// <param name="line">Line the token have been parsed</param>
        public void SetToken(int index, int line)
        {
            Token = new NonTerminal(index, line);
        }

        /// <summary>
        /// Adds a terminal token to end of derivation
        /// </summary>
        /// <param name="index">Index of the token in terminals list</param>
        /// <param name="line">Line the token have been parsed</param>
        public void AddTerminal(int index, int line)
        {
            Derivation.Add(new Terminal(index, line));
        }

        /// <summary>
        /// Adds a non terminal token to end of derivation
        /// </summary>
        /// <param name="index">Index of the token in non terminals list</param>
        /// <param name="line">Line the token have been parsed</param>
        public void AddNonTerminal(int index, int line)
        {
            Derivation.Add(new NonTerminal(index, line));
        }

        /// <summary>
        /// Gets a string representing this rule
        /// </summary>
        /// <returns>String with the derivated symbol and derivation of the rule</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Token == null ? "<<undefined>>" : nonterminals[Token.Index].Name);
            sb.Append(" -> "); sb.Append(Derivation.ToString());
            return sb.ToString();
        }
    }
}
