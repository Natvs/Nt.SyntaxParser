using Nt.Parser.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Exportation
{

    public static class FileExporterExtensions
    {
        public static string Save(this SymbolsList symbols, GrammarTokenType types, FileStyle style = FileStyle.Default)
        {
            return FileExporter.Save(symbols.GetSymbols(), types, style);
        }
        public static string Save(this RulesSet rules, FileStyle style = FileStyle.Default)
        {
            return FileExporter.Save(rules.ToList(), style);
        }
        public static string Save(this RegexSet regexes, FileStyle style = FileStyle.Default)
        {
            return FileExporter.Save(regexes.ToList(), style);
        }
        public static string Save(this Grammar grammar, FileStyle style = FileStyle.Default)
        {
            var terminals = grammar.Terminals.GetSymbols();
            var nonterminals = grammar.NonTerminals.GetSymbols();
            var rules = grammar.Rules.ToList();
            var regexes = grammar.RegularExpressions.ToList();
            return FileExporter.Save(terminals, nonterminals, grammar.Axiom, rules, regexes, style);
        }
    }
}
