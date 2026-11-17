namespace Nt.Syntax.Exceptions
{
    public class EndOfStringException() : Exception("Unexpected end of grammar string. Some symbols should be missing.")
    {
    }
}
