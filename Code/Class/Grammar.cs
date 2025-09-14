using GrammarReader.Code.Grammar.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Class
{
    /// <summary>
    /// Represents datas of a language grammar
    /// </summary>
    public class Grammar
    {

        public TokensList Terminals { get; } = [];
        public TokensList NonTerminals { get; } = [];
        public int Axiom { get; private set; } = -1;

        public List<Rule> Rules { get; } = [];

        #region Public Methods

        /// <summary>
        /// Adds a new terminal to the terminals list of this grammar
        /// </summary>
        /// <param name="name">Name of the terminal to add</param>
        /// <exception cref="RegisteredTerminalException">The terminal may already exists in the list of terminals</exception>
        public void AddTerminal(string name)
        {
            if (Terminals.Contains(name)) throw new RegisteredTerminalException(name);
            if (NonTerminals.Contains(name)) throw new RegisteredNonTerminalException(name);
            Terminals.Add(name);
        }
        /// <summary>
        /// Gets index of a terminal in this grammar
        /// </summary>
        /// <param name="name">Name of the terminal</param>
        /// <returns>Index of the terminal in the grammar terminals list</returns>
        /// <exception cref="NotDeclaredTerminalException">The terminal may not exists in the list</exception>
        public int GetTerminalIndex(string name)
        {
            for (int i = 0; i < Terminals.Count; i++) 
            {
                if ((Terminals[i].Name == name)) return i;
            }
            throw new NotDeclaredTerminalException(name);
        }

        /// <summary>
        /// Adds a new non terminal to the non terminals list of this grammar
        /// </summary>
        /// <param name="name">Name of the non terminal to add</param>
        /// <exception cref="RegisteredNonTerminalException">The non terminal may already exists in the list of non terminals</exception>
        public void AddNonTerminal(string name)
        {
            if (NonTerminals.Contains(name)) throw new RegisteredNonTerminalException(name);
            if (Terminals.Contains(name)) throw new RegisteredTerminalException(name);
            NonTerminals.Add(name);
        }
        /// <summary>
        /// Gets index of a non terminal in this grammar
        /// </summary>
        /// <param name="name">Name of the non terminal</param>
        /// <returns>Index of the non terminal in the grammar non terminals list</returns>
        /// <exception cref="NotDeclaredTerminalException">The non terminal may not exists in the list</exception>
        public int GetNonTerminalIndex(string name)
        {
            for (int i = 0; i < NonTerminals.Count; i++)
            {
                if ((NonTerminals[i].Name == name)) return i;
            }
            throw new NotDeclaredNonTerminalException(name);
        }

        /// <summary>
        /// Sets the axiom of this grammar
        /// </summary>
        /// <param name="name">Name of the new axiom</param>
        /// <exception cref="NotDeclaredNonTerminalException">The axiom might not exist in the non terminals list of this grammar</exception>
        public void SetAxiom(string name)
        {
            if (!NonTerminals.Contains(name)) throw new NotDeclaredNonTerminalException(name);
            Axiom = NonTerminals.IndexOf(name);
        }

        public Rule AddRule(string nonTerminal, int line)
        {
            var rule = new Rule(Terminals, NonTerminals);
            Rules.Add(rule);
            try { 
                rule.SetToken(NonTerminals.IndexOf(nonTerminal), line);
                return rule;
            }
            catch (KeyNotFoundException) { Console.WriteLine("KeyNotFoundException detected!!!"); throw new NotDeclaredNonTerminalException(nonTerminal); }
        }
        public Rule AddRule(int nonTerminalIndex, int line)
        {
            var rule = new Rule(Terminals, NonTerminals);
            Rules.Add(rule);
            rule.SetToken(nonTerminalIndex, line);
            return rule;
        }

        #endregion

        /// <summary>
        /// Gets a string containing datas about the grammar
        /// </summary>
        /// <returns>A string representing this grammar</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Terminals: ").Append(Terminals.ToString()).Append('\n');
            sb.Append("Non terminals: ").Append(NonTerminals.ToString()).Append('\n');
            if (Axiom > -1) sb.Append("Axiom: ").Append(NonTerminals[Axiom].Name).Append('\n');

            if (Rules.Count > 0) sb.Append("\nRules\n");
            foreach (var rule in Rules)
            {
                sb.Append(rule.ToString()).Append('\n');
            }

            return sb.ToString();
        }
    }
}
