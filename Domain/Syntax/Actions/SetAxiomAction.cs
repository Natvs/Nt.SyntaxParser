using Nt.Parser.Structures;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that sets grammar axiom
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="tokens">List of all tokens</param>
    public class SetAxiomAction(Structures.Grammar grammar) : Action
    {
        /// <summary>
        /// Sets a parsed token as new axiom of the grammar. Axiom should be a valid non terminal.
        /// </summary>
        /// <param name="word">Parsed token to add as new terminal</param>
        public override void Perform(ParsedToken word)
        {
            try
            {
                var new_token = grammar.ParseToGrammarToken(word);
                if (new_token is NonTerminal nt)
                {
                    grammar.SetAxiom(nt);
                }
                else throw new NotDeclaredNonTerminalException(new_token.Name, new_token.Line);
            }
            catch (UnknownSymbolException ex)
            {
                throw new NotDeclaredNonTerminalException(ex.TokenName, ex.Line);
            }
        }

    }
}
