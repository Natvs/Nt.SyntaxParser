namespace Nt.SyntaxParser.Parsing.Exceptions
{
    public class EmptySymbolException : Exception
    {

        public EmptySymbolException() : base("Symbol should not be empty") { }

    }
}
