using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class BlockSyntaxExtensions
    {
        public static bool HasStatements(this BlockSyntax block)
        {
            var output = block.Statements.Count > 0;
            return output;
        }

        public static BlockSyntax Indent(this BlockSyntax block,
            SyntaxTriviaList indentation)
        {
            var blockOutput = block
                .WithOpenBraceToken(block.OpenBraceToken.IndentStartLine(indentation))
                .WithCloseBraceToken(block.CloseBraceToken.IndentStartLine(indentation))
                ;

            return blockOutput;
        }

        /// <summary>
        /// Useful since the last statement is usually a return statement.
        /// </summary>
        public static BlockSyntax RemoveLastStatement(this BlockSyntax block,
            bool preserveLeadingTrivia = true)
        {
            // If no statements, return.
            if(!block.Statements.Any())
            {
                return block;
            }

            // As of now, there is at least one statement.

            // If we want to preserve the leading trivia of the last statement, and has any non-whitespace leading trivia, preserve the non-whitespace leading trivia.
            var outputBlock = block;

            if(preserveLeadingTrivia)
            {
                var lastStatement = block.Statements.Last();

                var hasNonWhitespaceLeadingTrivia = lastStatement.HasNonWhitespaceLeadingTrivia();
                if(hasNonWhitespaceLeadingTrivia)
                {
                    // Add the non-whitespace leading trivia as trailing trivia of the prior node.
                    var previousToken = lastStatement.GetFirstToken().GetPreviousToken();

                    var outputPreviousToken = previousToken.AddLeadingTrailingTrivia(hasNonWhitespaceLeadingTrivia.Result.ToArray());

                    outputBlock = outputBlock.ReplaceToken(previousToken, outputPreviousToken);
                }
            }

            // Ok, since there is at least one statement by now.
            outputBlock = outputBlock.WithStatements(outputBlock.Statements.SkipLast(1).ToSyntaxList());
            return outputBlock;
        }
    }
}
