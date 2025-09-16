using GrammarReader.Code.Parser.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Grammar.Actions
{
    /// <summary>
    /// Represents an action that adds new non terminal
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="tokens">List of all tokens</param>
    public class AddNonTerminalAction(Structures.Grammar grammar, TokensList tokens) : Action
    {
        /// <summary>
        /// Adds a parsed token as new non terminal of the grammar
        /// </summary>
        /// <param name="word">Parsed token to add as new non terminal</param>
        public override void Perform(ParsedToken word)
        {
            grammar.AddNonTerminal(tokens[word.TokenIndex].Name);
        }

    }

}
