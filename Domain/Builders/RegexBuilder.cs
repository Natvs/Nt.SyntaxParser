using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Builders
{
    /// <summary>
    /// A builder class for configurating a <see cref="RegularExpression"/> instance
    /// </summary>
    /// <param name="regex">The regular expression this builder configures</param>
    public class RegexBuilder(RegularExpression regex)
    {
        private readonly RegularExpression _regex = regex;

        /// <summary>
        /// Set the non-terminal to derive the regular expression from.
        /// </summary>
        /// <param name="nt">Non-terminal to set</param>
        /// <returns>This instance of <see cref="RegexBuilder"/> for method chaining</returns>
        /// <exception cref="UnregisteredNonTerminalException">Thrown if the <paramref name="nt"/> is not registered in the grammar.</exception>
        public RegexBuilder SetToken(NonTerminal nt)
        {
            if (!_regex.Grammar.NonTerminals.Contains(nt.Name)) throw new UnregisteredNonTerminalException(nt.Name, nt.Line);
            _regex.Token = nt;
            return this;
        }

        /// <summary>
        /// Add a pattern to the current pattern of the regular expression.
        /// </summary>
        /// <param name="pattern">Pattern to add</param>
        /// <returns>This instance of <see cref="RegexBuilder"/> for method chaining</returns>
        public RegexBuilder AddSymbols(string pattern)
        {
            _regex.Pattern += pattern;
            return this;
        }

        /// <summary>
        /// Builds and returns the configured regular expression instance.
        /// </summary>
        /// <returns>The <see cref="RegularExpression"/> object representing the constructed regular expression.</returns>
        public RegularExpression Build()
        {
            return _regex;
        }
    }
}
