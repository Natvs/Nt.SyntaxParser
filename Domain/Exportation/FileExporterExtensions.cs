using Nt.Parser.Structures;
using Nt.Syntax.Structures;
using System.Text.RegularExpressions;
using static Nt.Syntax.Exportation.ExportationMethods;

namespace Nt.Syntax.Exportation
{

    public static class FileExporterExtensions
    {
        public static string Save(this ElementsSet<GrammarToken> symbols, GrammarTokenType types, ExportationMode mode, FileStyle style = FileStyle.Default)
        {
            var orderedSymbols = Reorder(symbols.ToList(), mode);
            return FileExporter.Save(orderedSymbols, types, style);
        }
        public static string Save(this ElementsSet<Rule> rules, ExportationMode mode, FileStyle style = FileStyle.Default)
        {
            var orderedRules = Reorder(rules.ToList(), mode);
            return FileExporter.Save(orderedRules, style);
        }
        public static string Save(this ElementsSet<RegularExpression> regexes, ExportationMode mode, FileStyle style = FileStyle.Default)
        {
            var orderedRegexes = Reorder(regexes.ToList(), mode);
            return FileExporter.Save(orderedRegexes, style);
        }
        public static string Save(this Grammar grammar, ExportationMode mode, FileStyle style = FileStyle.Default)
        {
            var terminals = Reorder([.. grammar.Terminals], mode);
            var nonterminals = Reorder([.. grammar.NonTerminals], mode);
            var rules = Reorder([.. grammar.Rules], mode);
            var regexes = Reorder([.. grammar.RegularExpressions], mode);
            return FileExporter.Save(terminals, nonterminals, grammar.Axiom, rules, regexes, style);
        }
    }
}
