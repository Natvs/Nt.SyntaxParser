using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Applications.SyntaxParser.Actions.IntermediatesStates;
using Nt.Applications.SyntaxParser.Actions.FinalStates;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class GrammarEditor(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            var state = base.GetState().SetFinal().OnCondition(() => Context.Grammar == null);
            var nonTerminalsState = new EditNonTerminals(Context).GetState();
            var termianlsState = new EditTerminals(Context).GetState();
            var axiomState = new AxiomSetter(Context).GetState();
            var rulesState = new EditRules(Context).GetState();
            var regexState = new EditRegexs(Context).GetState();

            state.AddTransition(new Transition<string>("1", nonTerminalsState));
            state.AddTransition(new Transition<string>("2", termianlsState));
            state.AddTransition(new Transition<string>("3", axiomState));
            state.AddTransition(new Transition<string>("4", rulesState));
            state.AddTransition(new Transition<string>("5", regexState));

            return state;
        }

        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                Clear();
                Console.WriteLine("Current grammar:");
                PrintGrammar();

                Console.WriteLine("1. Edit list of non terminals");
                Console.WriteLine("2. Edit list of terminals");
                Console.WriteLine("3. Edit axiom");
                Console.WriteLine("4. Edit rules");
                Console.WriteLine("5. Edit regular expressions");
                Console.WriteLine("6. Exit");
            }
        }
    }
}
