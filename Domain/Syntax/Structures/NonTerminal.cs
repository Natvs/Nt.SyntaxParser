namespace Nt.Syntax.Structures
{
    public class NonTerminal:  GrammarToken
    {
        public NonTerminal(int index, int line): base(index, line)
        {
            Type = GrammarTokenType.NonTerminal;
        }
    }
}
