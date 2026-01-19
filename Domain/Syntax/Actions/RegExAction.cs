using Nt.Parser.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    public abstract class RegExAction : IAction
    {
        public abstract RegularExpression? Perform(RegularExpression? regex, ParsedToken word);
    }

}
