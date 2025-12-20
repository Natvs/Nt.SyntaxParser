namespace Nt.Parsing.Structures
{
    /// <summary>
    /// Represents a token. A token is just a word.
    /// </summary>
    public class Symbol
    {
        public string Name { get; private set; }

        public Symbol(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;

    }
}
