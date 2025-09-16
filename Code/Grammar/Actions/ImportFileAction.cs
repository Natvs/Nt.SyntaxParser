using GrammarReader.Code.Parser.Structures;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using GrammarReader.Code.Grammar.Exceptions;

namespace GrammarReader.Code.Grammar.Actions
{
    public class ImportFileAction(TokensList tokens): ImportAction
    {
        public override string Perform(ParsedToken word)
        {
            var sb = new StringBuilder();

            var fileName = tokens[word.TokenIndex].Name;
            if (!FileSystem.FileExists(fileName)) throw new ImportFileNotFoundException(fileName);
            sb.Append(File.ReadAllText(fileName));
            return sb.ToString();
        }
    }

}
