using Nt.Syntax.Structures;

namespace Nt.Syntax.Exceptions
{
    public class RegexNotFoundException(RegularExpression regex, string message) : Exception(message)
    {
        public RegularExpression Regex { get; } = regex;
    }
}
