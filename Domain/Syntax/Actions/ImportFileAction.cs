using Microsoft.VisualBasic.FileIO;
using System.Text;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Automaton;
using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Reads the performed token as a file name and gets its content
    /// </summary>
    public class ImportFileAction(AutomatonContext context) : IAction<string>
    {
        public void Perform(IAutomatonToken<string> word)
        {
            var sb = new StringBuilder();

            string fileName = context.CurrentImportFile;
            context.CurrentImportFile = "";

            foreach (string path in context.GetPath())
            {
                if (FileSystem.FileExists(path + "/" + fileName))
                {
                    sb.Append(File.ReadAllText(path + "/" + fileName));
                    context.ImportedString = sb.ToString();
                    return;
                }
            }
            if (!FileSystem.FileExists(fileName)) throw new ImportFileNotFoundException(fileName);
            sb.Append(File.ReadAllText(fileName));
            context.ImportedString = sb.ToString();
        }
    }

}
