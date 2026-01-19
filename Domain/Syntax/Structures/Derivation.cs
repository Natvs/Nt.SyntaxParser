using Nt.Parser.Structures;
using System.Collections;
using System.Text;

namespace Nt.Syntax.Structures
{
    /// <summary>
    /// Represents a rule derivation of tokens
    /// </summary>
    /// <param name="terminals">Terminal tokens of the rule</param>
    /// <param name="nonterminals">Non terminal tokens of the rule</param>
    public class Derivation : IReadOnlyList<GrammarToken>
    {
        #region Private

        private List<GrammarToken> Tokens { get; } = [];

        #endregion

        #region Public

        public int Count => Tokens.Count;

        public GrammarToken this[int index] => throw new NotImplementedException();
        public IEnumerator<GrammarToken> GetEnumerator()
        {
            foreach (var token in Tokens)
            {
                yield return token;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns a list of all grammar tokens in the current collection.
        /// </summary>
        /// <returns>A list of <see cref="GrammarToken"/> objects representing the tokens in the collection.</returns>
        public List<GrammarToken> GetTokens()
        {
            return [.. Tokens];
        }

        /// <summary>
        /// Retrieves the grammar token at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the token to retrieve.</param>
        /// <returns>The <see cref="GrammarToken"/> at the specified index.</returns>
        public GrammarToken Get(int index)
        {
            return Tokens[index];
        }

        /// <summary>
        /// Adds the specified grammar token to the collection.
        /// </summary>
        /// <param name="token">The grammar token to add to the collection.</param>
        public void Add(GrammarToken token)
        {
            Tokens.Add(token);
        }

        /// <summary>
        /// Inserts the specified token into the collection at the given position.
        /// </summary>
        /// <param name="position">The zero-based index at which the token should be inserted.</param>
        /// <param name="token">The token to insert into the collection.</param>
        public void Insert(int position, GrammarToken token)
        {
            Tokens.Insert(position, token);
        }

        /// <summary>
        /// Gets a string representing the list of tokens in derivation
        /// </summary>
        /// <returns>String representing the derivation</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Tokens.Count > 0)
            {
                for (int i = 0; i < Tokens.Count - 1; i++)
                {
                    sb.Append(Tokens[i].Symbol.Name).Append(' ');
                }
                sb.Append(Tokens[^1].Symbol.Name);
            }
            else
            {
                sb.Append("<<undefined>>");
            }
            return sb.ToString();
        }

        #endregion

    }
}
