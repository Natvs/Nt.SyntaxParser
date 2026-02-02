using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that sets grammar axiom
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="tokens">List of all tokens</param>
    public class SetAxiomAction(Structures.Grammar grammar) : IAction<string>
    {
        /// <summary>
        /// Sets a parsed token as new axiom of the grammar. Axiom should be a valid non terminal.
        /// </summary>
        /// <param name="word">Parsed token to add as new terminal</param>
        public void Perform(IAutomatonToken<string> word)
        {
            try
            {
                if (word is AutomatonToken token)
                {
                    var new_token = grammar.ParseToGrammarToken(token);
                    if (new_token is NonTerminal nt)
                    {
                        grammar.SetAxiom(nt);
                    }
                    else throw new UnregisteredNonTerminalException(new_token.Name, new_token.Line);
                }
            }
            catch (UnknownSymbolException ex)
            {
                throw new UnregisteredNonTerminalException(ex.Name, ex.Line);
            }
        }

    }
}
