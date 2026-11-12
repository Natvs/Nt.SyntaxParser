using Nt.Syntax.Structures;

namespace Nt.Syntax.Branching.Exceptions
{
    public class IncorrectDataTypeException : Exception
    {
        public IncorrectDataTypeException(GrammarToken token, Type expected, Type actual) : base($"Data type is incorrect for token {token}. Expected {expected.Name}, but got {actual.Name}.")
        {
        }

        public IncorrectDataTypeException(Rule rule, Type expected, Type actual) : base($"Data type is incorrect for rule {rule}. Expected {expected.Name}, but got {actual.Name}.")
        {
        }

        public IncorrectDataTypeException(RegularExpression regex, Type expected, Type actual) : base($"Data type is incorrect for regular expression {regex}. Expected {expected.Name}, but got {actual.Name}.")
        {
        }
    }
}
