using Nt.Parser.Structures;
using Nt.Parser.Symbols;
using Nt.Syntax.Structures;
using System;
using System.Collections.Generic;
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
                sb.AppendLine($"- {symbol}");
            }
            return sb.ToString();
        }

        public static string ToString(IEnumerable<Rule> rules)
        {
            var sb = new StringBuilder();
            foreach (var rule in rules)
            {
                sb.AppendLine($"- {rule}");
            }
            return sb.ToString();
        }

        public static string ToString(IEnumerable<RegularExpression> regexes)
        {
            var sb = new StringBuilder();
            foreach (var regex in regexes)
            {
                sb.AppendLine($"- {regex}");
            }
            return sb.ToString();
        }

        public static string ToString(IEnumerable<ISymbol> terminals, IEnumerable<ISymbol> nonterminals, NonTerminal? axiom, IEnumerable<Rule> rules, IEnumerable<RegularExpression> regexes)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Terminals:\n {ToString(terminals)}");
            sb.AppendLine($"Nonterminals:\n {ToString(nonterminals)}");
            if (axiom != null) sb.AppendLine($"Axiom: {axiom}");
            if (rules.Any()) sb.AppendLine($"Rules:\n{ToString(rules)}");
            if (regexes.Any()) sb.AppendLine($"Regular Expressions:\n{ToString(regexes)}");
            return sb.ToString();
        }

    }
}
