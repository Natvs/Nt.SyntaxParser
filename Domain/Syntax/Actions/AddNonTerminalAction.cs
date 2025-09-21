using Nt.SyntaxParser.Parsing.Structures;
using Nt.SyntaxParser.Syntax.Exceptions;

namespace Nt.SyntaxParser.Syntax.Actions
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
            try
            {
                grammar.AddNonTerminal(tokens[word.TokenIndex].Name);
            }
            catch (RegisteredNonTerminalException) { } // Ignore those 2 exceptions for now
            catch (RegisteredTerminalException) { } // Maybe someone should remove them later
        }

    }

}
