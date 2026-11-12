using Nt.Syntax.Structures;

namespace Nt.Syntax.Branching.Exceptions
{
    public class UninitializedDataException : Exception
    {
        public UninitializedDataException(GrammarToken token, Type type) : base($"No data of type {type.Name} found for token {token}.")
        {
        }

        public UninitializedDataException(Rule rule, Type type) : base($"No data of type {type.Name} found for rule {rule}.")
        {
        }

        public UninitializedDataException(RegularExpression regex, Type type) : base($"No data of type {type.Name} found for regular expression {regex}.")
        {
        }

    }
}
