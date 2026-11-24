using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Structures;
using Nt.Syntax.Builders;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that sets grammar axiom
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="tokens">List of all tokens</param>
    internal class SetAxiomAction(Grammar grammar) : ITokenAction<string>
    {
        /// <summary>
        /// Sets a parsed token as new axiom of the grammar. Axiom should be a valid non terminal.
        /// </summary>
        /// <param name="token">Parsed token to add as new terminal</param>
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken)
            {
                var name = grammar.RemoveEscapeCharacter(automatonToken.Value);
                grammar.GetBuilder().SetAxiom(name, automatonToken.Line);
            }
        }

    }
}
