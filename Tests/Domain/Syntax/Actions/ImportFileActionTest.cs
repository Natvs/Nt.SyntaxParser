using Nt.Parsing.Structures;
using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;

namespace Nt.Syntax.Actions.Tests
{
    public class ImportFileActionTest
    {
        [Fact]
        public void ImportFileAction_EmptyPathTest1()
        {
            var filename = "../../../Resources/test_file.txt";
            var tokens = new SymbolsList([filename]);
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
            var tokens = new SymbolsList([filename]);
            var path = new ImportPath();

            var action = new ImportFileAction(tokens, path);

            Assert.Throws<ImportFileNotFoundException>(() => action.Perform(new(0, 0)));
        }

        [Fact]
        public void ImportFileAction_EmptyPathTest3()
        {
            var filename = "../not/an/existing/path";
            var tokens = new SymbolsList([filename]);
            var path = new ImportPath();

            var action = new ImportFileAction(tokens, path);

            Assert.Throws<ImportFileNotFoundException>(() => action.Perform(new(0, 0)));
        }

        [Fact]
        public void ImportFileAction_WithPathTest1()
        {
            var folderpath = "../../../Resources";
            var filename = "test_file.txt";
            var tokens = new SymbolsList([filename]);
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
            var tokens = new SymbolsList([filename]);
            var path = new ImportPath([folderpath]);

            var action = new ImportFileAction(tokens, path);

            Assert.Throws<ImportFileNotFoundException>(() => action.Perform(new(0, 0)));
        }

        [Fact]
        public void ImportFileAction_WithPathTest3()
        {
            var folderpath = "../../../Resources";
            var filename = "test_file.txt";
            var tokens = new SymbolsList([folderpath + "/" + filename]);
            var path = new ImportPath([folderpath]);

            var action = new ImportFileAction(tokens, path);
            var imported = action.Perform(new(0, 0));
            var expected = File.ReadAllText(folderpath + "/" + filename);

            Assert.Equal(expected, imported);
        }
    }
}
