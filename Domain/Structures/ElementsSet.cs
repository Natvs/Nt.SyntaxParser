using Nt.Syntax.Exceptions;
using Nt.Syntax.Exportation;
using Nt.Syntax.Structures;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace Nt.Syntax.Structures
{
    public class ElementsSet<T>() : IEnumerable<T>
    {
        #region Implementation of IEnumerable

        private HashSet<T> Elements { get; } = [];

        public int Count { get => Elements.Count; }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var element in Elements)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion


        /// <summary>
        /// Add the specified element to the collection.
        /// </summary>
        /// <param name="element">The element to add to the collection.</param>
        internal void Add(T element)
        {
            Elements.Add(element);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the current collection.
        /// </summary>
        /// <param name="elements">The collection of elements to add.</param>
        internal void AddRange(IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                Add(element);
            }
        }

        /// <summary>
        /// Remove the specified element from the collection.
        /// </summary>
        /// <param name="regex">The element to remove from the collection. Cannot be null.</param>
        /// <exception cref="InternalException">Thrown if the specified element is not found in the collection.</exception>
        internal void Remove(T element)
        {
            if (!Elements.Remove(element)) throw new InternalException($"Element {element} not found in collection of elements");
        }

        /// <summary>
        /// Get an element at the specified index
        /// </summary>
        /// <param name="index">Index of the element</param>
        /// <returns>The element at the specified index</returns>
        /// <exception cref="InternalException">Thrown if the index is out of range</exception>
        public T Get(int index)
        {
            if (index < 0 || index >= Elements.Count) throw new InternalException($"Index {index} is out of range for collection of elements with count {Elements.Count}");
            return Elements.ElementAt(index);
        }

    }

}
