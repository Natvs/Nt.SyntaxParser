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

        public string ToString(SymbolsList nonTerminals)
        {
            var sb = new StringBuilder();
            sb.Append('{');
            foreach (var nt in nonTerminals.GetSymbols())
            {
                var rules = this.Where(r => r.Token != null && r.Token.Index == nonTerminals.IndexOf(nt.Name)).ToList();
                sb.Append('}');
            }
            return sb.ToString();
        }
    }
}