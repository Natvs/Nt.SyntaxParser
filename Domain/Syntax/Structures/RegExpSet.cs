using System.Collections;
using System.Text;

namespace Nt.Syntax.Structures
{
    public class RegExpSet() : IEnumerable<RegularExpression>
    {
        #region Private

        private HashSet<RegularExpression> Regexs { get; } = [];

        #endregion

        #region Internal

        /// <summary>
        /// Adds the specified regular expression to the collection of rules.
        /// </summary>
        /// <param name="rule">The regular expression to add to the collection.</param>
        internal void Add(RegularExpression regex)
        {
            Regexs.Add(regex);
        }

        #endregion

        #region Public

        public int Count { get => Regexs.Count; }

        public IEnumerator<RegularExpression> GetEnumerator()
        {
            foreach (var regex in Regexs)
            {
                yield return regex;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns a string that represents the collection in a comma-separated list enclosed in braces.
        /// </summary>
        /// <returns>A string containing the elements of the collection, separated by commas and enclosed in curly bracets.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('{').Append(string.Join(",", this)).Append('}');
            return sb.ToString();
        }

        #endregion
    }
}