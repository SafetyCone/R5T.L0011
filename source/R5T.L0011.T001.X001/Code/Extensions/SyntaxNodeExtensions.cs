using System;
using System.IO;

using Microsoft.CodeAnalysis;

using R5T.L0011.T001;

using CSharpSyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static class SyntaxNodeExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static TSyntaxNode WithLineLeadingWhitespace<TSyntaxNode>(this TSyntaxNode syntaxNode, SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode
                .WithLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace())
                ;

            return output;
        }

        public static TSyntaxNode WithEndOfLine<TSyntaxNode>(this TSyntaxNode syntaxNode)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode
                .WithTrailingTrivia(
                    SyntaxFactory.EndOfLine());

            return output;
        }
    }
}
