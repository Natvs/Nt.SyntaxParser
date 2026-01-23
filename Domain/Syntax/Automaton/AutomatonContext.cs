using Nt.Syntax.Structures;
using Nt.Automaton;

namespace Nt.Syntax.Automaton
{
    public class AutomatonContext
    {
        internal ImportPath ImportPath { get; private set; } = new ImportPath();
        public string? ImportedString { get; internal set; }
        public Rule? Rule { get; internal set; }
        public RegularExpression? Regex { get; internal set; }
        internal string CurrentTerminal { get; set; } = "";
        internal string CurrentNonTerminal { get; set; } = "";
        internal string CurrentImportFile { get; set; } = "";
        internal string CurrentImportPath { get; set; } = "";

        internal void Reset()
        {
            ImportPath.ClearPath();
            ImportedString = null;
            Rule = null;
            Regex = null;
            CurrentTerminal = "";
            CurrentNonTerminal = "";
        }

        #region Public methods
        
        public List<string> GetPath()
        {
            return ImportPath.GetPath();
        }
        
        #endregion
    }
}
