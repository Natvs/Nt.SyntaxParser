using GrammarReader.Code.Grammar.Actions;
using GrammarReader.Code.Grammar.Structures;
using GrammarReader.Code.Parser.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Grammar
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

    public class AutomatonContext
    {
        public Rule? Rule { get; set; }
        public RegularExpression? RegularExpression { get; set; }
        public string? ImportedString { get; set; }
    }
}
