using Nt.Parser.Symbols;
using Nt.Syntax.Automaton;
using Nt.Syntax.Actions;
using Nt.Parser.Structures;

namespace Nt.Tests.Syntax.Actions
{
    public class AddImportPathActionTest
    {
        private SymbolFactory SymbolFactory = new SymbolFactory();

        [Fact]
        public void AddImportPathAction_Test1()
        {
            var tokens = new SymbolsList(SymbolFactory, ["dir"]);
            var context = new AutomatonContext();
            var readAction = new AppendToCurrentImportPathAction(context);
            var setAction = new AddImportPathAction(context);
            readAction.Perform(new AutomatonToken(tokens.Get(0), 0));
            setAction.Perform(new AutomatonToken(new Symbol(""), 0));

            var path = context.GetPath();
            Assert.Single(path);
            Assert.Contains("dir", path);
        }

        [Fact]
        public void AddImportPathAction_Test2()
        {
            var tokens = new SymbolsList(SymbolFactory, ["dir1", "dir2", "dir3"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentImportPathAction(context);
            var setAction = new AddImportPathAction(context);
            for (int i = 0; i < tokens.GetCount(); i++)
            {
                readAction.Perform(new AutomatonToken(tokens.Get(i), 0));
                setAction.Perform(new AutomatonToken(new Symbol(""), 0));
            }

            var path = context.GetPath();
            Assert.Equal(tokens.GetCount(), path.Count);
            for (int i = 0; i < tokens.GetCount(); i++) Assert.Contains(tokens.Get(i).Name, path);
        }
    }
}
