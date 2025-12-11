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
            var path = new ImportPath();
            var action = new AddImportPathAction(tokens, path);
            action.Perform(new ParsedToken(0, 0));

            Assert.Single(path.Path);
            Assert.Equal("dir", path.Path[0]);
        }

        [Fact]
        public void AddImportPathAction_Test2()
        {
            var tokens = new SymbolsList(["dir1", "dir2", "dir3"]);
            var path = new ImportPath();
            var action = new AddImportPathAction(tokens, path);
            for (int i = 0; i < tokens.Count; i++) action.Perform(new ParsedToken(i, 0));

            Assert.Equal(tokens.Count, path.Path.Count);
            for (int i = 0; i < tokens.Count; i++) Assert.Equal(tokens[i].Name, path.Path[i]);
        }
    }
}
