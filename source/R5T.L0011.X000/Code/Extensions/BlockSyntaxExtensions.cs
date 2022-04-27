using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    using N8;


    public static class BlockSyntaxExtensions
    {
        public static bool HasBraces(this BlockSyntax block)
        {
            var hasOpenBrace = block.HasOpenBrace();
            var hasCloseBrace = block.HasCloseBrace();

            var output = hasOpenBrace && hasCloseBrace;
            return output;
        }

        public static bool HasCloseBrace(this BlockSyntax block)
        {
            var output = !block.CloseBraceToken.IsMissing;
            return output;
        }

        public static bool HasOpenBrace(this BlockSyntax block)
        {
            var output = !block.OpenBraceToken.IsMissing;
            return output;
        }

        public static BlockSyntax SetBracesLineIndentation(this BlockSyntax block,
            SyntaxTriviaList indentation)
        {
            block.VerifyHasBraces();

            block = block.SetBracesLineIndentation(
                block.OpenBraceToken,
                block.CloseBraceToken,
                indentation);

            return block;
        }

        public static BlockSyntax SetBracesLineIndentation(this BlockSyntax block,
            SyntaxToken openBraceToken,
            SyntaxToken closeBraceToken,
            SyntaxTriviaList indentation)
        {
            block = block.SetDescendantBracesLineIndentation(
                openBraceToken,
                closeBraceToken,
                indentation);

            return block;
        }

        public static void VerifyHasBraces(this BlockSyntax block)
        {
            var hasBraces = block.HasBraces();
            if (!hasBraces)
            {
                throw new Exception("No open or close brace found for block.");
            }
        }
    }
}
