using Nt.Parsing.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    public abstract class RuleAction : IAction
    {
        public abstract Rule? Perform(Rule? rule, ParsedToken word);
    }

}
