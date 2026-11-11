using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using Nt.Syntax.Builders;
using Nt.Parser.Symbols;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that adds new non terminal
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="tokens">List of all tokens</param>
    public class AddNonTerminalAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
        /// <summary>
        /// Adds a parsed token as new non terminal of the grammar
        /// </summary>
        /// <param name="token">Parsed token to add as new non terminal</param>
        public void Perform(IAutomatonToken<string> token)
        {
            try
            {
                if (token is AutomatonToken automatonToken)
                {
                    if (context.NonTerminal == null)
                    {
                        throw new SyntaxError(token.Value, automatonToken.Line);
                    }
                    var name = grammar.RemoveEscapeCharacter(context.NonTerminal.Symbol.Name);
                    grammar.GetBuilder().AddNonTerminal(name, automatonToken.Line);
                }
            }
            catch (RegisteredNonTerminalException) { } // Ignore those 2 exceptions for now
            catch (RegisteredTerminalException) { } // Maybe someone should remove them later

            context.AddingToNonTerminal = false;
        }
    }

}
