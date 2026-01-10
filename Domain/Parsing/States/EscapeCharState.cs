using Nt.Parsing;

namespace Nt.SyntaxParser.Parsing.States
{
    internal class EscapeCharState(Parser parser) : IState
    {
        public void Handle(char c)
        {
            var next = parser.NextSymbols(parser.CurrentToken);
            if (parser.Symbols.Contains(parser.CurrentToken))
            {
                parser.ParseCurrent();
            }

            if (parser.Breaks.Contains(c))
            {
                parser.CurrentState = new SymbolState(parser);
                parser.CurrentToken += c;
            }
            else if (parser.Separators.Contains(c))
            {
                parser.CurrentState = new DefaultState(parser);
            }
            else
            {
                parser.CurrentState = new DefaultState(parser);
                parser.CurrentToken += c;
            }
        }
    }
}
