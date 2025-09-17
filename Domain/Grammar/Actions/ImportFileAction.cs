using GrammarReader.Domain.Grammar.Exceptions;
using GrammarReader.Domain.Grammar.Structures;
using GrammarReader.Infrastructure.Parser.Structures;
using Microsoft.VisualBasic.FileIO;
using System.Text;

namespace GrammarReader.Domain.Grammar.Actions
{
    /// <summary>
    /// Reads the performed token as a file name and gets its content
    /// </summary>
    /// <param name="tokens">List of unique parsed tokens</param>
    public class ImportFileAction(TokensList tokens, ImportPath importPath) : IAction
    {
        public string Perform(ParsedToken word)
        {
            var sb = new StringBuilder();

            string fileName = tokens[word.TokenIndex].Name;
            foreach (string path in importPath.Path)
            {
                if (FileSystem.FileExists(path + "/" + fileName))
                {
                    sb.Append(File.ReadAllText(path + "/" + fileName));
                    return sb.ToString();
                }
            }
            if (!FileSystem.FileExists(fileName)) throw new ImportFileNotFoundException(fileName);
            sb.Append(File.ReadAllText(fileName));
            return sb.ToString();
        }
    }

}
