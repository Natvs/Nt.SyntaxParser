using Nt.Syntax.Structures;

namespace Nt.Syntax.Automaton
{
    public class AutomatonContext
    {
        internal ImportPath ImportPath { get; } = new ImportPath();
        internal string CurrentTerminal { get; set; } = "";
        internal string CurrentNonTerminal { get; set; } = "";
        internal string CurrentImportFile { get; set; } = "";
        internal string CurrentImportPath { get; set; } = "";

        /// <summary>
        /// Represents the last imported string from an import statement.
        /// </summary>
        public string? ImportedString { get; internal set; }

        /// <summary>
        /// Represents the last rule that was processed in the automaton context.
        /// </summary>
        public Rule? Rule { get; internal set; }

        /// <summary>
        /// Represents the last regular expression that was processed in the automaton context.
        /// </summary>
        public RegularExpression? Regex { get; internal set; }

        internal void Reset()
        {
            ImportPath.ClearPath();
            ImportedString = null;
            Rule = null;
            Regex = null;
            CurrentTerminal = "";
            CurrentNonTerminal = "";
        }

        /// <summary>
        /// Get a list of paths that have been added to the import path.
        /// </summary>
        /// <returns></returns>
        public List<string> GetPath()
        {
            return ImportPath.GetPath();
        }
        
    }
}
