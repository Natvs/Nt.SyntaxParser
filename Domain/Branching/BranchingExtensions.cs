using Nt.Automaton.States;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Nt.Syntax.Branching
{
    public static class BranchingExtensions
    {

        /// <summary>
        /// Begin a new branch when a specified token is read.
        /// </summary>
        /// <param name="token">Token to read to jump on the new branch</param>
        /// <param name="state">State the automaton jumps to after reading the token</param>
        public static void StartBranch(this SyntaxParser parser, string token, IState<string> state)
        {
            parser.Automaton?.StartBranch(token, state);
        }

        /// <summary>
        /// End a branch by linking it to the default state
        /// </summary>
        /// <param name="state">Terminal state of the branch to link to the default state</param>
        /// <param name="token">Token to read to trigger the linking</param>
        public static void EndBranch(this SyntaxParser parser, IState<string> state, string token)
        {
            parser.Automaton?.EndBranch(state, token);
        }


    }
}
