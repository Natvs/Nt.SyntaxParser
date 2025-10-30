using System.Text;

namespace Nt.Syntax.Exceptions
{
    public class SyntaxError : Exception
    {

        public SyntaxError(string name, int line) : base($"Syntax error in grammar at line {line}: unknown symbol {name}") { }

        private static string ASCIIComponents(string name)
        {
            var sb = new StringBuilder();
            foreach (char c in name)
            {
                sb.Append((int)c).Append(' ');
            }
            return sb.ToString();
        }
    }
}
