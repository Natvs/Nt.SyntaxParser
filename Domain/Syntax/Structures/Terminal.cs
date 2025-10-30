namespace Nt.Syntax.Structures
{
    public class Terminal : GrammarToken
    {
        public Terminal(int index, int line): base(index, line)
        {
            this.Type = GrammarTokenType.Terminal;
        }
    }
}
