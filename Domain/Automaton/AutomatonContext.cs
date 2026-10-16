using Nt.Syntax.Structures;

namespace Nt.Syntax.Automaton
{
    public class AutomatonContext
    {
        internal void Reset()
        {
            ImportPath.ClearPath();
            ImportedString = null;
            Rule = null;
            Regex = null;
            Terminal = "";
            NonTerminal = "";
        }




        // Terminal

        /// <summary>
        /// Represent the terminal previously parsed
        /// </summary>
        public string Terminal { get; internal set; } = "";
        internal bool AddingToTerminal { get; set; } = false;




        // Non terminal

        /// <summary>
        /// Represent the non terminal previously parsed
        /// </summary>
        public string NonTerminal { get; internal set; } = "";
        internal bool AddingToNonTerminal { get; set; } = false;



        // Rule

        /// <summary>
        /// Represents the last rule that was processed in the automaton context.
        /// </summary>
        public Rule? Rule { get; internal set; }




        // Regular expression

        /// <summary>
        /// Represents the last regular expression that was processed in the automaton context.
        /// </summary>
        public RegularExpression? Regex { get; internal set; }




        // Importation

        /// <summary>
        /// Represents the last imported string from an import statement.
        /// </summary>
        public string? ImportedString { get; internal set; }
        internal ImportPath ImportPath { get; } = new ImportPath();
        internal string CurrentImportFile { get; set; } = "";
        internal string CurrentImportPath { get; set; } = "";

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
