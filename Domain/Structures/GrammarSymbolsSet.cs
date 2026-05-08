using Nt.Parser.Structures;
using Nt.Parser.Symbols;

namespace Nt.Syntax.Structures
{
    public class GrammarSymbolsSet(ISymbolFactory factory)
    {
        private SymbolsList Symbols { get; } = new(factory);

        public int Count { get => Symbols.GetCount(); }

        internal void Add(string name)
        {
            Symbols.Add(name);
        }

        internal void Remove(string name)
        {
            Symbols.Remove(name);
        }

        /// <summary>
        /// Get a list of symbols in this set
        /// </summary>
        /// <returns>List of symbol contained in this set</returns>
        public List<ISymbol> GetSymbols()
        {
            return Symbols.GetSymbols();
        }

        /// <summary>
        /// Determine whether a symbol with the specified name exists in the collection.
        /// </summary>
        /// <param name="name">The name of the symbol to locate</param>
        /// <returns>true if a symbol with the specified name exists in the collection; otherwise, false.</returns>
        public bool Contains(string name)
        {
            return Symbols.Contains(name);
        }

        /// <summary>
        /// Get the index of a symbol in the collection.
        /// </summary>
        /// <param name="name">Name of the symbol to locate</param>
        /// <returns>The index of the symbol with the specified name</returns>
        /// <exception cref="KeyNotFoundException">Thrown when a symbol with the specified name does not exist in the collection</exception>
        public int IndexOf(string name)
        {
            return Symbols.IndexOf(name);
        }

        /// <summary>
        /// Retrieve the symbol associated with the specified name.
        /// </summary>
        /// <param name="name">Name of the symbol to retrieve.</param>
        /// <returns>The symbol associated with the specified name.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when a symbol with the specified name does not exist in the collection</exception>
        public ISymbol Get(string name)
        {
            return Symbols.Get(name);
        }

        /// <summary>
        /// Retrieves the symbol at the specified index in the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the symbol to retrieve.</param>
        /// <returns>The symbol located at the specified index.</returns>
        public ISymbol Get(int index)
        {
            return Symbols.Get(index);
        }

        /// <summary>
        /// Get a string representing a list of all symbols in the collection.
        /// </summary>
        /// <returns>A string that represents this instance.</returns>
        public override string ToString()
        {
            return Symbols.ToString();
        }
    }
}
