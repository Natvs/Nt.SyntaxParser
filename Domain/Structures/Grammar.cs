using System.Text;
using Nt.Parser.Structures;
using Nt.Parser.Symbols;
using Nt.Syntax.Exceptions;

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
        public NonTerminal? Axiom { get; private set; } = null;
        public RulesSet Rules { get; } = [];
        public RegExpSet RegularExpressions { get; } = [];

        /// <summary>
        /// Sets the character used to escape special characters in parsing or formatting operations.
        /// </summary>
        /// <param name="c">The character to use as the escape character.</param>
        public void SetEscapeCharacter(char c)
        {
            EscapeCharacter = c;
        }

        /// <summary>
        /// Adds a new terminal to the terminals list of this grammar
        /// </summary>
        /// <param name="name">Name of the terminal to add</param>
        /// <exception cref="RegisteredTerminalException">The terminal may already exists in the list of terminals</exception>
        /// <exception cref="RegisteredNonTerminalException">The non terminal may already exists in the list of non terminals</exception>
        public ISymbol AddTerminal(string name)
        {
            if (Terminals.Contains(name)) throw new RegisteredTerminalException(name);
            if (NonTerminals.Contains(name)) throw new RegisteredNonTerminalException(name);
            return Terminals.Add(name);
        }

        /// <summary>
        /// Adds a new non terminal to the non terminals list of this grammar
        /// </summary>
        /// <param name="name">Name of the non terminal to add</param>
        /// <exception cref="RegisteredNonTerminalException">The non terminal may already exists in the list of non terminals</exception>
        /// <exception cref="RegisteredNonTerminalException">The non terminal may already exists in the list of non terminals</exception>
        public ISymbol AddNonTerminal(string name)
        {
            if (NonTerminals.Contains(name)) throw new RegisteredNonTerminalException(name);
            if (Terminals.Contains(name)) throw new RegisteredTerminalException(name);
            return NonTerminals.Add(name);
        }

        /// <summary>
        /// Removes the specified terminal symbol from the collection of terminals.
        /// </summary>
        /// <param name="symbol">The terminal symbol to remove from the collection. Cannot be null.</param>
        /// <exception cref="KeyNotFoundException">Thrown when symbol does not exists in collection of terminals.</exception>
        public void RemoveTerminal(ISymbol symbol)
        {
            Terminals.Remove(symbol);
        }

        /// <summary>
        /// Removes the specified non-terminal symbol from the collection of non-terminals.
        /// </summary>
        /// <param name="symbol">The non-terminal symbol to remove from the collection. Cannot be null.</param>
        /// <exception cref="KeyNotFoundException">Thrown when symbol does not exists in collection of non-terminals.</exception>"
        public void RemoveNonTerminal(ISymbol symbol)
        {
            NonTerminals.Remove(symbol);
        }

        /// <summary>
        /// Sets the axiom of this grammar
        /// </summary>
        /// <param name="name">Non terminal to set for axiom</param>
        /// <exception cref="UnregisteredNonTerminalException">The axiom might not exist in the non terminals list of this grammar</exception>
        public void SetAxiom(NonTerminal token)
        {
            if (!NonTerminals.Contains(token.Name)) throw new UnregisteredNonTerminalException(token.Name, token.Line);
            Axiom = token;
        }

        /// <summary>
        /// Adds a new grammar rule for the specified non-terminal symbol at the given source line.
        /// </summary>
        /// <param name="nonTerminal">The non-terminal symbol for which the rule is to be created. Must be declared in the grammar.</param>
        /// <param name="line">The line number in the source where the rule is defined. Used for error reporting and diagnostics.</param>
        /// <returns>A Rule instance representing the newly added grammar rule.</returns>
        /// <exception cref="UnregisteredNonTerminalException">Thrown if the specified non-terminal symbol has not been declared in the grammar.</exception>
        public Rule AddRule(NonTerminal nt)
        {
            var rule = new Rule(this);
            Rules.Add(rule);
            rule.SetToken(nt);
            return rule;
        }

        /// <summary>
        /// Removes the specified rule from the collection of rules.
        /// </summary>
        /// <param name="rule">The rule to remove from the collection.</param>
        /// <exception cref="RuleNotFoundException">The rule is not part of the grammar</exception>
        public void Remove(Rule rule)
        {
            Rules.Remove(rule);
        }

        /// <summary>
        /// Removes the specified regular expression from the collection.
        /// </summary>
        /// <param name="regex">The regular expression to remove from the collection. Cannot be null.</param>
        /// <exception cref="RegexNotFoundException">The regular expression is not part of the grammar</exception>
        public void Remove(RegularExpression regex)
        {
            RegularExpressions.Remove(regex);
        }

        /// <summary>
        /// Adds a new regular expression associated with the specified non-terminal symbol at the given source line.
        /// </summary>
        /// <param name="nonTerminal">The non-terminal symbol to associate with the new regular expression. Must be declared prior to calling this
        /// method.</param>
        /// <param name="line">The line number in the source where the regular expression is defined.</param>
        /// <returns>A RegularExpression instance representing the newly added regular expression.</returns>
        /// <exception cref="UnregisteredNonTerminalException">Thrown if the specified non-terminal symbol has not been declared.</exception>
        public RegularExpression AddRegex(NonTerminal nt)
        {
            var regex = new RegularExpression(this);
            RegularExpressions.Add(regex);
            regex.SetToken(nt);
            return regex;
        }

        /// <summary>
        /// Gets a string containing datas about the grammar
        /// </summary>
        /// <returns>A string representing this grammar</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Terminals: ").Append(Terminals.ToString()).Append('\n');
            sb.Append("Non terminals: ").Append(NonTerminals.ToString()).Append('\n');
            if (Axiom != null) sb.Append("Axiom: ").Append(Axiom.Name).Append('\n');

            if (Rules.Count > 0) sb.Append("\nRules\n");
            foreach (Rule rule in Rules)
            {
                sb.Append("  ").Append(rule.ToString()).Append('\n');
            }

            if (RegularExpressions.Count > 0) sb.Append("\nRegular expressions\n");
            foreach (RegularExpression regularExpression in RegularExpressions)
            {
                sb.Append("  ").Append(regularExpression.ToString()).Append('\n');
            }

            return sb.ToString();
        }

        #endregion
    }
}
