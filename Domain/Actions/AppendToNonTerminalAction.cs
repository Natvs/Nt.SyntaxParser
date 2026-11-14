using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Parser.Symbols;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class AppendToNonTerminalAction(AutomatonContext context) : ITokenAction<string>
    {
        /// <summary>
        /// Appends a parsed token to the current non terminal being built
        /// </summary>
        /// <param name="token">Parsed token to append</param>
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken)
            {
                var factory = SyntaxParserConfig.GetConfig().SymbolFactory;
                if (!context.AddingToNonTerminal)
                {
                    context.NonTerminal = new Structures.NonTerminal(factory.Create(""), automatonToken.Line);
                    context.AddingToNonTerminal = true;
                }
                context.NonTerminal?.Symbol = factory.Create(context.NonTerminal.Symbol.Name + automatonToken.Symbol.Name);
            }
        }
    }

}
