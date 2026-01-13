using Nt.Syntax.Structures;

namespace Nt.Syntax
{
    public class AutomatonContext
    {
        internal ImportPath ImportPath { get; private set; } = new ImportPath();
        internal string? ImportedString { get; 
            set; }
        internal Rule? Rule { get; set; }
        internal RegularExpression? RegularExpression { get; set; }
        internal string CurrentTerminal { get; set; } = "";
        internal string CurrentNonTerminal { get; set; } = "";
        internal string CurrentImportFile { get; set; } = "";
        internal string CurrentImportPath { get; set; } = "";

        internal void Reset()
        {
            ImportPath.Reset();
            ImportedString = null;
            Rule = null;
            RegularExpression = null;
            CurrentTerminal = "";
            CurrentNonTerminal = "";
        }
    }
}
