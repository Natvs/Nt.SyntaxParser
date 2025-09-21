namespace Nt.SyntaxParser.Syntax.Exceptions
{

    public class RegisteredTerminalException : Exception
    {

        public RegisteredTerminalException(string name) : base($"Terminal {name} is already registered") { }

    }
}
