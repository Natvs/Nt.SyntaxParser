using Nt.Syntax.Structures;

namespace Nt.Syntax
{
    public class AutomatonContext
    {
        public ImportPath ImportPath { get; private set; } = new ImportPath();
        public string? ImportedString { get; set; }
        public Rule? Rule { get; set; }
        public RegularExpression? RegularExpression { get; set; }

        internal void Reset()
        {
            ImportPath.Reset();
            ImportedString = null;
            Rule = null;
            RegularExpression = null;
        }
    }
}
