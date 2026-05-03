using Nt.Parser.Symbols;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Syntax.Builders
{
    /// <summary>
    /// A builder class for configuring a <see cref="Grammar"/> instance.
    /// </summary>
    /// <param name="grammar">The <see cref="Grammar"/> instance to configure.</param>
    public class GrammarBuilder(Grammar grammar)
    {
        private readonly Grammar _grammar = grammar;

        /// <summary>
        /// Add a terminal symbol to the grammar.
        /// </summary>
        /// <param name="name">Name of the terminal to add</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="RegisteredTerminalException">Thrown if the symbol is already registered as a terminal.</exception>
        /// <exception cref="RegisteredNonTerminalException">Thrown if the symbol is already registered as a non-terminal.</exception>
        public GrammarBuilder AddTerminal(string name)
        {
            if (_grammar.Terminals.Contains(name)) throw new RegisteredTerminalException(name);
            if (_grammar.NonTerminals.Contains(name)) throw new RegisteredNonTerminalException(name);
            _grammar.Terminals.Add(name);
            return this;
        }

        /// <summary>
        /// Add a non terminal symbol to the grammar.
        /// </summary>
        /// <param name="name">Name of the non-terminal to add</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="RegisteredNonTerminalException">Thrown if the symbol is already registered as a non-terminal.</exception>
        /// <exception cref="RegisteredTerminalException">Thrown if the symbol is already registered as a terminal.</exception>
        public GrammarBuilder AddNonTerminal(string name)
        {
            if (_grammar.NonTerminals.Contains(name)) throw new RegisteredNonTerminalException(name);
            if (_grammar.Terminals.Contains(name)) throw new RegisteredTerminalException(name);
            _grammar.NonTerminals.Add(name);
            return this;
        }

        /// <summary>
        /// Add multiple terminal symbols to the grammar.
        /// </summary>
        /// <param name="names">Collection of terminal names to add</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        public GrammarBuilder AddTerminals(ICollection<string> names)
        {
            foreach (var name in names)
            {
                _grammar.Terminals.Add(name);
            }
            return this;
        }

        /// <summary>
        /// Add multiple non-terminal symbols to the grammar.
        /// </summary>
        /// <param name="names">Collection of non-terminal names to add</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        public GrammarBuilder AddNonTerminals(ICollection<string> names)
        {
            foreach (var name in names)
            {
                _grammar.NonTerminals.Add(name);
            }
            return this;
        }

        /// <summary>
        /// Sets the axiom (start symbol) of the grammar to the specified non-terminal.
        /// </summary>
        /// <param name="name">The name of the non-terminal to set as the axiom. Cannot be null or empty. Must refer to a registered non-terminal.</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> to allow method chaining.</returns>
        /// <exception cref="UnregisteredNonTerminalException">Thrown if the specified non-terminal name is not registered in the grammar.</exception>
        public GrammarBuilder SetAxiom(string name)
        {
            try
            {
                var symbol = _grammar.NonTerminals.Get(name);
                if (!_grammar.NonTerminals.Contains(symbol.Name)) throw new UnregisteredNonTerminalException(name, -1);
                _grammar.Axiom = new NonTerminal(symbol, -1);
            }
            catch (KeyNotFoundException)
            {
                throw new UnregisteredNonTerminalException(name, -1);
            }
            return this;
        }

        /// <summary>
        /// Sets the axiom (start symbol) of the grammar to the specified non-terminal symbol.
        /// </summary>
        /// <param name="symbol">The symbol to set as the axiom. Must be a non-terminal symbol defined in the grammar.</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> to allow method chaining.</returns>
        /// <exception cref="UnregisteredNonTerminalException">Thrown if <paramref name="symbol"/> is not a non-terminal symbol in the grammar.</exception>
        public GrammarBuilder SetAxiom(Symbol symbol)
        {
            try
            {
                if (!_grammar.NonTerminals.Contains(symbol.Name)) throw new UnregisteredNonTerminalException(symbol.Name, -1);
                _grammar.Axiom = new NonTerminal(symbol, -1);
            }
            catch (KeyNotFoundException)
            {
                throw new UnregisteredNonTerminalException(symbol.Name, -1);
            }
            return this;
        }

        /// <summary>
        /// Sets the axiom (start symbol) of the grammar to the specified non-terminal token.
        /// </summary>
        /// <param name="token">The non-terminal token to use as the axiom of the grammar. Cannot be null.</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="UnregisteredNonTerminalException">Thrown if <paramref name="token"/> is not a non-terminal symbol in the grammar.</exception>
        public GrammarBuilder SetAxiom(NonTerminal token)
        {
            try
            {
                if (!_grammar.NonTerminals.Contains(token.Name)) throw new UnregisteredNonTerminalException(token.Name, token.Line);
                _grammar.Axiom = token;
            }
            catch (KeyNotFoundException)
            {
                throw new UnregisteredNonTerminalException(token.Name, token.Line);
            }
            return this;
        }

        /// <summary>
        /// Add a rule to the grammar
        /// </summary>
        /// <param name="rule">Rule to add</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        public GrammarBuilder Add(Rule rule)
        {
            _grammar.Rules.Add(rule);
            return this;
        }

        /// <summary>
        /// Add a regular expression to the grammar
        /// </summary>
        /// <param name="regex">Regular expression to add</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        public GrammarBuilder Add(RegularExpression regex)
        {
            _grammar.RegularExpressions.Add(regex);
            return this;
        }

        /// <summary>
        /// Remove the specified rule from the grammar.
        /// </summary>
        /// <param name="rule">Rule to remove</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="RuleNotFoundException">Thrown if the specified rule is not found in the grammar.</exception>
        public GrammarBuilder Remove(Rule rule)
        {
            _grammar.Rules.Remove(rule);
            return this;
        }

        /// <summary>
        /// Removes the specified regular expression from the grammar.
        /// </summary>
        /// <param name="regex">The regular expression to remove from the grammar. Cannot be null.</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        public GrammarBuilder Remove(RegularExpression regex)
        {
            _grammar.RegularExpressions.Remove(regex);
            return this;
        }

        /// <summary>
        /// Builds and returns the configured Grammar instance.
        /// </summary>
        /// <returns>The <see cref="Grammar"/> instance this builder refers to.</returns>
        public Grammar Build()
        {
            return _grammar;
        }

    }
}
