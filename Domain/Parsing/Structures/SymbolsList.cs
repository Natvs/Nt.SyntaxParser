using System.Text;

namespace Nt.Parsing.Structures
{

    public class NonTerminalSymbol(string word) : Symbol(word)
    {
        public bool IsRegExp { get; internal set; } = false;
    }

    public class SymbolsList : SymbolsList<Symbol>
    {
        /// <summary>
        /// Instantiates a list of tokens
        /// </summary>
        public SymbolsList() : base() { }
        /// <summary>
        /// Instantiates a list of tokens from a list of strings.
        /// </summary>
        /// <param name="list">List of string used to instantiate the tokens</param>
        public SymbolsList(List<string> list)
        {
            foreach (var word in list) Add(new Symbol(word));
        }

        /// <summary>
        /// Adds a new token to the list.
        /// </summary>
        /// <param name="name">Name of the token to add</param>
        /// <returns>Last index of the list once the token has been added</returns>
        public int Add(string name)
        {
            if (!Contains(name))
            {
                Add(new Symbol(name));
                return Count - 1;
            }
            return IndexOf(name);
        }

        /// <summary>
        /// Add a range of tokens to the list.
        /// </summary>
        /// <param name="names">Names of the tokens to add</param>
        /// <returns>Last index of the list once all the tokens have been added</returns>
        public int AddRange(IEnumerable<string> names)
        {
            foreach (var name in names) Add(new Symbol(name));
            return Count - 1;
        }

        /// <summary>
        /// Check if a token is in the list
        /// </summary>
        /// <param name="name">Name of the token to look for</param>
        /// <returns>True if the token is in the list, False if not</returns>
        public bool Contains(string name)
        {
            foreach (Symbol token in this)
            {
                if (token.Name.Equals(name)) { return true; }
            }
            return false;
        }

        /// <summary>
        /// Gets a token in this list by its string content.
        /// </summary>
        /// <param name="name">Name of the token to get</param>
        /// <returns>First occurence of such token with the given name in the list</returns>
        /// <exception cref="KeyNotFoundException">It might be that no token with the given name was found.</exception>
        public Symbol Get(string name)
        {
            foreach (Symbol token in this)
            {
                if (token.Name == name) { return token; }
            }
            throw new KeyNotFoundException("No token " + name + " found in list");
        }

        /// <summary>
        /// Gets the index of a token by its string content
        /// </summary>
        /// <param name="name">Name of the token to get</param>
        /// <returns>First occurence of such token indice with the given name</returns>
        /// <exception cref="KeyNotFoundException">It might be that no token with the given name was found.</exception>
        public int IndexOf(string name)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == name)
                {
                    return i;
                }
            }
            throw new KeyNotFoundException("No token " + name + " found in list");
        }

        /// <summary>
        /// Gets a string of the tokens in this list.
        /// </summary>
        /// <returns>A string representation of tokens in this list</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder().Append('{');
            for (int i = 0; i < Count - 1; i++)
            {
                sb.Append(this[i].Name).Append(", ");
            }
            if (Count > 0) sb.Append(this[Count - 1].Name);
            sb.Append('}');
            return sb.ToString();
        }
    }

    /// <summary>
    /// Represents a list of tokens, where tokens are words.
    /// </summary>
    public class SymbolsList<T> : List<T> where T : Symbol
    {

        
    }
}
