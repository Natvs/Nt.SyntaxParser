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

            parser.CurrentState = new DefaultState(parser);
            parser.CurrentToken += c;
        }
    }
}
