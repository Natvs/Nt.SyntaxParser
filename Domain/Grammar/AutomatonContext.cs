using GrammarReader.Domain.Grammar.Structures;

namespace GrammarReader.Domain.Grammar
{
    public class AutomatonContext
    {
        public ImportPath ImportPath { get; } = new ImportPath();
        public string? ImportedString { get; set; }
        public Rule? Rule { get; set; }
        public RegularExpression? RegularExpression { get; set; }
    }
}
