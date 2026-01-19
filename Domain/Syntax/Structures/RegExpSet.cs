using System.Text;
using Nt.Parser.Structures;

namespace Nt.Syntax.Structures
{
    public class RegExpSet() : HashSet<RegularExpression>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('{').Append(string.Join(",", this)).Append('}');
            return sb.ToString();
        }
    }
}