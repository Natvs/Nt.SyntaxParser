using Nt.Syntax.Exportation;
using Nt.Tests.Syntax.Exportation;

using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax.Exportation
{
    public class StringExporterTest
    {

        // Original order tests

        [Theory]
        [InlineData("Alphabetical", ExportationMode.Original)]
        [InlineData("Reversed", ExportationMode.Alphabetical)]
        public void StringExporter_ToString_ShouldReturnRightOrder(string filename, ExportationMode mode)
        {
            var grammar = ParseTest(TestType.Exportation, filename);

            var result = grammar.ToString(mode);
            Assert.Equal("" +
                "Terminals:\r\n" +
                "- a\r\n" +
                "- b\r\n" +
                "- c\r\n" +
                "\r\n" +
                "Non terminals:\r\n" +
                "- A\r\n" +
                "- B\r\n" +
                "- C\r\n" +
                "- D\r\n" +
                "- E\r\n" +
                "\r\n" +
                "Rules:\r\n" +
                "- A -> a\r\n" +
                "- B -> b\r\n" +
                "- C -> c\r\n" +
                "\r\n" +
                "Regular expressions:\r\n" +
                "- D = d\r\n" +
                "- E = e", result);
        }

        [Theory]
        [InlineData("Groups", ExportationMode.Grouped)]
        public void StringExporter_ToString_ShouldGroup(string filename, ExportationMode mode)
        {
            var grammar = ParseTest(TestType.Exportation, filename);

            var result = grammar.ToString(mode);

            Assert.Equal("" +
                "Terminals:\r\n" +
                "- a\r\n" +
                "- a2\r\n" +
                "- b\r\n" +
                "- b2\r\n" +
                "- c\r\n" +
                "- c2\r\n" +
                "- d\r\n" +
                "- d2\r\n" +
                "\r\n" +
                "Non terminals:\r\n" +
                "- A\r\n" +
                "- A2\r\n" +
                "- B\r\n" +
                "- B2\r\n" +
                "- C\r\n" +
                "- C2\r\n" +
                "- D\r\n" +
                "- D2\r\n" +
                "\r\n" +
                "Rules:\r\n" +
                "- A -> a\r\n" +
                "- A2 -> a2\r\n" +
                "- B -> b\r\n" +
                "- B2 -> b2\r\n" +
                "\r\n" +
                "Regular expressions:\r\n" +
                "- C = c\r\n" +
                "- C2 = c2\r\n" +
                "- D = d\r\n" +
                "- D2 = d2", result);
        }

        [Fact]
        public void StringExporter_ToString_ShouldGroupRulesWithSameAxiom()
        {
            var grammar = ParseTest(TestType.Exportation, "SameAxiomRules");

            var result = grammar.ToString(ExportationMode.Original);

            Assert.Equal("" +
                "Terminals:\r\n" +
                "- a\r\n" +
                "- b\r\n" +
                "\r\n" +
                "Non terminals:\r\n" +
                "- A\r\n" +
                "\r\n" +
                "Rules:\r\n" +
                "- A -> a | b", result);
        }
    }

}
