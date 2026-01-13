using Nt.Syntax.Structures;
using Nt.Parsing.Structures;

namespace Nt.Syntax.Actions.Tests
{
    public class AddImportPathActionTest
    {
        [Fact]
        public void AddImportPathAction_Test1()
        {
            var tokens = new SymbolsList(["dir"]);
            var context = new AutomatonContext();
            var readAction = new AppendToCurrentImportPathAction(tokens, context);
            var setAction = new AddImportPathAction(context);
            readAction.Perform(new ParsedToken(0, 0));
            setAction.Perform(new ParsedToken(0, 0));

            var path = context.GetPath();
            Assert.Single(path);
            Assert.Contains("dir", path);
        }

        [Fact]
        public void AddImportPathAction_Test2()
        {
            var tokens = new SymbolsList(["dir1", "dir2", "dir3"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentImportPathAction(tokens, context);
            var setAction = new AddImportPathAction(context);
            for (int i = 0; i < tokens.Count; i++)
            {
                readAction.Perform(new ParsedToken(i, 0));
                setAction.Perform(new ParsedToken(0, 0));
            }

            var path = context.GetPath();
            Assert.Equal(tokens.Count, path.Count);
            for (int i = 0; i < tokens.Count; i++) Assert.Contains(tokens[i].Name, path);
        }
    }
}
