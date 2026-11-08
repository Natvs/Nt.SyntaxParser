namespace Nt.Syntax.Branching.Exceptions
{
    public class UninitializedDataException(object obj, Type dataType) : Exception($"The data of type `{dataType}` has not been initialized for the element `{obj}`.")
    {
    }
}
