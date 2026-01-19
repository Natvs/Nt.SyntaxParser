using System.Text;
using Nt.Parser.Structures;
using Nt.Syntax.Exceptions;

namespace Nt.Syntax.Structures
{

    /// <summary>
    /// Represents datas of a language grammar
    /// </summary>
    public class Grammar
    {
        public char EscapeCharacter { get; internal set; } = '\'';

        public SymbolsList Terminals { get; } = new();
        public SymbolsList NonTerminals { get; } = new();
        public NonTerminal? Axiom { get; internal set; } = null;

        public RulesSet Rules { get; } = [];
        public RegExpSet RegularExpressions { get; } = [];

        #region Public Methods

        /// <summary>
        /// Adds a new terminal to the terminals list of this grammar
        /// </summary>
        /// <param name="name">Name of the terminal to add</param>
        /// <exception cref="RegisteredTerminalException">The terminal may already exists in the list of terminals</exception>
        internal void AddTerminal(string name)
        {
            if (Terminals.Contains(name)) throw new RegisteredTerminalException(name);
            if (NonTerminals.Contains(name)) throw new RegisteredNonTerminalException(name);
            Terminals.Add(name);
        }

        /// <summary>
        /// Adds a new non terminal to the non terminals list of this grammar
        /// </summary>
        /// <param name="name">Name of the non terminal to add</param>
        /// <exception cref="RegisteredNonTerminalException">The non terminal may already exists in the list of non terminals</exception>
        internal void AddNonTerminal(string name)
        {
            if (NonTerminals.Contains(name)) throw new RegisteredNonTerminalException(name);
            if (Terminals.Contains(name)) throw new RegisteredTerminalException(name);
            NonTerminals.Add(name);
        }

        /// <summary>
        /// Sets the axiom of this grammar
        /// </summary>
        /// <param name="name">Name of the new axiom</param>
        /// <exception cref="NotDeclaredNonTerminalException">The axiom might not exist in the non terminals list of this grammar</exception>
        internal void SetAxiom(NonTerminal token)
        {
            Axiom = token;
        }

        internal Rule AddRule(Symbol nonTerminal, int line)
        {
            var rule = new Rule(this);
            Rules.Add(rule);
            try
            {
                rule.SetToken(new(nonTerminal, line));
                return rule;
            }
            catch (KeyNotFoundException) { throw new NotDeclaredNonTerminalException(nonTerminal.Name, line); }
        }
        internal RegularExpression AddRegularExpression(Symbol nonTerminal, int line)
        {
            var regex = new RegularExpression(this);
            RegularExpressions.Add(regex);
            try
            {
                regex.SetToken(new(nonTerminal, line));
                return regex;
            }
            catch (KeyNotFoundException) { throw new NotDeclaredNonTerminalException(nonTerminal.Name, line); }
        }

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
    }
}
