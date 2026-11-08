using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Structures;
using Nt.Syntax.Builders;

namespace Nt.Syntax.Actions
{
    public class AddNewRegExAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken) 
            {
                context.Regex = new RegularExpression(grammar);
                context.Regex.GetBuilder().SetToken(new NonTerminal(automatonToken.Symbol, automatonToken.Line));
                grammar.RegularExpressions.Add(context.Regex);               
            }
        }
    }
}
