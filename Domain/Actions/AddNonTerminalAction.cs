using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Builders;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that adds new non terminal
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="tokens">List of all tokens</param>
    internal class AddNonTerminalAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
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

                    var name = grammar.RemoveEscapeCharacter(automatonToken.Value);
                    grammar.GetBuilder().AddNonTerminal(name, automatonToken.Line);
                    context.NonTerminal = grammar.NonTerminals.Get((nt) => nt.Name == name);
                }
            }
            catch (RegisteredNonTerminalException) { } // Ignore those 2 exceptions for now
            catch (RegisteredTerminalException) { } // Maybe someone should remove them later
        }
    }

}
