using Nt.Syntax.Structures;

namespace Nt.Syntax.Automaton
{
    internal class AutomatonContext
    {
        internal void Reset()
        {
            ImportPath.ClearPath();
            Files.Clear();
            Rule = null;
            Regex = null;
            Terminal = null;
            NonTerminal = null;
        }

        // Parsed Elements

        /// <summary>
        /// Represent the terminal previously parsed
        /// </summary>
        internal Terminal? Terminal { get; set; }

        /// <summary>
        /// Represent the non terminal previously parsed
        /// </summary>
        internal NonTerminal? NonTerminal { get; set; }

        /// <summary>
        /// Represents the last rule that was processed in the automaton context.
        /// </summary>
        internal Rule? Rule { get; set; }

        /// <summary>
        /// Represents the last regular expression that was processed in the automaton context.
        /// </summary>
        internal RegularExpression? Regex { get; set; }


        // Importation

        /// <summary>
        /// Represents a list of all files imported to the current grammar
        /// </summary>
        internal List<string> Files { get; set; } = [];

        /// <summary>
        /// Represents all the paths used to import files
        /// </summary>
        internal ImportPath ImportPath { get; } = new ImportPath();

        /// <summary>
        /// Get a list of paths that have been added to the import path
        /// </summary>
        /// <returns></returns>
        internal List<string> GetPath()
        {
            return ImportPath.GetPath();
        }
        
    }
}
