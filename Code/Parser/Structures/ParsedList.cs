using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Parser.Structures
{

    /// <summary>
    /// Represents a list of parsed words, identified by their value.
    /// </summary>
    /// <param name="tokens">List of tokens that contains all parsed tokens</param>
    public class ParsedList(TokensList tokens) : List<ParsedToken>
    {
        /// <summary>
        /// Gets a string representing the parsed list
        /// </summary>
        /// <returns>String that represents the parsed list</returns>
        public override string ToString()
        {
            var sb = new StringBuilder().Append('{');
            for (int i = 0; i < Count - 1; i++)
            {
                sb.Append(tokens[this[i].TokenIndex].Name).Append(", ");
            }
            if (Count > 0) sb.Append(tokens[this[Count - 1].TokenIndex].Name);
            sb.Append('}');
            return sb.ToString();
        }
    }

}
