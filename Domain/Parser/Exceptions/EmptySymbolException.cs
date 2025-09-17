namespace GrammarReader.Domain.Parser.Exceptions
{
    public class EmptySymbolException : Exception
    {

        public EmptySymbolException() : base("Symbol should not be empty") { }

    }
}
