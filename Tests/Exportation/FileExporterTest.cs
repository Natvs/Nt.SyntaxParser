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
                "TERMINALS (\r\n" +
                "\ta\r\n" +
                "\tb\r\n" +
                "\tc\r\n" +
                ")\r\n" +
                "\r\n" +
                "NON TERMINALS (\r\n" +
                "\tA\r\n" +
                "\tB\r\n" +
                "\tC\r\n" +
                "\tD\r\n" +
                "\tE\r\n" +
                ")\r\n" +
                "\r\n" +
                "RULES (\r\n" +
                "\tA -> a ;\r\n" +
                "\tB -> b ;\r\n" +
                "\tC -> c ;\r\n" +
                ")\r\n" +
                "\r\n" +
                "REGULAR EXPRESSIONS (\r\n" +
                "\tD = d\r\n" +
                "\tE = e\r\n" +
                ")", result);
        }

        [Theory]
        [InlineData("Groups", ExportationMode.Grouped)]
        public void FileExporter_Save_ShouldGroup(string filename, ExportationMode mode)
        {
            var grammar = ParseTest(TestType.Exportation, filename);

            var result = grammar.Save(mode);

            Assert.Equal("" +
                "TERMINALS (\r\n" +
                "\ta\r\n" +
                "\ta2\r\n" +
                "\tb\r\n" +
                "\tb2\r\n" +
                "\tc\r\n" +
                "\tc2\r\n" +
                "\td\r\n" +
                "\td2\r\n" +
                ")\r\n" +
                "\r\n" +
                "NON TERMINALS (\r\n" +
                "\tA\r\n" +
                "\tA2\r\n" +
                "\tB\r\n" +
                "\tB2\r\n" +
                "\tC\r\n" +
                "\tC2\r\n" +
                "\tD\r\n" +
                "\tD2\r\n" +
                ")\r\n" +
                "\r\n" +
                "RULES (\r\n" +
                "\tA -> a ;\r\n" +
                "\tA2 -> a2 ;\r\n" +
                "\tB -> b ;\r\n" +
                "\tB2 -> b2 ;\r\n" +
                ")\r\n" +
                "\r\n" +
                "REGULAR EXPRESSIONS (\r\n" +
                "\tC = c\r\n" +
                "\tC2 = c2\r\n" +
                "\tD = d\r\n" +
                "\tD2 = d2\r\n" +
                ")", result);
        }

        [Fact]
        public void FileExporter_Save_ShouldGroupRulesWithSameAxiom()
        {
            var grammar = ParseTest(TestType.Exportation, "SameAxiomRules");

            var result = grammar.Save(ExportationMode.Grouped);

            Assert.Equal("" +
                "TERMINALS (\r\n" +
                "\ta\r\n" +
                "\tb\r\n" +
                ")\r\n" +
                "\r\n" +
                "NON TERMINALS (\r\n" +
                "\tA\r\n" +
                ")\r\n" +
                "\r\n" +
                "RULES (\r\n" +
                "\tA -> a | b ;\r\n" +
                ")", result);
        }

    }
}
