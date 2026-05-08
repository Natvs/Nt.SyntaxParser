using Nt.Syntax.Structures;

namespace Nt.Syntax.Builders
{
    public static class BuilderExtensions
    {
        /// <summary>
        /// Get a builder for the grammar. This is useful for chaining methods.
        /// </summary>
        /// <param name="grammar">The <see cref="Grammar"/> instance to get a builder for.</param>
        /// <returns>A <see cref="GrammarBuilder"/> instance for the specified grammar.</returns>
        public static GrammarBuilder GetBuilder(this Grammar grammar)
        {
            return new GrammarBuilder(grammar);
        }

        /// <summary>
        /// Get a builder for the rule. This is useful for chaining methods.
        /// </summary>
        /// <param name="rule">The <see cref="Rule"/> instance to get a builder for.</param>
        /// <returns>A <see cref="RuleBuilder"/> instance for the specified rule.</returns>
        public static RuleBuilder GetBuilder(this Rule rule)
        {
            return new RuleBuilder(rule);
        }

        /// <summary>
        /// Get a builder for the regular expression. This is useful for chaining methods.
        /// </summary>
        /// <param name="regex">The <see cref="RegularExpression"/> instance to get a builder for.</param>
        /// <returns>A <see cref="RegexBuilder"/> instance for the specified regular expression.</returns>
        public static RegexBuilder GetBuilder(this RegularExpression regex)
        {
            return new RegexBuilder(regex);
        }
    }
}
