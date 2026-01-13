using Nt.Parsing.Structures;
using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;

namespace Nt.Syntax.Actions
{

    /// <summary>
    /// Abstract class used for grammar automaton actions
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="tokens">List of all tokens</param>
    public abstract class Action : IAction
    {

        /// <summary>
        /// Performs an action
        /// </summary>
        /// <param name="word">Token of the action</param>
        public abstract void Perform(ParsedToken word);

    }

    public class SetEscapeCharAction(Grammar grammar, SymbolsList symbols) : Action()
    {
        public override void Perform(ParsedToken word)
        {
            if (symbols[word.TokenIndex].Name.Length != 1)
            {
                throw new InvalidEscapeCharSymbolException(symbols[word.TokenIndex].Name, word.Line);
            }
            grammar.EscapeCharacter = symbols[word.TokenIndex].Name.ToCharArray()[0];
        }
    }

}
