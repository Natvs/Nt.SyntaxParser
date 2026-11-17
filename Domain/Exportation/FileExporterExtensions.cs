using Nt.Parser.Structures;
using Nt.Syntax.Structures;
using System.Text.RegularExpressions;
using static Nt.Syntax.Exportation.ExportationMethods;

namespace Nt.Syntax.Exportation
{

    public static class FileExporterExtensions
    {

        /// <summary>
        /// Export a collection of terminals to a file syntax.
        /// </summary>
        /// <param name="symbols">Set of symbols to export</param>
        /// <param name="mode">Exportation mode (original, sorted, etc.)</param>
        /// <returns>A string representing the exported terminals</returns>
        public static string Save(this ElementsSet<Terminal> symbols, ExportationMode mode)
        {
            List<GrammarToken> grammarTokens = [.. symbols];
            var orderedSymbols = Reorder(grammarTokens, mode);
            return FileExporter.Save(orderedSymbols, GrammarTokenType.Terminal);
        }

        /// <summary>
        /// Export a collection of non terminals to a file syntax.
        /// </summary>
        /// <param name="symbols">Set of symbols to export</param>
        /// <param name="mode">Exportation mode (original, sorted, etc.)</param>
        /// <returns>A string representing the exported non terminals</returns>
        public static string Save(this ElementsSet<NonTerminal> symbols, ExportationMode mode)
        {
            List<GrammarToken> grammarTokens = [.. symbols];
            var orderedSymbols = Reorder(grammarTokens, mode);
            return FileExporter.Save(orderedSymbols, GrammarTokenType.NonTerminal);
        }

        /// <summary>
        /// Export a collection of rules to a file syntax.
        /// </summary>
        /// <param name="rules">Set of rules to export</param>
        /// <param name="mode">Exportation mode (original, sorted, etc.)</param>
        /// <returns>A string representing the exported rules</returns>
        public static string Save(this ElementsSet<Rule> rules, ExportationMode mode)
        {
            var orderedRules = Reorder(rules.ToList(), mode);
            return FileExporter.Save(orderedRules);
        }

        /// <summary>
        /// Export a collection of regular expressions to a file syntax.
        /// </summary>
        /// <param name="regexes">Set of regular expressions to export</param>
        /// <param name="mode">Exportation mode (original, sorted, etc.)</param>
        /// <returns>A string representing the exported regular expressions</returns>
        public static string Save(this ElementsSet<RegularExpression> regexes, ExportationMode mode)
        {
            var orderedRegexes = Reorder(regexes.ToList(), mode);
            return FileExporter.Save(orderedRegexes);
        }

        /// <summary>
        /// Export a grammar to a file syntax.
        /// </summary>
        /// <param name="grammar">Grammar to export</param>
        /// <param name="mode">Exportation mode (original, sorted, etc.)</param>
        /// <returns></returns>
        public static string Save(this Grammar grammar, ExportationMode mode)
        {
            var terminals = Reorder([.. grammar.Terminals], mode);
            var nonterminals = Reorder([.. grammar.NonTerminals], mode);
            var rules = Reorder([.. grammar.Rules], mode);
            var regexes = Reorder([.. grammar.RegularExpressions], mode);
            return FileExporter.Save(terminals, nonterminals, grammar.Axiom, rules, regexes);
        }
    }
}
