namespace GrammarParser.Syntax.Exceptions
{

    public class RegisteredTerminalException : Exception
    {

        public RegisteredTerminalException(string name) : base($"Terminal {name} is already registered") { }

    }
}
