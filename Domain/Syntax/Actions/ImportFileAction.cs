using Microsoft.VisualBasic.FileIO;
using System.Text;
using Nt.Syntax.Exceptions;
using Nt.Parsing.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
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
