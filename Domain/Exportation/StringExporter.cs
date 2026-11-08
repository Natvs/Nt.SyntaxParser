using Nt.Parser.Symbols;
using Nt.Syntax.Structures;
using System.Text;

namespace Nt.Syntax.Exportation
{

    internal class StringExporter
    {
        public static string ToString(IEnumerable<ISymbol> symbols)
        {
            var sb = new StringBuilder();
            foreach(var symbol in symbols)
            {
                sb.Append("- ").Append(symbol).AppendLine();
            }
            return sb.ToString();
        }

        public static string ToString(IEnumerable<Rule> rules)
        {
            var sb = new StringBuilder();

            var symbol = "";
            foreach (var rule in rules)
            {
                if (symbol == "")
                {
                    sb.Append("- ").Append(rule);
                    symbol = rule.Token?.Name;
                    continue;
                }

                if (symbol == rule.Token?.Name)
                {
                    sb.Append(" | ").Append(rule.Derivation);
                }
                else
                {
                    sb.AppendLine().Append("- ").Append(rule);
                    symbol = rule.Token?.Name;
                }
            }
            return sb.ToString();
        }

        public static string ToString(IEnumerable<RegularExpression> regexes)
        {
            var sb = new StringBuilder();
            foreach (var regex in regexes)
            {
                sb.AppendLine($"- ").Append(regex).AppendLine();
            }
            return sb.ToString();
        }

        public static string ToString(IEnumerable<ISymbol> terminals, IEnumerable<ISymbol> nonterminals, NonTerminal? axiom, IEnumerable<Rule> rules, IEnumerable<RegularExpression> regexes)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Terminals:").Append(ToString(terminals)).AppendLine();
            sb.AppendLine("Non terminals:").Append(ToString(nonterminals)).AppendLine();
            if (axiom != null) sb.Append("Axiom: ").Append(axiom).AppendLine();
            if (rules.Any()) sb.AppendLine("Rules:").Append(ToString(rules)).AppendLine();
            if (regexes.Any()) sb.Append("Regular expressions:").Append(ToString(regexes)).AppendLine();
            return sb.ToString();
        }

    }


}
