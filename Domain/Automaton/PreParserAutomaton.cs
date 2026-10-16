using Nt.Syntax.Actions;
using State = Nt.Automaton.States.State<string>;
using StateAutomaton = Nt.Automaton.Automatons.StateAutomaton<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;

namespace Nt.Syntax.Automaton
{
    internal class PreParserAutomaton : BaseAutomaton
    {

        // Generation
        protected override void Build()
        {
            var initial = new State(); initial.SetDefault(new Transition(initial));
            Context.Reset();

            Automaton = new StateAutomaton(initial);

            var addToPathState = new State();
            var importState = new State();
            var escapeState = new State().SetDefault(new Transition(initial).SetAction(new SetEscapeCharAction(Grammar)));

            initial.AddTransition(new Transition("import", importState));
            initial.AddTransition(new Transition("IMPORT", importState));
            importState.SetDefault(new Transition(importState).SetAction(new AppendToCurrentImportFileAction(Context)));
            importState.AddTransition(new Transition(";", initial).SetAction(new ImportFileAction(Context)));

            initial.AddTransition(new Transition("addtopath", addToPathState));
            initial.AddTransition(new Transition("ADDTOPATH", addToPathState));
            addToPathState.SetDefault(new Transition(addToPathState).SetAction(new AppendToCurrentImportPathAction(Context)));
            addToPathState.AddTransition(new Transition(";", initial).SetAction(new AddImportPathAction(Context)));

            initial.AddTransition(new Transition("ESCAPE", escapeState));
            initial.AddTransition(new Transition("escape", escapeState));
        }

        // Internal methods
        internal string? GetImportedString()
        {
            return Context.ImportedString;
        }

        internal void ResetImportedString()
        {
            Context.ImportedString = null;
        }

    }
}
