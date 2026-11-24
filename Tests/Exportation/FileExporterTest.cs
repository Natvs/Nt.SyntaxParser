using Nt.Syntax.Exportation;

using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax.Exportation
{
    public class FileExporterTest
    {

        [Theory]
        [InlineData("Alphabetical", ExportationMode.Original)]
        [InlineData("Reversed", ExportationMode.Alphabetical)]
        public void FileExporter_Save_ShouldReturnCorrectString(string filename, ExportationMode mode)
        {
            var grammar = ParseTest(TestType.Exportation, filename);
            var result = grammar.Save(mode);
            
            Assert.Equal("" +
                "TERMINALS (" + Environment.NewLine +
                "\ta" + Environment.NewLine +
                "\tb" + Environment.NewLine +
                "\tc" + Environment.NewLine +
                ")" + Environment.NewLine +
                Environment.NewLine +
                "NON TERMINALS (" + Environment.NewLine +
                "\tA" + Environment.NewLine +
                "\tB" + Environment.NewLine +
                "\tC" + Environment.NewLine +
                "\tD" + Environment.NewLine +
                "\tE" + Environment.NewLine +
                ")" + Environment.NewLine +
                Environment.NewLine +
                "RULES (" + Environment.NewLine +
                "\tA -> a ;" + Environment.NewLine +
                "\tB -> b ;" + Environment.NewLine +
                "\tC -> c ;" + Environment.NewLine +
                ")" + Environment.NewLine +
                Environment.NewLine +
                "REGULAR EXPRESSIONS (" + Environment.NewLine +
                "\tD = d" + Environment.NewLine +
                "\tE = e" + Environment.NewLine +
                ")", result);
        }

        [Theory]
        [InlineData("Groups", ExportationMode.Grouped)]
        public void FileExporter_Save_ShouldGroup(string filename, ExportationMode mode)
        {
            var grammar = ParseTest(TestType.Exportation, filename);

            var result = grammar.Save(mode);

            Assert.Equal("" +
                "TERMINALS (" +
                "\ta" + Environment.NewLine +
                "\ta2" + Environment.NewLine +
                "\tb" + Environment.NewLine +
                "\tb2" + Environment.NewLine +
                "\tc" + Environment.NewLine +
                "\tc2" + Environment.NewLine +
                "\td" + Environment.NewLine +
                "\td2" + Environment.NewLine +
                ")" + Environment.NewLine +
                Environment.NewLine +
                "NON TERMINALS (" + Environment.NewLine +
                "\tA" + Environment.NewLine +
                "\tA2" + Environment.NewLine +
                "\tB" + Environment.NewLine +
                "\tB2" + Environment.NewLine +
                "\tC" + Environment.NewLine +
                "\tC2" + Environment.NewLine +
                "\tD" + Environment.NewLine +
                "\tD2" + Environment.NewLine +
                ")" + Environment.NewLine +
                Environment.NewLine +
                "RULES (" + Environment.NewLine +
                "\tA -> a ;" + Environment.NewLine +
                "\tA2 -> a2 ;" + Environment.NewLine +
                "\tB -> b ;" + Environment.NewLine +
                "\tB2 -> b2 ;" + Environment.NewLine +
                ")" + Environment.NewLine +
                Environment.NewLine +
                "REGULAR EXPRESSIONS (" + Environment.NewLine +
                "\tC = c" + Environment.NewLine +
                "\tC2 = c2" + Environment.NewLine +
                "\tD = d" + Environment.NewLine +
                "\tD2 = d2" + Environment.NewLine +
                ")", result);
        }

        [Fact]
        public void FileExporter_Save_ShouldGroupRulesWithSameAxiom()
        {
            var grammar = ParseTest(TestType.Exportation, "SameAxiomRules");

            var result = grammar.Save(ExportationMode.Grouped);

            Assert.Equal("" +
                "TERMINALS (" + Environment.NewLine +
                "\ta" + Environment.NewLine +
                "\tb" + Environment.NewLine +
                ")" + Environment.NewLine +
                Environment.NewLine +
                "NON TERMINALS (" + Environment.NewLine +
                "\tA" + Environment.NewLine +
                ")" + Environment.NewLine +
                Environment.NewLine +
                "RULES (" + Environment.NewLine +
                "\tA -> a | b ;" + Environment.NewLine +
                ")", result);
        }

    }
}
