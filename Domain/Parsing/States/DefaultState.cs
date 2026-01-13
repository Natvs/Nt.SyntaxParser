using Nt.Parsing;

namespace Nt.SyntaxParser.Parsing.States
{
    internal class DefaultState(Parser parser) : IState
    {
        public void Handle(char c)
        {
            if (parser.Breaks.Contains(c)) 
            {
                parser.ParseCurrent();
                parser.CurrentToken = c.ToString();
                parser.CurrentState = new SymbolState(parser);
            }
            else if (parser.Separators.Contains(c))
            {
                parser.ParseCurrent();
            }
            else if (c == '\\')
            { 
                parser.CurrentState = new EscapeCharState(parser);
            } 
            else parser.CurrentToken += c;
        }

    }
}
