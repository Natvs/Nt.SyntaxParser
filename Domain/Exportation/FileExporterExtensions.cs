using Nt.Parser.Structures;
using Nt.Syntax.Structures;
using static Nt.Syntax.Exportation.ExportationMethods;

namespace Nt.Syntax.Exportation
{

    public static class FileExporterExtensions
    {
        public static string Save(this SymbolsList symbols, GrammarTokenType types, ExportationMode mode, FileStyle style = FileStyle.Default)
        {
            var orderedSymbols = Reorder(symbols.GetSymbols(), mode);
            return FileExporter.Save(orderedSymbols, types, style);
        }
        public static string Save(this RulesSet rules, ExportationMode mode, FileStyle style = FileStyle.Default)
        {
            var orderedRules = Reorder(rules.ToList(), mode);
            return FileExporter.Save(orderedRules, style);
        }
        public static string Save(this RegexSet regexes, ExportationMode mode, FileStyle style = FileStyle.Default)
        {
            var orderedRegexes = Reorder(regexes.ToList(), mode);
            return FileExporter.Save(orderedRegexes, style);
        }
        public static string Save(this Grammar grammar, ExportationMode mode, FileStyle style = FileStyle.Default)
        {
            var terminals = Reorder(grammar.Terminals.GetSymbols(), mode);
            var nonterminals = Reorder(grammar.NonTerminals.GetSymbols(), mode);
            var rules = Reorder(grammar.Rules.ToList(), mode);
            var regexes = Reorder(grammar.RegularExpressions.ToList(), mode);
            return FileExporter.Save(terminals, nonterminals, grammar.Axiom, rules, regexes, style);
        }
    }
}
