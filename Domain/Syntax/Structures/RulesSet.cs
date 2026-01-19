using System.Collections;
using System.Text;

namespace Nt.Syntax.Structures
{
    public class RulesSet() : IEnumerable<Rule>
    {
        #region Private

        private HashSet<Rule> Rules { get; } = [];

        #endregion

        #region Internal

        /// <summary>
        /// Adds the specified rule to the collection of rules.
        /// </summary>
        /// <param name="rule">The rule to add to the collection.</param>
        internal void Add(Rule rule)
        {
            Rules.Add(rule);
        }

        #endregion

        #region Public

        public int Count { get => Rules.Count; }

        public IEnumerator<Rule> GetEnumerator()
        {
            foreach (var rule in Rules)
            {
                yield return rule;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns a string that represents the collection in a comma-separated list enclosed in braces.
        /// </summary>
        /// <returns>A string containing the elements of the collection, separated by commas and enclosed in curly braces.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('{').Append(string.Join(",", this)).Append('}');
            return sb.ToString();
        }

        #endregion
    }
}