using Nt.Syntax.Exceptions;

using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax.Blocks
{
    public class BlocksTests
    {

        [Fact]
        public void Blocks_TestBlock_ShouldThrowIfBlockNotDefined()
        {
            Assert.Throws<ExpectedException>(() => ParseTest(TestType.Blocks, "SimpleBlock"));
        }

        [Fact]
        public void Blocks_TestBlock_ShouldNotThrowIfIngoreUnknownBlocksIsTrue()
        {
            ParseTest(TestType.Blocks, "IgnoreBlock");
        }

        [Fact]
        public void Blocks_TestBlock_ShouldRecoverAfterAnIgnoredBlock()
        {
            var grammar = ParseTest(TestType.Blocks, "IgnoreBlockRecovery");

            AssertTokens(grammar.Terminals, ["ok"]);
        }

    }
}
