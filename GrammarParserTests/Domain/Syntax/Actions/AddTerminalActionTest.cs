using GrammarParser.Parsing.Structures;
using GrammarParser.Syntax.Actions;
using GrammarParser.Syntax.Structures;
using GrammarParser.Syntax.Exceptions;

namespace Tests.Domain.Syntax.Actions
{
    public class AddTerminalActionTest
    {
        [Fact]
        public void AddTerminalAction_Test()
        {
            var grammar = new Grammar();
            var tokens = new TokensList(["a"]);
            var action = new AddTerminalAction(grammar, tokens);
            action.Perform(new ParsedToken(0, 0));

            Assert.Single(grammar.Terminals);
            Assert.Equal("a", grammar.Terminals[0].Name);
        }
    }

    public class ImportFileActionTest
    {
        [Fact]
        public void ImportFileAction_EmptyPathTest1()
        {
            var filename = "../../../Resources/test_file.txt";
            var tokens = new TokensList([filename]);
            var path = new ImportPath();

            var action = new ImportFileAction(tokens, path);
            var imported = action.Perform(new(0, 0));
            var expected = File.ReadAllText(filename);

            Assert.Equal(expected, imported);
        }

        [Fact]
        public void ImportFileAction_EmptyPathTest2()
        {
            var filename = "../../../Resources/non_existent_file.txt";
            var tokens = new TokensList([filename]);
            var path = new ImportPath();

            var action = new ImportFileAction(tokens, path);

            Assert.Throws<ImportFileNotFoundException>( () => action.Perform(new(0, 0)));
        }

        [Fact]
        public void ImportFileAction_EmptyPathTest3()
        {
            var filename = "../not/an/existing/path";
            var tokens = new TokensList([filename]);
            var path = new ImportPath();

            var action = new ImportFileAction(tokens, path);

            Assert.Throws<ImportFileNotFoundException>(() => action.Perform(new(0, 0)));
        }

        [Fact]
        public void ImportFileAction_WithPathTest1()
        {
            var folderpath = "../../../Resources";
            var filename = "test_file.txt";
            var tokens = new TokensList([filename]);
            var path = new ImportPath([folderpath]);

            var action = new ImportFileAction(tokens, path);
            var imported = action.Perform(new(0, 0));
            var expected = File.ReadAllText(folderpath + "/" + filename);

            Assert.Equal(expected, imported);
        }

        [Fact]
        public void ImportFileAction_WithPathTest2()
        {
            var folderpath = "../../../Resources";
            var filename = "non_existent_file.txt";
            var tokens = new TokensList([filename]);
            var path = new ImportPath([folderpath]);

            var action = new ImportFileAction(tokens, path);

            Assert.Throws<ImportFileNotFoundException>(() => action.Perform(new(0, 0)));
        }

        [Fact]
        public void ImportFileAction_WithPathTest3()
        {
            var folderpath = "../../../Resources";
            var filename = "test_file.txt";
            var tokens = new TokensList([folderpath + "/" + filename]);
            var path = new ImportPath([folderpath]);

            var action = new ImportFileAction(tokens, path);
            var imported = action.Perform(new(0, 0));
            var expected = File.ReadAllText(folderpath + "/" + filename);

            Assert.Equal(expected, imported);
        }
    }
}
