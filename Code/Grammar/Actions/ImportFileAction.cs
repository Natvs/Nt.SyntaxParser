using GrammarReader.Code.Parser.Structures;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using GrammarReader.Code.Grammar.Exceptions;
using GrammarReader.Code.Grammar.Structures;

namespace GrammarReader.Code.Grammar.Actions
{
    /// <summary>
    /// Reads the performed token as a file name and gets its content
    /// </summary>
    /// <param name="tokens">List of unique parsed tokens</param>
    public class ImportFileAction(TokensList tokens, ImportPath importPath): IAction
    {
        public string Perform(ParsedToken word)
        {
            var sb = new StringBuilder();

            var fileName = tokens[word.TokenIndex].Name;
            foreach (var path in importPath.Path)
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

    public class AddImportPathAction(TokensList tokens, ImportPath path): IAction
    {
        public void Perform(ParsedToken word)
        {
            path.Path.Add(tokens[word.TokenIndex].Name);   
        }
    }

}
