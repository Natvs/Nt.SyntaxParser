namespace Nt.Syntax.Exceptions
{
    public class EndOfStringException() : InternalException("Unexpected end of grammar string. Some symbols should be missing.")
    {
    }
}
