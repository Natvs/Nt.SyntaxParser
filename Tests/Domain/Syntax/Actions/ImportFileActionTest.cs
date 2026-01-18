using Nt.Parsing.Structures;
using Nt.Syntax.Exceptions;
using Nt.Syntax;
using Nt.Syntax.Actions;

namespace Nt.Tests.Domain.Syntax.Actions
{
    public class ImportFileActionTest
    {
        [Fact]
        public void ImportFileAction_EmptyPathTest1()
        {
            var filename = "../../../Resources/test_file.txt";
            var tokens = new SymbolsList([filename]);
            var context = new AutomatonContext();;

            var readAction = new AppendToCurrentImportFileAction(tokens, context);
            var importAction = new ImportFileAction(context);

            readAction.Perform(new(0, 0));
            var imported = importAction.Perform(new(0, 0));
            var expected = File.ReadAllText(filename);

            Assert.Equal(expected, imported);
        }

        [Fact]
        public void ImportFileAction_EmptyPathTest2()
        {
            var filename = "../../../Resources/non_existent_file.txt";
            var tokens = new SymbolsList([filename]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentImportFileAction(tokens, context);
            var importAction = new ImportFileAction(context);

            readAction.Perform(new(0, 0));
            Assert.Throws<ImportFileNotFoundException>(() => importAction.Perform(new(0, 0)));
        }

        [Fact]
        public void ImportFileAction_EmptyPathTest3()
        {
            var filename = "../not/an/existing/path";
            var tokens = new SymbolsList([filename]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentImportFileAction(tokens, context);
            var importAction = new ImportFileAction(context);

            readAction.Perform(new(0, 0));
            Assert.Throws<ImportFileNotFoundException>(() => importAction.Perform(new(0, 0)));
        }

        [Fact]
        public void ImportFileAction_WithPathTest1()
        {
            var folderpath = "../../../Resources";
            var filename = "test_file.txt";
            var tokens = new SymbolsList([folderpath, filename]);
            var context = new AutomatonContext();

            var pathReadAction = new AppendToCurrentImportPathAction(new SymbolsList([folderpath]), context);
            var pathAction = new AddImportPathAction(context);

            pathReadAction.Perform(new(0, 0));
            pathAction.Perform(new(0, 0));

            var readAction = new AppendToCurrentImportFileAction(tokens, context);
            var action = new ImportFileAction(context);

            readAction.Perform(new(1, 0));
            var imported = action.Perform(new(0, 0));
            var expected = File.ReadAllText(folderpath + "/" + filename);

            Assert.Equal(expected, imported);
        }

        [Fact]
        public void ImportFileAction_WithPathTest2()
        {
            var folderpath = "../../../Resources";
            var filename = "non_existent_file.txt";
            var tokens = new SymbolsList([folderpath, filename]);
            var context = new AutomatonContext();

            var pathReadAction = new AppendToCurrentImportPathAction(new SymbolsList([folderpath]), context);
            var pathAction = new AddImportPathAction(context);

            pathReadAction.Perform(new(0, 0));
            pathAction.Perform(new(0, 0));

            var readAction = new AppendToCurrentImportFileAction(tokens, context);
            var importAction = new ImportFileAction(context);

            readAction.Perform(new(1, 0));

            Assert.Throws<ImportFileNotFoundException>(() => importAction.Perform(new(0, 0)));
        }

        [Fact]
        public void ImportFileAction_WithPathTest3()
        {
            var folderpath = "../../../Resources";
            var filename = "test_file.txt";
            var tokens = new SymbolsList([folderpath, folderpath + "/" + filename]);
            var context = new AutomatonContext();

            var pathReadAction = new AppendToCurrentImportPathAction(new SymbolsList([folderpath]), context);
            var pathAction = new AddImportPathAction(context);

            pathReadAction.Perform(new(0, 0));
            pathAction.Perform(new(0, 0));

            var readAction = new AppendToCurrentImportFileAction(tokens, context);
            var importAction = new ImportFileAction(context);

            readAction.Perform(new(1, 0));
            var imported = importAction.Perform(new(0, 0));
            var expected = File.ReadAllText(folderpath + "/" + filename);

            Assert.Equal(expected, imported);
        }

    }
}
