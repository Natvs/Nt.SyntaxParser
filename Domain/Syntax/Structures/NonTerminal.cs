namespace Nt.SyntaxParser.Syntax.Structures
{
    public class NonTerminal:  GrammarToken
    {
        public NonTerminal(int index, int line): base(index, line)
        {
            this.Type = GrammarTokenType.NonTerminal;
        }
    }
}
