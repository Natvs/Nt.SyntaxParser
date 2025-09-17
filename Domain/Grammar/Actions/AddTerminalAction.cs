using GrammarReader.Domain.Grammar.Exceptions;
using GrammarReader.Domain.Parser.Structures;

namespace GrammarReader.Domain.Grammar.Actions
{
    /// <summary>
    /// Represents an action that adds new terminal
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="tokens">List of all tokens</param>
    public class AddTerminalAction(Structures.Grammar grammar, TokensList tokens) : Action
    {
        /// <summary>
        /// Adds a parsed token as new non terminal of the grammar
        /// </summary>
        /// <param name="word">Parsed token to add as new terminal</param>
        public override void Perform(ParsedToken word)
        {
            try
            {
                grammar.AddTerminal(tokens[word.TokenIndex].Name);
            }
            catch (RegisteredNonTerminalException) { }
            catch (RegisteredTerminalException) { }
        }

    }
}
