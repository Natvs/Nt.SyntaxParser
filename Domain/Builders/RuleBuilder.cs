using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Builders
{
    /// <summary>
    /// A builder class for configuring a <see cref="Rule"/> instance.
    /// </summary>
    /// <param name="rule">The rule this builder configures</param>
    public class RuleBuilder(Rule rule)
    {
        private readonly Rule _rule = rule;

        /// <summary>
        /// Sets the token for the rule using the specified non-terminal symbol.
        /// </summary>
        /// <param name="nt">The non-terminal symbol the rule must derive.</param>
        /// <returns>The current instance of <see cref="RuleBuilder"/> for method chaining.</returns>
        /// <exception cref="UnregisteredNonTerminalException">Thrown if <paramref name="nt"/> is not registered in the grammar's collection of non-terminals.</exception>
        public RuleBuilder SetAxiom(string name, int line)
        {
            if (_rule.Grammar.NonTerminals.TryGet((t) => t.Name == name, out var nonterminal))
            {
                _rule.Axiom = nonterminal.Clone(line);
                return this;
            }
            throw new AxiomException(name, line);
        }

        /// <summary>
        /// Clears the current derivation sequence of the rule.
        /// </summary>
        /// <returns>The current instance of <see cref="RuleBuilder"/> for method chaining.</returns>
        public RuleBuilder Clear()
        {
            _rule.Derivation.Clear();
            return this;
        }

        /// <summary>
        /// Add a <see cref="GrammarToken"/> to the rule's derivation sequence.
        /// </summary>
        /// <param name="token">Name of the token to add.</param>
        /// <param name="line">Line of the token</param>
        /// <returns>The current instance of <see cref="RuleBuilder"/> for method chaining.</returns>
        /// <exception cref="UnknownSymbolException">Thrown if <paramref name="token"/> is neither registered as a terminal or non terminal.</exception>
        public RuleBuilder Add(string token, int line)
        {
            if (_rule.Grammar.Terminals.TryGet((t) => t.Name == token, out var terminal))
            {
                _rule.Derivation.Add(terminal.Clone(line));
                return this;
            }
            if (_rule.Grammar.NonTerminals.TryGet((t) => t.Name == token, out var nonTerminal))
            {
                _rule.Derivation.Add(nonTerminal.Clone(line));
                return this;
            }
            throw new UnknownSymbolException(token, line);
        }
        
        /// <summary>
        /// Insert a <see cref="GrammarToken"/> at the specified position in the rule's derivation sequence.
        /// </summary>
        /// <param name="position">The position at which to insert the token.</param>
        /// <param name="token">Grammar token to insert. Can be either a terminal or non-terminal symbol.</param>
        /// <returns>The current instance of <see cref="RuleBuilder"/> for method chaining.</returns>
        /// <exception cref="UnknownSymbolException">Thrown if <paramref name="token"/> is neither registered as a terminal or non terminal.</exception>
        public RuleBuilder Insert(int position, string token, int line)
        {
            if (_rule.Grammar.Terminals.TryGet((t) => t.Name == token, out var terminal))
            {
                _rule.Derivation.Insert(position, terminal.Clone(line));
                return this;
            }
            if (_rule.Grammar.NonTerminals.TryGet((t) => t.Name == token, out var nonTerminal))
            {
                _rule.Derivation.Insert(position, nonTerminal.Clone(line));
                return this;
            }
            throw new UnknownSymbolException(token, line);
        }

        /// <summary>
        /// Build and return the configured rule instance.
        /// </summary>
        /// <returns>The instance of <see cref="Rule"/> representing the constructed rule.</returns>
        public Rule Build()
        {
            return _rule;
        }
    }
}
