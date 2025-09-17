namespace GrammarParser.Domain.Grammar.Exceptions
{
    public class ImportFileNotFoundException : Exception
    {
        public ImportFileNotFoundException(string fileName) : base($"The file {fileName} you are trying to import does not exists") { }
    }
}
