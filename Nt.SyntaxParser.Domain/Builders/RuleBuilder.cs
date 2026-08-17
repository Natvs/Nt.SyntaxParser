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
        public RuleBuilder SetToken(NonTerminal nt)
        {
            if (!_rule.Grammar.NonTerminals.Contains(nt.Name)) throw new UnregisteredNonTerminalException(nt.Name, nt.Line);
            _rule.Token = nt;
            return this;
        }

        /// <summary>
        /// Add a <see cref="GrammarToken"/> to the rule's derivation sequence.
        /// </summary>
        /// <param name="token">Grammar token to add. Can be either a terminal or non-terminal symbol.</param>
        /// <returns>The current instance of <see cref="RuleBuilder"/> for method chaining.</returns>
        /// <exception cref="UnknownSymbolException">Thrown if <paramref name="token"/> is neither registered as a terminal or non terminal.</exception>
        public RuleBuilder Add(GrammarToken token)
        {
            if (!_rule.Grammar.Terminals.Contains(token.Name) && !_rule.Grammar.NonTerminals.Contains(token.Name))
                throw new UnknownSymbolException(token.Name, token.Line);
            _rule.Derivation.Add(token);
            return this;
        }
        
        /// <summary>
        /// Insert a <see cref="GrammarToken"/> at the specified position in the rule's derivation sequence.
        /// </summary>
        /// <param name="position">The position at which to insert the token.</param>
        /// <param name="token">Grammar token to insert. Can be either a terminal or non-terminal symbol.</param>
        /// <returns>The current instance of <see cref="RuleBuilder"/> for method chaining.</returns>
        /// <exception cref="UnknownSymbolException">Thrown if <paramref name="token"/> is neither registered as a terminal or non terminal.</exception>
        public RuleBuilder Insert(int position, GrammarToken token)
        {
            if (!_rule.Grammar.Terminals.Contains(token.Name) && !_rule.Grammar.NonTerminals.Contains(token.Name))
                throw new UnknownSymbolException(token.Name, token.Line);
            _rule.Derivation.Insert(position, token);
            return this;
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
