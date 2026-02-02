namespace Nt.Syntax.Exceptions
{
    public class ImportFileNotFoundException(string fileName) : InternalException($"The file {fileName} you are trying to import does not exists")
    { }
}
