using Nt.Automaton.Tokens;

namespace Nt.SyntaxParser.Automaton
{
    internal class ApplicationToken(string name): IAutomatonToken<string>
    {
        string IAutomatonToken<string>.Value => name;
    }

}
