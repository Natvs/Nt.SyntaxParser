namespace Nt.Syntax.Exceptions
{
    public class ExpectedException(int line, string expected, string got) : Exception($"Line {line}: Expected {expected} but got {got}")
    {

    }

}
