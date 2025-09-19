namespace GrammarParser.Syntax.Exceptions
{
    public class EndOfStringException : Exception
    {
       public EndOfStringException() : base("Unexpected end of grammar string. Some symbols should be missing.") { }
    }
}
