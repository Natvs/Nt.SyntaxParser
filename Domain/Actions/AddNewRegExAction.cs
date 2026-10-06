using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Structures;
using Nt.Syntax.Builders;

namespace Nt.Syntax.Actions
{
    public class AddNewRegExAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
        public void Perform(IAutomatonToken<string> word)
        {
            if (word is AutomatonToken token) 
            {
                context.Regex = new RegularExpression(grammar);
                context.Regex.GetBuilder().SetToken(new NonTerminal(token.Symbol, token.Line));
                grammar.RegularExpressions.Add(context.Regex);               
            }
        }
    }
}
