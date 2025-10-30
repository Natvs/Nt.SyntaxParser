namespace Nt.Syntax.Structures
{
    public enum GrammarTokenType
    {
        NonTerminal,
        Terminal
    }

    public class GrammarToken(int index, int line)
    {
        public GrammarTokenType Type { get; protected set; }
        public int Index { get; } = index;
        public int Line { get; } = line;

        public override string ToString() => $"(Index: {Index}, Line: {Line})";

    }
}
