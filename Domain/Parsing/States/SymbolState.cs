using Nt.Parsing;

namespace Nt.SyntaxParser.Parsing.States
{
    internal class SymbolState(Parser parser) : IState
    {

        public void Handle(char c)
        {
            List<char> next = parser.NextSymbols(parser.CurrentToken);
            if (c == '\\')
            {
                parser.CurrentState = new EscapeCharState(parser);
            }
            else if (next.Contains(c))
            {
                parser.CurrentToken += c.ToString();
            }
            else
            {
                parser.ParseCurrent();
                if (parser.Breaks.Contains(c))
                {
                    parser.CurrentToken = c.ToString();
                }
                else if (parser.Separators.Contains(c))
                {
                    parser.CurrentState = new DefaultState(parser);
                }
                else
                {
                    parser.CurrentToken = c.ToString();
                    parser.CurrentState = new DefaultState(parser);
                }
            }
        }
    }
}
