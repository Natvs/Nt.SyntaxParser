namespace GrammarReader.Code.Class
{
    /// <summary>
    /// Represents a token. A token is just a word.
    /// </summary>
    public class Token
    {
        public string Name { get; private set; }

        public Token(string name)
        {
            this.Name = name;
        }

    }
}
