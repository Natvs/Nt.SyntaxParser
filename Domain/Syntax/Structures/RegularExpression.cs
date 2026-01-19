using Nt.Syntax.Exceptions;
using System.Text;

namespace Nt.Syntax.Structures
{
    public class RegularExpression(Grammar grammar)
    {
        public NonTerminal? Token { get; private set; }
        public string Pattern { get; private set; } = "";

        /// <summary>
        /// Sets the token of this regular expression to the specified non-terminal symbol.
        /// </summary>
        /// <param name="nt">The non-terminal symbol to assign as the current token. The non-terminal must be declared in the grammar;</param>
        /// <exception cref="NotDeclaredNonTerminalException">Thrown if the specified non-terminal symbol is not declared in the grammar.</exception>
        public void SetToken(NonTerminal nt)
        {
            if (!grammar.NonTerminals.Contains(nt.Name)) throw new NotDeclaredNonTerminalException(nt.Name, nt.Line);
            Token = nt;
        }

        /// <summary>
        /// Appends the specified symbols to the current pattern.
        /// </summary>
        /// <param name="symbols">A string containing the symbols to add to the pattern. Cannot be null.</param>
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

            if (Token != null) sb.Append(Token.Symbol.Name);
            else sb.Append("<<undefined>>");

            sb.Append(" = ").Append(Pattern.Equals("") ? "<<empty>>" : Pattern);
            return sb.ToString();
        }
    }
}
