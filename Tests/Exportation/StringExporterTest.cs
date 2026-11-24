using Nt.Syntax.Exportation;
using Nt.Tests.Syntax.Exportation;
using System.Text;
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
                "Terminals:" + Environment.NewLine +
                "- a" + Environment.NewLine +
                "- b" + Environment.NewLine +
                "- c" + Environment.NewLine +
                Environment.NewLine +
                "Non terminals:" + Environment.NewLine +
                "- A" + Environment.NewLine +
                "- B" + Environment.NewLine +
                "- C" + Environment.NewLine +
                "- D" + Environment.NewLine +
                "- E" + Environment.NewLine +
                Environment.NewLine +
                "Rules:" + Environment.NewLine +
                "- A -> a" + Environment.NewLine +
                "- B -> b" + Environment.NewLine +
                "- C -> c" + Environment.NewLine +
                Environment.NewLine +
                "Regular expressions:" + Environment.NewLine +
                "- D = d" + Environment.NewLine +
                "- E = e", result);
        }

        [Theory]
        [InlineData("Groups", ExportationMode.Grouped)]
        public void StringExporter_ToString_ShouldGroup(string filename, ExportationMode mode)
        {
            var grammar = ParseTest(TestType.Exportation, filename);

            var result = grammar.ToString(mode);

            var builder = new StringBuilder();
            

            Assert.Equal("" +
                "Terminals:" + Environment.NewLine +
                "- a" + Environment.NewLine +
                "- a2" + Environment.NewLine +
                "- b" + Environment.NewLine +
                "- b2" + Environment.NewLine +
                "- c" + Environment.NewLine +
                "- c2" + Environment.NewLine +
                "- d" + Environment.NewLine +
                "- d2" + Environment.NewLine +
                Environment.NewLine +
                "Non terminals:" + Environment.NewLine +
                "- A" + Environment.NewLine +
                "- A2" + Environment.NewLine +
                "- B" + Environment.NewLine +
                "- B2" + Environment.NewLine +
                "- C" + Environment.NewLine +
                "- C2" + Environment.NewLine +
                "- D" + Environment.NewLine +
                "- D2" + Environment.NewLine +
                Environment.NewLine +
                "Rules:" + Environment.NewLine +
                "- A -> a" + Environment.NewLine +
                "- A2 -> a2" + Environment.NewLine +
                "- B -> b" + Environment.NewLine +
                "- B2 -> b2" + Environment.NewLine +
                Environment.NewLine +
                "Regular expressions:" + Environment.NewLine +
                "- C = c" + Environment.NewLine +
                "- C2 = c2" + Environment.NewLine +
                "- D = d" + Environment.NewLine +
                "- D2 = d2", result);
        }

        [Fact]
        public void StringExporter_ToString_ShouldGroupRulesWithSameAxiom()
        {
            var grammar = ParseTest(TestType.Exportation, "SameAxiomRules");

            var result = grammar.ToString(ExportationMode.Original);

            Assert.Equal("" +
                "Terminals:" + Environment.NewLine +
                "- a" + Environment.NewLine +
                "- b" + Environment.NewLine +
                Environment.NewLine +
                "Non terminals:" + Environment.NewLine +
                "- A" + Environment.NewLine +
                Environment.NewLine +
                "Rules:" + Environment.NewLine +
                "- A -> a | b", result);
        }
    }

}
