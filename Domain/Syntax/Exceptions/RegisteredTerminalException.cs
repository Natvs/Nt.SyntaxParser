namespace Nt.Syntax.Exceptions
{

    public class RegisteredTerminalException : InternalException
    {

        public RegisteredTerminalException(string name) : base($"Terminal {name} is already registered") { }

    }
}
