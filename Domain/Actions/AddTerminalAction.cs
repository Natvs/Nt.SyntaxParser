using Nt.Automaton.Actions;
using Nt.Automaton.Automatons;
using Nt.Automaton.Tokens;
using Nt.Parser.Structures;
using Nt.Parser.Symbols;
using Nt.Syntax.Automaton;
using Nt.Syntax.Builders;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that adds a new terminal to the grammar
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    public class AddTerminalAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
        /// <summary>
        /// Adds a parsed token as a new terminal of the grammar
        /// </summary>
        /// <param name="token">Parsed token to add as new terminal</param>
        public void Perform(IAutomatonToken<string> token)
        {
            try
            {
                if (token is AutomatonToken automatonToken)
                {
                    if (context.Terminal == null)
                    {
                        throw new SyntaxError(token.Value, automatonToken.Line);
                    }
                    var name = grammar.RemoveEscapeCharacter(context.Terminal.Symbol.Name);
                    grammar.GetBuilder().AddTerminal(name, automatonToken.Line);
                }
            }
            catch (RegisteredNonTerminalException) { }
            catch (RegisteredTerminalException) { }

            context.AddingToTerminal = false;
        }
    }
}
