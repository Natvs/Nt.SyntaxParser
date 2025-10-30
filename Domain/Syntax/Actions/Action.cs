using Nt.Parsing.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    public interface IAction { }

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
    public abstract class RuleAction : IAction
    {
        public abstract Rule? Perform(Rule? rule, ParsedToken word);
    }

    public abstract class RegExAction : IAction
    {
        public abstract RegularExpression? Perform(RegularExpression? regex, ParsedToken word);
    }

}
