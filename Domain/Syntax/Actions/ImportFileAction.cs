using Microsoft.VisualBasic.FileIO;
using System.Text;
using Nt.Syntax.Exceptions;
using Nt.Parser.Structures;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Reads the performed token as a file name and gets its content
    /// </summary>
    /// <param name="tokens">List of unique parsed tokens</param>
    public class ImportFileAction(AutomatonContext context) : IAction
    {
        public string Perform(ParsedToken word)
        {
            var sb = new StringBuilder();

            string fileName = context.CurrentImportFile;
            context.CurrentImportFile = "";

            foreach (string path in context.GetPath())
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
