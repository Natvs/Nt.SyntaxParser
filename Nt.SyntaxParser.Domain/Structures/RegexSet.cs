using Nt.Syntax.Exceptions;
using Nt.Syntax.Exportation;
using System.Collections;
using System.Text;

namespace Nt.Syntax.Structures
{
    public class RegexSet() : IEnumerable<RegularExpression>
    {
        #region Implementation of IEnumerable

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

        #endregion

        #region Private

        private HashSet<RegularExpression> Regexs { get; } = [];

        #endregion

        #region Public

        /// <summary>
        /// Add the specified regular expression to the collection of rules.
        /// </summary>
        /// <param name="rule">The regular expression to add to the collection.</param>
        internal void Add(RegularExpression regex)
        {
            Regexs.Add(regex);
        }

        /// <summary>
        /// Adds the elements of the specified collection of regular expressions to the current collection.
        /// </summary>
        /// <param name="regexs">The collection of <see cref="RegularExpression"/> objects to add.</param>
        internal void AddRange(IEnumerable<RegularExpression> regexs)
        {
            foreach (var regex in regexs)
            {
                Add(regex);
            }
        }

        /// <summary>
        /// Remove the specified regular expression from the collection.
        /// </summary>
        /// <param name="regex">The regular expression to remove from the collection. Cannot be null.</param>
        /// <exception cref="RegexNotFoundException">Thrown if the specified regular expression is not found in the collection.</exception>
        internal void Remove(RegularExpression regex)
        {
            if (!Regexs.Remove(regex)) throw new RegexNotFoundException(regex, $"Regular expression {regex} not found in collection of regular expressions");
        }

        /// <summary>
        /// Get a regular expression at the specified index
        /// </summary>
        /// <param name="index">Index of the regular expression</param>
        /// <returns>The regular expression at the specified index</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of range</exception>
        public RegularExpression Get(int index)
        {
            if (index < 0 || index >= Regexs.Count) throw new IndexOutOfRangeException($"Index {index} is out of range for collection of regular expressions with count {Regexs.Count}");
            return Regexs.ElementAt(index);
        }

        /// <summary>
        /// Returns a string that represents the collection in a comma-separated list enclosed in braces.
        /// </summary>
        /// <returns>A string containing the elements of the collection, separated by commas and enclosed in curly bracets.</returns>
        public override string ToString()
        {
            return this.ToString(ExportationMode.Original);
        }

        #endregion
    }
}