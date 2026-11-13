using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Structures;
using Nt.Syntax.Builders;
using Nt.Syntax.Exceptions;

namespace Nt.Syntax.Actions
{
    public class AddNewRuleAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
        /// <summary>
        /// Adds the symbol of a rule
        /// </summary>
        /// <param name="token">The token read</param>
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken)
            {
                context.Rule = new Rule(grammar);

                try
                {
                    var axiom = grammar.ParseToGrammarToken(automatonToken);
                    if (axiom is NonTerminal nonterminal)
                    {
                        context.Rule.GetBuilder().SetToken(nonterminal);
                        grammar.Rules.Add(context.Rule);
                    }
                    else
                    {
                        throw new AxiomException(automatonToken.Symbol.Name, automatonToken.Line);
                    }
                }
                catch (UnknownSymbolException)
                {
                    throw new UnregisteredNonTerminalException(automatonToken.Symbol.Name, automatonToken.Line);
                }

            }
        }
    }
}
