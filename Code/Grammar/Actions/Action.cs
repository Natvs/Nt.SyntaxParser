using GrammarReader.Code.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Grammar.Actions
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
}
