using Nt.Parser.Symbols;
using Nt.Syntax.Structures;
using System.Text;

namespace Nt.Syntax.Exportation
{

    internal sealed class FileExporter
    {
        public static string Save(IEnumerable<ISymbol> symbols, GrammarTokenType types, FileStyle style = FileStyle.Default)
        {
            var sb = new StringBuilder();

            var first = true;
            switch (style)
            {
                case FileStyle.Light:
                    if (types == GrammarTokenType.Terminal) sb.AppendLine("Terminals:");          
                    else sb.AppendLine("Non Terminals:");

                    foreach (var symbol in symbols)
                    {
                        if (first)
                        {
                            first = false;
                            sb.Append("     ").Append(symbol.Name);
                            continue;
                        }
                        sb.AppendLine(",").Append("    ").Append(symbol.Name);
                    }
                    sb.AppendLine(";");

                    break;
                case FileStyle.Default:
                    if (types == GrammarTokenType.Terminal) sb.Append("T = {");
                    else sb.Append("N = {");

                    foreach (var symbol in symbols)
                    {
                        if (first)
                        {
                            first = false;
                            sb.Append(symbol.Name);
                            continue;
                        }
                        sb.Append(", ").Append(symbol.Name);
                    }
                    sb.AppendLine("}");
                    break;
            }

            return sb.ToString();
        }

        public static string Save(IEnumerable<Rule> rules, FileStyle style = FileStyle.Default)
        {
            var sb = new StringBuilder();

            var symbol = "";
            switch (style) 
            {
                case FileStyle.Light:
                    sb.AppendLine("Rules:");

                    foreach (var rule in rules)
                    {
                        if (symbol == "")
                        {
                            symbol = rule.Token?.Symbol.Name;
                            sb.Append("     ").Append(rule);
                            continue;
                        }

                        if (symbol == rule.Token?.Symbol.Name)
                        {
                            sb.Append(" | ").Append(rule.Derivation);
                        }
                        else
                        {
                            symbol = rule.Token?.Symbol.Name;
                            sb.AppendLine(",").Append("    ").Append(rule);
                        }
                    }

                    sb.AppendLine(";");
                    break;

                case FileStyle.Default:
                    
                    foreach (var rule in rules)
                    {
                        if (symbol == "")
                        {
                            symbol = rule.Token?.Symbol.Name;
                            sb.Append("R: ").Append(rule);
                            continue;
                        }

                        if (symbol == rule.Token?.Symbol.Name)
                        {
                            sb.Append(" | ").Append(rule.Derivation);
                        }
                        else
                        {
                            symbol = rule.Token?.Symbol.Name;
                            sb.AppendLine(";").Append("R: ").Append(rule);
                        }
                    }
                   
                    sb.AppendLine(";");
                    break;
            }

            return sb.ToString();
        }

        public static string Save(IEnumerable<RegularExpression> regexes, FileStyle style = FileStyle.Default)
        {
            var sb = new StringBuilder();
            var first = true;
            switch (style)
            {
                case FileStyle.Light:
                    sb.AppendLine("Regular Expressions:");
                    foreach (var regex in regexes)
                    {
                        if (first)
                        {
                            first = false;
                            sb.Append("     ").Append(regex);
                            continue;
                        }                    
                        sb.AppendLine(",").Append("    ").Append(regex);
                    }
                    sb.AppendLine(";");
                    break;
                case FileStyle.Default:
                    foreach (var regex in regexes)
                    {
                        sb.Append("E: ").Append(regex).AppendLine(";");
                    }
                    break;
            }
            return sb.ToString();
        }

        public static string Save(IEnumerable<ISymbol> terminals, IEnumerable<ISymbol> nonterminals, NonTerminal? axiom, IEnumerable<Rule> rules, IEnumerable<RegularExpression> regexes, FileStyle style = FileStyle.Default)
        {
            var sb = new StringBuilder();
            sb.AppendLine(Save(terminals, GrammarTokenType.Terminal, style));
            sb.AppendLine(Save(nonterminals, GrammarTokenType.NonTerminal, style));
            if (axiom != null) sb.Append("S = ").Append(axiom).AppendLine().AppendLine();
            if (rules.Any()) sb.AppendLine(Save(rules, style));
            if (regexes.Any()) sb.AppendLine(Save(regexes, style));
            return sb.ToString();
        }

    }
}
