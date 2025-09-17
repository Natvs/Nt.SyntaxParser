namespace GrammarReader.Domain.Grammar.Structures
{
    public class GrammarToken(int index, int line)
    {
        public int Index { get; } = index;
        public int Line { get; } = line;

    }
}
