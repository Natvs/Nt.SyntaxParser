using Nt.Syntax.Structures;
using System.Text;

namespace Nt.Syntax.Exportation
{

    internal class StringExporter
    {
        public static string ToString(IEnumerable<GrammarToken> symbols)
        {
            var sb = new StringBuilder();
            
            for (int i = 0; i < symbols.Count() - 1; i++)
            {
                sb.Append("- ").Append(symbols.ElementAt(i)).AppendLine();
            }
            sb.Append("- ").Append(symbols.Last());
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
                    symbol = rule.Axiom?.Name;
                    continue;
                }

                if (symbol == rule.Axiom?.Name)
                {
                    sb.Append(" | ").Append(rule.Derivation);
                }
                else
                {
                    sb.AppendLine().Append("- ").Append(rule);
                    symbol = rule.Axiom?.Name;
                }
            }
            return sb.ToString();
        }

        public static string ToString(IEnumerable<RegularExpression> regexes)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < regexes.Count() - 1; i++)
            {
                sb.Append("- ").Append(regexes.ElementAt(i)).AppendLine();
            }
            sb.Append("- ").Append(regexes.Last());
            return sb.ToString();
        }

        public static string ToString(IEnumerable<GrammarToken> terminals, IEnumerable<GrammarToken> nonterminals, NonTerminal? axiom, IEnumerable<Rule> rules, IEnumerable<RegularExpression> regexes)
        {
            var sb = new StringBuilder();
            var jump = false;
            if (terminals.Any())
            {
                jump = true;
                sb.AppendLine("Terminals:").Append(ToString(terminals));
            }
            if (nonterminals.Any()) { 
                if (jump) sb.AppendLine().AppendLine();
                sb.AppendLine("Non terminals:").Append(ToString(nonterminals));
            }
            if (axiom != null) { 
                if (jump) sb.AppendLine().AppendLine();
                sb.Append("Axiom: ").Append(axiom);
            }
            if (rules.Any()) { 
                if (jump) sb.AppendLine().AppendLine();
                sb.AppendLine("Rules:").Append(ToString(rules));
            }
            if (regexes.Any()) { 
                if (jump) sb.AppendLine().AppendLine();
                sb.AppendLine("Regular expressions:").Append(ToString(regexes));
            }
            return sb.ToString();
        }

    }


}
