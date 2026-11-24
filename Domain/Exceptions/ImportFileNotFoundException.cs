namespace Nt.Syntax.Exceptions
{
    public class ImportFileNotFoundException(string fileName) : Exception($"The file {fileName} you are trying to import does not exists")
    {
        public string FileName { get; } = fileName;
    }
}
