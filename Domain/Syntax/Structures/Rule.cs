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
        /// <summary>
        /// Token that would derive into derivation
        /// </summary>
        public NonTerminal? Token { get; private set; }
        /// <summary>
        /// List of tokens that represent derivation
        /// </summary>
        public Derivation Derivation { get; } = [];


        /// <summary>
        /// Sets the token to derive
        /// </summary>
        /// <param name="symbol">Symbol of the token</param>
        /// <param name="line">Line the token have been parsed</param>
        public void SetToken(NonTerminal nt)
        {
            if (!grammar.NonTerminals.Contains(nt.Name)) throw new NotDeclaredNonTerminalException(nt.Name, nt.Line);
            Token = nt;
        }

        /// <summary>
        /// Adds a derivation token to the current grammar derivation sequence.
        /// </summary>
        /// <param name="token">The token to add to the derivation sequence.</param>
        public void AddDerivationToken(GrammarToken token)
        {
            if (!grammar.Terminals.Contains(token.Name) && !grammar.NonTerminals.Contains(token.Name))
                throw new UnknownSymbolException(token.Name, token.Line);
            Derivation.Add(token);
        }

        /// <summary>
        /// Inserts a derivation token at the specified position in the derivation sequence.
        /// </summary>
        /// <param name="position">The index at which the token should be inserted.</param>
        /// <param name="token">The derivation token to insert.</param>
        public void InsertDerivationToken(int position, GrammarToken token)
        {
            if (!grammar.Terminals.Contains(token.Name) && !grammar.NonTerminals.Contains(token.Name))
                throw new UnknownSymbolException(token.Name, token.Line);
            Derivation.Insert(position, token);
        }

        /// <summary>
        /// Adds a terminal token to end of derivation
        /// </summary>
        /// <param name="symbol">Symbol of the token</param>
        /// <param name="line">Line the token have been parsed</param>
        public void AddTerminal(Terminal t)
        {
            if (!grammar.Terminals.Contains(t.Name)) throw new NotDeclaredTerminalException(t.Name, t.Line);
            Derivation.Add(t);
        }

        /// <summary>
        /// Inserts a terminal token at specified position in derivation
        /// </summary>
        /// <param name="position">Position for inserting the terminal</param>
        /// <param name="symbol">Symbol of the token</param>
        /// <param name="line">Line of the new terminal</param>
        public void InsertTerminal(int position, Terminal t)
        {
            if (!grammar.Terminals.Contains(t.Name)) throw new NotDeclaredTerminalException(t.Name, t.Line);
            Derivation.Insert(position, t);
        }

        /// <summary>
        /// Adds a non terminal token to end of derivation
        /// </summary>
        /// <param name="symbol">Symbol of the token</param>
        /// <param name="line">Line the token have been parsed</param>
        public void AddNonTerminal(NonTerminal nt)
        {
            if (!grammar.NonTerminals.Contains(nt.Name)) throw new NotDeclaredNonTerminalException(nt.Name, nt.Line);
            Derivation.Add(nt);
        }

        /// <summary>
        /// Inserts a non terminal token at specified position in derivation
        /// </summary>
        /// <param name="position">Position for inserting the non terminal</param>
        /// <param name="symbol">Symbol of the token</param>
        /// <param name="line">Line of the new non terminal</param>
        public void InsertNonTerminal(int position, NonTerminal nt)
        {
            if (!grammar.NonTerminals.Contains(nt.Name)) throw new NotDeclaredNonTerminalException(nt.Name, nt.Line);
            Derivation.Insert(position, nt);
        }

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
