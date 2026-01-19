using Nt.Syntax.Exceptions;
using Nt.Syntax;
using Nt.Syntax.Actions;
using Nt.Parser.Structures;

namespace Nt.Tests.Domain.Syntax.Actions
{
    public class ImportFileActionTest
    {
        [Fact]
        public void ImportFileAction_EmptyPathTest1()
        {
            var filename = "../../../Resources/test_file.txt";
            var symbols = new SymbolsList([filename]);
            var context = new AutomatonContext();;

            var readAction = new AppendToCurrentImportFileAction(context);
            var importAction = new ImportFileAction(context);

            readAction.Perform(new(symbols.Get(0), 0));
            var imported = importAction.Perform(new(symbols.Get(0), 0));
            var expected = File.ReadAllText(filename);

            Assert.Equal(expected, imported);
        }

        [Fact]
        public void ImportFileAction_EmptyPathTest2()
        {
            var filename = "../../../Resources/non_existent_file.txt";
            var symbols = new SymbolsList([filename]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentImportFileAction(context);
            var importAction = new ImportFileAction(context);

            readAction.Perform(new(symbols.Get(0), 0));
            Assert.Throws<ImportFileNotFoundException>(() => importAction.Perform(new(symbols.Get(0), 0)));
        }

        [Fact]
        public void ImportFileAction_EmptyPathTest3()
        {
            var filename = "../not/an/existing/path";
            var symbols = new SymbolsList([filename]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentImportFileAction(context);
            var importAction = new ImportFileAction(context);

            readAction.Perform(new(symbols.Get(0), 0));
            Assert.Throws<ImportFileNotFoundException>(() => importAction.Perform(new(symbols.Get(0), 0)));
        }

        [Fact]
        public void ImportFileAction_WithPathTest1()
        {
            var folderpath = "../../../Resources";
            var filename = "test_file.txt";
            var symbols = new SymbolsList([folderpath, filename]);
            var context = new AutomatonContext();

            var pathReadAction = new AppendToCurrentImportPathAction(context);
            var pathAction = new AddImportPathAction(context);

            pathReadAction.Perform(new(symbols.Get(0), 0));
            pathAction.Perform(new(symbols.Get(0), 0));

            var readAction = new AppendToCurrentImportFileAction(context);
            var action = new ImportFileAction(context);

            readAction.Perform(new(symbols.Get(1), 0));
            var imported = action.Perform(new(symbols.Get(0), 0));
            var expected = File.ReadAllText(folderpath + "/" + filename);

            Assert.Equal(expected, imported);
        }

        [Fact]
        public void ImportFileAction_WithPathTest2()
        {
            var folderpath = "../../../Resources";
            var filename = "non_existent_file.txt";
            var symbols = new SymbolsList([folderpath, filename]);
            var context = new AutomatonContext();

            var pathReadAction = new AppendToCurrentImportPathAction(context);
            var pathAction = new AddImportPathAction(context);

            pathReadAction.Perform(new(symbols.Get(0), 0));
            pathAction.Perform(new(symbols.Get(0), 0));

            var readAction = new AppendToCurrentImportFileAction(context);
            var importAction = new ImportFileAction(context);

            readAction.Perform(new(symbols.Get(1), 0));

            Assert.Throws<ImportFileNotFoundException>(() => importAction.Perform(new(symbols.Get(0), 0)));
        }

        [Fact]
        public void ImportFileAction_WithPathTest3()
        {
            var folderpath = "../../../Resources";
            var filename = "test_file.txt";
            var symbols = new SymbolsList([folderpath, folderpath + "/" + filename]);
            var context = new AutomatonContext();

            var pathReadAction = new AppendToCurrentImportPathAction(context);
            var pathAction = new AddImportPathAction(context);

            pathReadAction.Perform(new(symbols.Get(0), 0));
            pathAction.Perform(new(symbols.Get(0), 0));

            var readAction = new AppendToCurrentImportFileAction(context);
            var importAction = new ImportFileAction(context);

            readAction.Perform(new(symbols.Get(1), 0));
            var imported = importAction.Perform(new(symbols.Get(0), 0));
            var expected = File.ReadAllText(folderpath + "/" + filename);

            Assert.Equal(expected, imported);
        }

    }
}
