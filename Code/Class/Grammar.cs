using GrammarReader.Code.Grammar.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Class
{
    public class Grammar
    {

        public TokensList Terminals { get; } = [];
        public TokensList NonTerminals { get; } = [];
        public int Axiom { get; private set; } = -1;

        public List<Rule> Rules { get; } = [];

        #region Public Methods

        public void AddTerminal(string name)
        {
            if (Terminals.Contains(name)) throw new RegisteredTerminalException(name);
            Terminals.Add(name);
        }

        public void AddNonTerminal(string name)
        {
            if (NonTerminals.Contains(name)) throw new RegisteredNonTerminalException(name);
            NonTerminals.Add(name);
        }

        public void SetAxiom(string name)
        {
            if (!NonTerminals.Contains(name)) throw new NotDeclaredNonTerminalException(name);
            Axiom = NonTerminals.IndexOf(name);
        }

        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Terminals: ").Append(Terminals.ToString()).Append('\n');
            sb.Append("Non terminals: ").Append(NonTerminals.ToString()).Append('\n');
            if (Axiom > -1) sb.Append("Axiom: ").Append(NonTerminals[Axiom].Name).Append('\n');

            return sb.ToString();
        }
    }
}
