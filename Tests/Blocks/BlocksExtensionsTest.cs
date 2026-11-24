using Nt.Syntax;
using Nt.Syntax.Blocks;
using Nt.Syntax.Exceptions;

using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax.Blocks
{
    public class BlocksExtensionsTest
    {

        [Fact]
        public void BlocksExtensions_TestBlock_ShouldCreateBlockSyntax()
        {
            var parser = new SyntaxParser();
            var block = parser.CreateBlock("NEW", "anything");
            parser.SetTransition(block.OpenState, block.CloseState, "anything");

            ParseTest(parser, TestType.Blocks, "SimpleBlock");
        }

        [Fact]
        public void BlocksExtensions_TestBlock_ShouldThrowExceptionOnNonExpectedSymbolEncountered()
        {
            var parser = new SyntaxParser();
            var block = parser.CreateBlock("NEW", "error");
            parser.SetTransition(block.OpenState, block.CloseState, "error");

            Assert.Throws<ExpectedException>(() => ParseTest(parser, TestType.Blocks, "SimpleBlock"));
        }

        [Fact]
        public void BlocksExtensions_TestBlockWithSpaces_ShouldCreateBlockSyntax()
        {
            var parser = new SyntaxParser();
            var block = parser.CreateBlock("CUSTOM BLOCK", "anything");
            parser.SetTransition(block.OpenState, block.CloseState, "anything");

            ParseTest(parser, TestType.Blocks, "CustomBlock");
        }

        [Fact]
        public void BlocksExtensions_TestBlockWithAction_ShoudExecuteAction()
        {
            var parser = new SyntaxParser();
            var block = parser.CreateBlock("NEW", "anything");

            var actionExecuted = false;
            parser.SetTransition(block.OpenState, block.CloseState, "anything", (g, t) => { actionExecuted = true; });
            ParseTest(parser, TestType.Blocks, "SimpleBlock");
            
            Assert.True(actionExecuted);
        }

        [Fact]
        public void BlocksExtensions_TestBlocksWithRegularExpressions_ShouldCreateBlockSyntax()
        {
            var parser = new SyntaxParser();
            var block = parser.CreateBlock("REGEX", "a number");
            parser.SetTransition(block.OpenState, block.CloseState, "[0-9]*");
            parser.SetTransition(block.CloseState, block.CloseState, "[0-9]*");

            ParseTest(parser, TestType.Blocks, "RegexBlock");
        }

    }

}
