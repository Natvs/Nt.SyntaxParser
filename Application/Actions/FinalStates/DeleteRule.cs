using Nt.Automaton.States;
using Nt.Syntax.Builders;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions.FinalStates
{
    internal class DeleteRule(ApplicationContext context, Rule rule) : ProgramAction(context)
    {
        private Rule Rule { get; set; } = rule;
        public override IState<string> GetState()
        {
            return base.GetState().SetFinal();
        }
        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                grammar.GetBuilder().Remove(Rule);
                Console.WriteLine($"Rule {Rule} has been removed");
                Context.Automaton.Pop();
            }
        }
    }
}