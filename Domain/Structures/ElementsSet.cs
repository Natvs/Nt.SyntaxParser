using Nt.Syntax.Exceptions;
using Nt.Syntax.Exportation;
using Nt.Syntax.Structures;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace Nt.Syntax.Structures
{
    public class ElementsSet<T>() : IEnumerable<T> where T: class
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
        /// Remove the first element validating a predicate
        /// </summary>
        /// <param name="predicate">The predicate to validate</param>
        internal void Remove(Func<T, bool> predicate)
        {
            if (TryGet(predicate, out var element))
            {
                Remove(element);
            }
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

        /// <summary>
        /// Get an element following a predicate
        /// </summary>
        /// <param name="predicate">The predicate to validate</param>
        /// <returns>The first element that validates the predicate</returns>
        /// <exception cref="InternalException">Thrown if no element is found that validates the predicate</exception>
        public T Get(Func<T, bool> predicate)
        {
            foreach (var element in Elements)
            {
                if (predicate(element))
                {
                    return element;
                }
            }
            throw new InternalException($"No element found in collection of elements that validates the predicate");
        }

        /// <summary>
        /// Get an element following a predicate, returning true if found, otherwise false
        /// </summary>
        /// <param name="predicate">The predicate to validate</param>
        /// <param name="element">The element that validates the predicate, or null if no such element is found</param>
        /// <returns>true if an element is found that validates the predicate, otherwise false</returns>
        public bool TryGet(Func<T, bool> predicate, [NotNullWhen(true)] out T? element)
        {
            foreach (var e in Elements)
            {
                if (predicate(e))
                {
                    element = e;
                    return true;
                }
            }
            element = default;
            return false;
        }

        /// <summary>
        /// Specify if the collection contains an element
        /// </summary>
        /// <param name="element">The element to check</param>
        /// <returns>True if the collection contains the element, otherwise false</returns>
        public bool Contains(T element)
        {
            return Elements.Contains(element);
        }

        /// <summary>
        /// Specify if the collection contains an element validating a predicates
        /// </summary>
        /// <param name="predicates">The predicates to validate</param>
        /// <returns>True if an element validating the predicate is found, otherwise false</returns>
        public bool Contains(Func<T, bool> predicates)
        {
            foreach (var element in Elements)
            {
                if (predicates(element))
                {
                    return true;
                }
            }
            return false;
        }
    }

}
