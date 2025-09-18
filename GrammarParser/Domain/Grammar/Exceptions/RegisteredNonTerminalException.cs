namespace GrammarParser.Domain.Grammar.Exceptions
{
    public class RegisteredNonTerminalException : Exception
    {

        public RegisteredNonTerminalException(string name) : base($"Non terminal {name} is already registered") { }
    }
}
