using Nt.Automaton.States;
using Nt.Syntax.Actions;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using State = Nt.Automaton.States.State<string>;
using StateAutomaton = Nt.Automaton.Automatons.StateAutomaton<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;

namespace Nt.Syntax.Automaton
{
    public class PreParserAutomaton
    {
        private StateAutomaton? Automaton { get; set; }
        private AutomatonContext Context { get; set; } = new AutomatonContext();
        private Grammar Grammar { get; set; }

        public PreParserAutomaton(Grammar grammar)
        {
            this.Grammar = grammar;
            GeneratePreAutomaton();
        }

        #region Private

        /// <summary>
        /// Initializes the pre-automaton structure used for parsing pre-parsing instructions.
        /// </summary>
        /// <exception cref="EndOfStringException">The pre-automaton might end on a state different from the initial state</exception>
        private void GeneratePreAutomaton()
        {
            var initial = new State(); initial.SetDefault(initial);
            Context.Reset();

            Automaton = new StateAutomaton(initial);

            State<string> addToPathState = new();
            State<string> importState = new();
            State<string> escapeState = new State().SetDefault(initial, new SetEscapeCharAction(Grammar));

            initial.AddTransition(new Transition("import", importState));
            initial.AddTransition(new Transition("IMPORT", importState));
            importState.SetDefault(importState, new AppendToCurrentImportFileAction(Context));
            importState.AddTransition(new Transition(";", initial, new ImportFileAction(Context)));

            initial.AddTransition(new Transition("addtopath", addToPathState));
            initial.AddTransition(new Transition("ADDTOPATH", addToPathState));
            addToPathState.SetDefault(addToPathState, new AppendToCurrentImportPathAction(Context));
            addToPathState.AddTransition(new Transition(";", initial, new AddImportPathAction(Context)));

            initial.AddTransition(new Transition("ESCAPE", escapeState));
            initial.AddTransition(new Transition("escape", escapeState));
        }


        #endregion

        #region Internal

        internal string? GetImportedString()
        {
            return Context.ImportedString;
        }

        internal void ResetImportedString()
        {
            Context.ImportedString = null;
        }

        internal void Read(AutomatonToken token)
        {
            Automaton?.Read(token);
        }

        #endregion
    }
}
