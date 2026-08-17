using Nt.Syntax.Exceptions;
using Nt.Syntax.Exportation;
using System.Collections;

namespace Nt.Syntax.Structures
{
    public class RulesSet() : IEnumerable<Rule>
    {
        #region Implementation of IEnumerable

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

        #endregion

        #region Private

        private HashSet<Rule> Rules { get; } = [];

        #endregion

        #region Public

        /// <summary>
        /// Add the specified rule to the collection of rules.
        /// </summary>
        /// <param name="rule">The rule to add to the collection.</param>
        internal void Add(Rule rule)
        {
            Rules.Add(rule);
        }

        /// <summary>
        /// Add a range of rules to the collection of rules.
        /// </summary>
        /// <param name="rules">The collection of <see cref="Rule"> objects to add</param>
        internal void AddRange(IEnumerable<Rule> rules)
        {
            foreach (var rule in rules)
            {
                Add(rule);
            }
        }

        /// <summary>
        /// Remove the specified rule from the collection.
        /// </summary>
        /// <param name="rule">The rule to remove from the collection. Cannot be null.</param>
        /// <exception cref="RuleNotFoundException">Thrown if the specified rule does not exist in the collection.</exception>
        internal void Remove(Rule rule)
        {
            if (!Rules.Remove(rule)) throw new RuleNotFoundException(rule, $"Rule {rule} not found in collection of rules");
        }

        /// <summary>
        /// Get a rule at a specified index
        /// </summary>
        /// <param name="index">Index of the rule to get</param>
        /// <returns>The rule at the specified index</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of range</exception>
        public Rule Get(int index)
        {
            if (index < 0 || index >= Rules.Count) throw new IndexOutOfRangeException($"Index {index} is out of range for collection of rules with count {Rules.Count}");
            return Rules.ElementAt(index);
        }

        /// <summary>
        /// Returns a string that represents the collection in a comma-separated list enclosed in braces.
        /// </summary>
        /// <returns>A string containing the elements of the collection, separated by commas and enclosed in curly braces.</returns>
        public override string ToString()
        {
            return this.ToString(ExportationMode.Original); 
        }

        #endregion
    }
}