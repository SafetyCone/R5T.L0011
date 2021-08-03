using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class BlockSyntaxExtensions
    {
        public static BlockSyntax Indent(this BlockSyntax block,
            SyntaxTriviaList indentation)
        {
            var blockOutput = block
                .WithOpenBraceToken(block.OpenBraceToken.Indent(indentation))
                .WithCloseBraceToken(block.CloseBraceToken.Indent(indentation))
                ;

            return blockOutput;
        }
    }
}
