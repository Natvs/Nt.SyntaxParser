namespace Nt.Syntax.Exceptions
{
    public class NullRegexException : InternalException
    {
        public NullRegexException(string message) : base(message) { }
    }
}
