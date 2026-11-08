using Nt.Automaton.Actions;
using Nt.Automaton.States;
using Nt.Syntax.Structures;
using Nt.Syntax.Exportation;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal abstract class ProgramAction(ApplicationContext context) : IAction
    {
        protected ApplicationContext Context { get; } = context;

        protected static void Transition()
        {
            Console.WriteLine();
            Console.WriteLine("- - - - - - - - - -");
            Console.WriteLine();
        }

        protected void PrintGrammar()
        {
            if (Context.Grammar == null)
            {
                Console.WriteLine("No grammar loaded.");
                return;
            }
            Console.WriteLine();
            Console.WriteLine(Context.Grammar?.ToString(ExportationMode.Grouped));
            Console.WriteLine();
        }

        protected static void Save(Grammar grammar, string path)
        {
            File.WriteAllText("grammars/" + path, grammar.Save(ExportationMode.Original));
        }

        public virtual IState<string> GetState()
        {
            return new State<string>().SetAction(this);
        }

        public abstract void Perform();
    }
}
