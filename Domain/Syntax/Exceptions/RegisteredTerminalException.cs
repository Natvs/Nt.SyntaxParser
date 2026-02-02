namespace Nt.Syntax.Exceptions
{

    public class RegisteredTerminalException(string name) : InternalException($"Terminal {name} is already registered")
    {
        public string Name { get; } = name;
    }
}
