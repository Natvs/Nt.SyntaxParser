using Nt.Automaton.Transitions.Strings;
using Nt.Syntax.Actions.PreParsing;
using State = Nt.Automaton.States.State<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;

namespace Nt.Syntax.Automaton
{
    internal class PreParserAutomaton : BaseAutomaton
    {
        internal static List<string> Commands { get; } = ["import", "addtopath", "escape", "ignore_unknown"];

        // Generation
        protected override void Build()
        { 
            InitialState.SetDefault(new Transition(InitialState));

            var importState = new State()
                        .SetDefault(new Transition(InitialState).SetAction(new ImportFileAction(Context)));
            InitialState.AddTransition(new StringTransition(importState).AddPattern("[Ii]mport|IMPORT"));

            var addToPathState = new State()
                        .SetDefault(new Transition(InitialState).SetAction(new AddImportPathAction(Context)));
            InitialState.AddTransition(new StringTransition(addToPathState).AddPattern("(([Aa]dd|ADD)_?([Tt]o|TO)_?)?([Pp]ath|PATH)"));

            var escapeState = new State()
                        .SetDefault(new Transition(InitialState).SetAction(new SetEscapeCharAction(Grammar)));
            InitialState.AddTransition(new StringTransition(escapeState).AddPattern("[Ee]scape|ESCAPE"));

            InitialState.AddTransition(new StringTransition(InitialState).AddPattern("([Ii]gnore|IGNORE)_?([Uu]nknown|UNKNOWN)_?([Bb]locks|BLOCKS)?").SetAction(new IgnoreUnknownBlocksAction(Context)));
        }

    }
}
