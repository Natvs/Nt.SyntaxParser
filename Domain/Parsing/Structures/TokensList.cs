using System.Text;

namespace GrammarParser.Parsing.Structures
{
    /// <summary>
    /// Represents a list of tokens, where tokens are words.
    /// </summary>
    public class TokensList : List<Token>
    {

        /// <summary>
        /// Instantiates a list of tokens
        /// </summary>
        public TokensList() : base() { }
        /// <summary>
        /// Instantiates a list of tokens from a list of strings.
        /// </summary>
        /// <param name="list">List of string used to instantiate the tokens</param>
        public TokensList(List<string> list)
        {
            foreach (var word in list) Add(new Token(word));
        }

        /// <summary>
        /// Adds a new token to the list.
        /// </summary>
        /// <param name="name">Name of the token to add</param>
        /// <returns>Last index of the list once the token has been added</returns>
        public int Add(string name)
        {
            var token = new Token(name);
            Add(token);
            return Count - 1;
        }

        /// <summary>
        /// Add a range of tokens to the list.
        /// </summary>
        /// <param name="names">Names of the tokens to add</param>
        /// <returns>Last index of the list once all the tokens have been added</returns>
        public int AddRange(IEnumerable<string> names)
        {
            foreach (var name in names)
            {
                var token = new Token(name);
                Add(token);
            }
            return Count - 1;
        }

        /// <summary>
        /// Check if a token is in the list
        /// </summary>
        /// <param name="name">Name of the token to look for</param>
        /// <returns>True if the token is in the list, False if not</returns>
        public bool Contains(string name)
        {
            foreach (Token token in this)
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
        public Token Get(string name)
        {
            foreach (Token token in this)
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
}
