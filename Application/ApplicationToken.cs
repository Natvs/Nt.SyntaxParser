using Nt.Automaton.Tokens;

namespace Nt.Applications.SyntaxParser
{
    internal class ApplicationToken(string name): IAutomatonToken<string>
    {
        string IAutomatonToken<string>.Value => name;
    }

}
