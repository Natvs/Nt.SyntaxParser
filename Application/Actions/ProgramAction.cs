using Nt.Automaton.Actions;
using Nt.Automaton.States;
using Nt.Syntax.Structures;
using Nt.Syntax.Exportation;
using System.Diagnostics.CodeAnalysis;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal abstract class ProgramAction(ApplicationContext context) : IAction
    {
        protected ApplicationContext Context { get; } = context;

        private static void Transition()
        {
            Console.WriteLine();
            Console.WriteLine("- - - - - - - - - -");
            Console.WriteLine();
        }

        protected static void Clear()
        {
            Transition();
            Console.Clear();
        }

        protected bool GetGrammar([NotNullWhen(true)] out Grammar? grammar)
        {
            if (Context.Grammar == null)
            {
                Transition();
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                Transition();

                grammar = default;
                return false;
            }
            grammar = Context.Grammar;
            return true;
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
