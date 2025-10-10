using System.Text;
using Nt.SyntaxParser.Parsing.Structures;

namespace Nt.SyntaxParser.Syntax.Structures
{
    public class RegExpSet() : HashSet<RegularExpression>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('{').Append(string.Join(",", this)).Append('}');
            return sb.ToString();
        }

        public string ToString(TokensList nonTerminals)
        {
            var sb = new StringBuilder();
            sb.Append('{');
            foreach (var nt in nonTerminals)
            {
                var rules = this.Where(r => r.Token != null && r.Token.Index == nonTerminals.IndexOf(nt.Name)).ToList();
                sb.Append('}');
            }
            return sb.ToString();
        }
    }
}
