using Nt.Parser.Structures;

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

}
