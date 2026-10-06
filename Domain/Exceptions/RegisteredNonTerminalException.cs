namespace Nt.Syntax.Exceptions
{
    public class RegisteredNonTerminalException(string name) : InternalException($"Non terminal {name} is already registered")
    {
        public string Name { get; } = name;
    }
}
