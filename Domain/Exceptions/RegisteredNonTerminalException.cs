namespace Nt.Syntax.Exceptions
{
    public class RegisteredNonTerminalException(string name) : Exception($"Non terminal {name} is already registered")
    {
        public string Name { get; } = name;
    }
}
