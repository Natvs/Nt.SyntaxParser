using GrammarParser.Domain.Parser.Structures;

namespace GrammarParser.Domain.Grammar.Actions
{
    /// <summary>
    /// Represents an action that sets grammar axiom
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="tokens">List of all tokens</param>
    public class SetAxiomAction(Structures.Grammar grammar, TokensList tokens) : Action
    {
        /// <summary>
        /// Sets a parsed token as new axiom of the grammar. Axiom should be a valid non terminal.
        /// </summary>
        /// <param name="word">Parsed token to add as new terminal</param>
        public override void Perform(ParsedToken word)
        {
            grammar.SetAxiom(tokens[word.TokenIndex].Name);
        }

    }
}
