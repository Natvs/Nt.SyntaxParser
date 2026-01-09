using Nt.Parsing;
using Nt.Parsing.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.SyntaxParser.Parsing.States
{
    internal interface IState 
    {
       void Handle(char c);
    }

    internal class EscapeCharState(Parser parser) : IState
    {
        public void Handle(char c)
        {
            if (c == '\r') return;

            var next = parser.NextSymbols(parser.CurrentToken);
            if (parser.Symbols.Contains(parser.CurrentToken))
            {
                parser.ParseCurrent();
            }

            if (parser.Breaks.Contains(c))
            {
                parser.CurrentState = new SymbolState(parser);
            }
            else if (parser.Separators.Contains(c))
            {
                parser.CurrentState = new DefaultState(parser);
            }
            else
            {
                parser.CurrentState = new DefaultState(parser);
            }
            parser.CurrentToken += c;
            parser.CurrentState = new DefaultState(parser);

            if (c == '\n') parser.CurrentLine += 1;
        }
    }

    internal class SymbolState(Parser parser) : IState
    {

        public void Handle(char c)
        {
            if (c == '\r') return;  // Ignores carriage return

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

            if (c == '\n') parser.CurrentLine += 1;
        }
    }

    internal class DefaultState(Parser parser) : IState
    {
        public void Handle(char c)
        {
            if (c == '\r') return; // Ignores carriage return

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

            if (c == '\n') parser.CurrentLine += 1;
        }

    }
}
