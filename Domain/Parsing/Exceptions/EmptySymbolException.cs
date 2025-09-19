namespace GrammarParser.Parsing.Exceptions
{
    public class EmptySymbolException : Exception
    {

        public EmptySymbolException() : base("Symbol should not be empty") { }

    }
}
