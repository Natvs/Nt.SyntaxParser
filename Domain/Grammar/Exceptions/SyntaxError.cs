using System.Text;

namespace GrammarParser.Domain.Grammar.Exceptions
{
    public class SyntaxError : Exception
    {

        public SyntaxError(string name, int line) : base($"Syntax error in grammar at line {line}: unknown symbol {name} ( {ASCIIComponents(name)})") { }

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
