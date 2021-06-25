using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;


namespace System
{
    public static class MethodDeclarationSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static MethodDeclarationSyntax WithSemicolon(this MethodDeclarationSyntax methodDeclarationSyntax)
        {
            var output = methodDeclarationSyntax
                .WithSemicolonToken(SyntaxFactory.Semicolon());

            return output;
        }
    }
}
