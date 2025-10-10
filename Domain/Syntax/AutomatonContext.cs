using Nt.SyntaxParser.Syntax.Structures;

namespace Nt.SyntaxParser.Syntax
{
    public class AutomatonContext
    {
        public ImportPath? ImportPath { get; private set; }
        public string? ImportedString { get; set; }
        public Rule? Rule { get; set; }
        public RegularExpression? RegularExpression { get; set; }

        internal void Reset()
        {
            ImportPath = new ImportPath();
            ImportedString = null;
            Rule = null;
            RegularExpression = null;
        }
    }
}
