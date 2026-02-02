namespace Nt.Syntax.Exceptions
{
    public class NoDefaultStateException : InternalException
    {
        public NoDefaultStateException() : base("Default state is not defined") { }
    }
}
