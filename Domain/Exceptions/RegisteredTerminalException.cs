namespace Nt.Syntax.Exceptions
{

    public class RegisteredTerminalException(string name) : Exception($"Terminal {name} is already registered")
    {
        public string Name { get; } = name;
    }
}
