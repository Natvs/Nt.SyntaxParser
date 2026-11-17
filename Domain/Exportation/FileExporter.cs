using Nt.Syntax.Structures;
using System.Text;

namespace Nt.Syntax.Exportation
{

    internal sealed class FileExporter
    {
        public static string Save(IEnumerable<GrammarToken> symbols, GrammarTokenType types)
        {
            var sb = new StringBuilder();

            var first = true;

            if (types == GrammarTokenType.Terminal) sb.AppendLine("TERMINALS (");          
            else sb.AppendLine("NON TERMINALS (");

            foreach (var symbol in symbols)
            {
                if (first)
                {
                    first = false;
                    sb.Append("    ").AppendLine(symbol.Name);
                    continue;
                }
            }
            sb.Append(')');
            return sb.ToString();
        }

        public static string Save(IEnumerable<Rule> rules)
        {
            var sb = new StringBuilder();

            var symbol = "";
            sb.AppendLine("RULES (");
            foreach (var rule in rules)
            {
                if (symbol == "")
                {
                    symbol = rule.Axiom?.Symbol.Name;
                    sb.Append("    ").Append(rule);
                    continue;
                }

                if (symbol == rule.Axiom?.Symbol.Name)
                {
                    sb.Append(" | ").Append(rule.Derivation);
                }
                else
                {
                    symbol = rule.Axiom?.Symbol.Name;
                    sb.AppendLine(" ;").Append("    ").Append(rule);
                }
            }

            sb.AppendLine(" ;").Append(')');

            return sb.ToString();
        }

        public static string Save(IEnumerable<RegularExpression> regexes)
        {
            var sb = new StringBuilder();
            sb.AppendLine("REGULAR EXPRESSIONS (");
            foreach (var regex in regexes)
            {
                sb.Append("    ").Append(regex).AppendLine();
            }
            sb.Append(')');

            return sb.ToString();
        }

        public static string Save(IEnumerable<GrammarToken> terminals, IEnumerable<GrammarToken> nonterminals, NonTerminal? axiom, IEnumerable<Rule> rules, IEnumerable<RegularExpression> regexes)
        {
            var sb = new StringBuilder();
            var jump = false;
            if (terminals.Any())
            {
                jump = true;
                sb.Append(Save(terminals, GrammarTokenType.Terminal));
            }
            if (nonterminals.Any())
            {
                if (jump) sb.AppendLine().AppendLine();
                sb.Append(Save(nonterminals, GrammarTokenType.NonTerminal));
            }
            if (axiom != null)
            {
                if (jump) sb.AppendLine().AppendLine();
                sb.Append("Axiom ( ").Append(axiom).Append(" )");
            }
            if (rules.Any())
            {
                if (jump) sb.AppendLine().AppendLine();
                sb.Append(Save(rules));
            }
            if (regexes.Any())
            {
                if (jump) sb.AppendLine().AppendLine();
                sb.Append(Save(regexes));
            }
            return sb.ToString();
        }

    }
}
