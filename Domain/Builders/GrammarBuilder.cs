using Nt.Parser.Symbols;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

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
        /// <param name="line">The line number of the symbol in the source code. Defaults to -1.</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="RegisteredTerminalException">Thrown if the symbol is already registered as a terminal.</exception>
        /// <exception cref="RegisteredNonTerminalException">Thrown if the symbol is already registered as a non-terminal.</exception>
        public GrammarBuilder AddTerminal(string name, int line = -1)
        {
            if (_grammar.Terminals.Contains((t) => t.Symbol.Name == name)) throw new RegisteredTerminalException(name);
            if (_grammar.NonTerminals.Contains((t) => t.Symbol.Name == name)) throw new RegisteredNonTerminalException(name);
            _grammar.Terminals.Add(new Terminal(new Symbol(name), line));
            return this;
        }

        /// <summary>
        /// Add a non terminal symbol to the grammar.
        /// </summary>
        /// <param name="name">Name of the non-terminal to add</param>
        /// <param name="line">The line number of the symbol in the source code. Defaults to -1.</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="RegisteredNonTerminalException">Thrown if the symbol is already registered as a non-terminal.</exception>
        /// <exception cref="RegisteredTerminalException">Thrown if the symbol is already registered as a terminal.</exception>
        public GrammarBuilder AddNonTerminal(string name, int line = -1)
        {
            if (_grammar.NonTerminals.Contains((t) => t.Symbol.Name == name)) throw new RegisteredNonTerminalException(name);
            if (_grammar.Terminals.Contains((t) => t.Symbol.Name == name)) throw new RegisteredTerminalException(name);
            _grammar.NonTerminals.Add(new NonTerminal(new Symbol(name), line));
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
                if (_grammar.NonTerminals.Contains((t) => t.Symbol.Name == name)) throw new RegisteredNonTerminalException(name);
                if (_grammar.Terminals.Contains((t) => t.Symbol.Name == name)) throw new RegisteredTerminalException(name);
                _grammar.Terminals.Add(new Terminal(new Symbol(name), -1));
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
                _grammar.NonTerminals.Add(new NonTerminal(new Symbol(name), -1));
            }
            return this;
        }

        /// <summary>
        /// Remove a terminal symbol from the grammar by its name.
        /// </summary>
        /// <param name="name">Name of the terminal to remove.</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="UnregisteredTerminalException">Thrown if the specified terminal name is not registered in the grammar.</exception>
        public GrammarBuilder RemoveTerminal(string name)
        {
            if (!_grammar.Terminals.Contains((t) => t.Symbol.Name == name)) throw new UnregisteredTerminalException(name, -1);
            _grammar.Terminals.Remove((t) => t.Symbol.Name == name);
            return this;
        }

        /// <summary>
        /// Removes a non-terminal from the grammar  by its name.
        /// </summary>
        /// <param name="name">Name of the non-terminal symbol to remove.</param>
        /// <returns>The current instance of<see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="UnregisteredNonTerminalException">Thrown if the specifed non-terminal name is not registered in the grammar.</exception>
        public GrammarBuilder RemoveNonTerminal(string name)
        {
            if (!_grammar.NonTerminals.Contains((t) => t.Symbol.Name == name)) throw new UnregisteredNonTerminalException(name, -1);
            _grammar.NonTerminals.Remove((t) => t.Symbol.Name == name);
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
            if (_grammar.NonTerminals.TryGet((t) => t.Symbol.Name == name, out var nonterminal))
            {
                _grammar.Axiom = new NonTerminal(nonterminal.Symbol, -1);
            }
            throw new UnregisteredNonTerminalException(name, -1);
        }

        /// <summary>
        /// Sets the axiom (start symbol) of the grammar to the specified non-terminal symbol.
        /// </summary>
        /// <param name="symbol">The symbol to set as the axiom. Must be a non-terminal symbol defined in the grammar.</param>
        /// <param name="line">The line number of the symbol in the source code. Defaults to -1.</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> to allow method chaining.</returns>
        /// <exception cref="UnregisteredNonTerminalException">Thrown if <paramref name="symbol"/> is not a non-terminal symbol in the grammar.</exception>
        public GrammarBuilder SetAxiom(Symbol symbol, int line = -1)
        {
            if (_grammar.NonTerminals.TryGet((t) => t.Symbol.Name == symbol.Name, out var nonterminal))
            {
                _grammar.Axiom = new NonTerminal(nonterminal.Symbol, line);
            }
            throw new UnregisteredNonTerminalException(symbol.Name, line);
        }

        /// <summary>
        /// Sets the axiom (start symbol) of the grammar to the specified non-terminal token.
        /// </summary>
        /// <param name="token">The non-terminal token to use as the axiom of the grammar. Cannot be null.</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="UnregisteredNonTerminalException">Thrown if <paramref name="token"/> is not a non-terminal symbol in the grammar.</exception>
        public GrammarBuilder SetAxiom(NonTerminal token)
        {
            if (_grammar.NonTerminals.TryGet((t) => t.Symbol.Name == token.Symbol.Name, out var nonterminal))
            {
                _grammar.Axiom = new NonTerminal(nonterminal.Symbol, token.Line);
                return this;
            }
            throw new UnregisteredNonTerminalException(token.Symbol.Name, token.Line);          
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
