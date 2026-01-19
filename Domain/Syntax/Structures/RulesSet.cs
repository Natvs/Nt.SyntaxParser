using System.Text;
using Nt.Parser.Structures;

namespace Nt.Syntax.Structures
{
    public class RulesSet(): HashSet<Rule>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('{').Append(string.Join(",", this)).Append('}');
            return sb.ToString();
        }

        /// <summary>
        /// Gets a string representing this rules set, grouping rules by non terminal orders
        /// </summary>
        /// <param name="nonTerminals"></param>
        /// <returns></returns>
        public string ToString(SymbolsList nonTerminals)
        {
            var sb = new StringBuilder().Append('{');
            foreach (var nt in nonTerminals.GetSymbols())
            {
                var rules = this.Where(r => r.Token != null && r.Token.Name.Equals(nt.Name)).ToList();
                sb.Append(string.Join(",", this));
            }
            sb.Append('}');
            return sb.ToString();
        }
    }
}