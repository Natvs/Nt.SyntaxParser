using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Builders;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    public class AddNewRegExAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken) 
            {
                context.Regex = new RegularExpression(grammar);

                try
                {
                    var axiom = grammar.ParseToGrammarToken(automatonToken);
                    if (axiom is NonTerminal nonterminal)
                    {
                        context.Regex.GetBuilder().SetToken(nonterminal);
                        grammar.RegularExpressions.Add(context.Regex);
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
