using Nt.Parser.Symbols;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using System.Diagnostics.CodeAnalysis;

namespace Nt.Syntax.Builders
{
    /// <summary>
    /// A builder class for configuring a <see cref="Grammar"/> instance.
    /// </summary>
    /// <param name="grammar">The <see cref="Grammar"/> instance to configure.</param>
    public class GrammarBuilder(Grammar grammar)
    {
        private readonly Grammar _grammar = grammar;

        private readonly ISymbolFactory factory = SyntaxParserConfig.GetConfig().SymbolFactory;

        // Terminals and non terminals

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
            _grammar.Terminals.Add(new Terminal(factory.Create(name), line));
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
            _grammar.NonTerminals.Add(new NonTerminal(factory.Create(name), line));
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
                _grammar.Terminals.Add(new Terminal(factory.Create(name), -1));
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
                _grammar.NonTerminals.Add(new NonTerminal(factory.Create(name), -1));
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

        // Axiom

        /// <summary>
        /// Sets the axiom (start symbol) of the grammar to the specified non-terminal token.
        /// </summary>
        /// <param name="token">The non-terminal token to use as the axiom of the grammar. Cannot be null.</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="AxiomException">Thrown if the token is not a non-terminal symbol in the grammar.</exception>
        public GrammarBuilder SetAxiom(string name, int line)
        {
            if (_grammar.NonTerminals.TryGet((t) => t.Symbol.Name == name, out var nonterminal))
            {
                _grammar.Axiom = nonterminal.Clone(line);
                return this;
            }
            throw new AxiomException(name, line);          
        }

        // Rules

        /// <summary>
        /// Add a new rule to the grammar
        /// </summary>
        /// <param name="axiom">Axiom of the new rule</param>
        /// <param name="line">Line of the new rule</param>
        /// <param name="rule">The newly created rule</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="AxiomException">Thrown if the axiom is not a non-terminal symbol in the grammar.</exception>
        public GrammarBuilder AddRule(string axiom, int line, [NotNull] out Rule rule)
        {
            rule = new Rule(_grammar).GetBuilder().SetAxiom(axiom, line).Build();
            _grammar.Rules.Add(rule);
            return this;
            throw new AxiomException(axiom, line);
        }

        /// <summary>
        /// Remove a rule from the grammar.
        /// </summary>
        /// <param name="rule">Rule to remove</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the rule is not found in the grammar.</exception>
        public GrammarBuilder Remove(Rule rule)
        {
            _grammar.Rules.Remove(rule);
            return this;
        }

        // Regex

        /// <summary>
        /// Add a new regular expression to the grammar
        /// </summary>
        /// <param name="pattern">Pattern of the new regular expression</param>
        /// <param name="line">Line of the new regular expression</param>
        /// <param name="regex">The newly created regular expression</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="AxiomException">Thrown if the pattern is not a non-terminal symbol in the grammar.</exception>
        public GrammarBuilder AddRegularExpression(string pattern, int line, [NotNull] out RegularExpression regex)
        {
            if (_grammar.NonTerminals.TryGet((t) => t.Symbol.Name == pattern, out var nonterminal))
            {
                regex = new RegularExpression(_grammar).GetBuilder().SetAxiom(pattern, line).Build();
                _grammar.RegularExpressions.Add(regex);
                return this;
            }
            throw new AxiomException(pattern, line);
        }
        
        /// <summary>
        /// Remove a regular expression from the grammar.
        /// </summary>
        /// <param name="regex">Regular expression to remove</param>
        /// <returns>The current instance of <see cref="GrammarBuilder"/> for method chaining.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the regular expression is not found in the grammar.</exception>
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
