namespace Nt.SyntaxParser.Syntax.Exceptions
{
    public class NoDefaultStateException : Exception
    {
        public NoDefaultStateException() : base("Default state is not defined") { }
    }
}
