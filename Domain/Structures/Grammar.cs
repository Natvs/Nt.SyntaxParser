using Nt.Parser.Structures;
using Nt.Parser.Symbols;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Exportation;

namespace Nt.Syntax.Structures
{

    /// <summary>
    /// Represents datas of a language grammar
    /// </summary>
    public class Grammar
    {
        public Grammar()
        {
            var configuration = SyntaxParserConfig.GetInstance();
            Terminals = new SymbolsList(configuration.SymbolFactory);
            NonTerminals = new SymbolsList(configuration.SymbolFactory);
        }

        #region Internal

        /// <summary>
        /// Removes escape characters from the specified string, returning a new string with escape sequences processed.
        /// </summary>
        /// <param name="content">The input string from which escape characters should be removed.</param>
        /// <returns>A new string with escape characters removed from the input.</returns>
        internal string RemoveEscapeCharacter(string content)
        {
            string new_content = "";
            bool escape = false;
            foreach (var c in content)
            {
                if (c == EscapeCharacter && !escape)
                {
                    escape = true;
                    continue;
                }
                new_content += c;
                escape = false;
            }
            return new_content;
        }

        /// <summary>
        /// Converts a parsed token to the corresponding grammar token, identifying it as either a terminal or
        /// nonterminal symbol.
        /// </summary>
        /// <param name="token">The parsed token to convert. Must contain a valid symbol name.</param>
        /// <returns>A <see cref="GrammarToken"/> representing the terminal or nonterminal symbol corresponding to the parsed token.</returns>
        /// <exception cref="UnknownSymbolException">Thrown if the symbol name in <paramref name="token"/> does not match any known terminal or nonterminal symbol.</exception>
        internal GrammarToken ParseToGrammarToken(ParsedToken token)
        {
            // Remove escape characters
            string new_content = RemoveEscapeCharacter(token.Symbol.Name);

            // Check if terminal or non terminal
            if (Terminals.Contains(new_content))
            {
                return new Terminal(Terminals.Get(new_content), token.Line);
            }
            else if (NonTerminals.Contains(new_content))
            {
                return new NonTerminal(NonTerminals.Get(new_content), token.Line);
            }
            throw new UnknownSymbolException(new_content, token.Line);
        }

        #endregion

        #region Public

        public char EscapeCharacter { get; private set; } = '\'';
        public SymbolsList Terminals { get; }
        public SymbolsList NonTerminals { get; }
        public NonTerminal? Axiom { get; internal set; } = null;
        public RulesSet Rules { get; } = [];
        public RegexSet RegularExpressions { get; } = [];

        /// <summary>
        /// Sets the character used to escape special characters in parsing or formatting operations.
        /// </summary>
        /// <param name="c">The character to use as the escape character.</param>
        public void SetEscapeChar(char c)
        {
            EscapeCharacter = c;
        }

        /// <summary>
        /// Gets a string containing datas about the grammar
        /// </summary>
        /// <returns>A string representing this grammar</returns>
        public override string ToString()
        {
           return this.ToString(ExportationMode.Original);
        }

        #endregion
    }
}
