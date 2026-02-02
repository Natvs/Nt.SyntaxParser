namespace Nt.Syntax.Exceptions
{
    public class RegisteredNonTerminalException : InternalException
    {
        public RegisteredNonTerminalException(string name) : base($"Non terminal {name} is already registered") { }
    }
}
