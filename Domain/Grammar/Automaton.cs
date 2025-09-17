using GrammarReader.Domain.Parser.Structures;

namespace GrammarReader.Domain.Grammar
{

    /// <summary>
    /// Represents an automaton
    /// </summary>
    /// <param name="tokens">List of tokens that this automaton can read</param>
    /// <param name="initialState">Initial state of the automaton</param>
    public class Automaton(TokensList tokens, State initialState)
    {
        public State CurrentState { get; private set; } = initialState;

        /// <summary>
        /// Reads a token from the current state and updates current state to the next state
        /// </summary>
        /// <param name="token">Parsed token to read</param>
        public void Read(ParsedToken token, AutomatonContext context)
        {
            if (CurrentState == null) { return; }
            CurrentState = CurrentState.Read(token, tokens, context);
        }
    }
}
