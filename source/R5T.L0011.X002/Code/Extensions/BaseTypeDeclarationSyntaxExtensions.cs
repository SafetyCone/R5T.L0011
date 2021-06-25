using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;
using R5T.L0011.T003;


namespace System
{
    public static class BaseTypeDeclarationSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static T WithCloseBrace<T>(this T namespaceDeclarationSyntax,
            SyntaxTriviaList leadingWhitespace, bool appendNewLine = false)
            where T : BaseTypeDeclarationSyntax
        {
            var output = namespaceDeclarationSyntax
                .WithCloseBraceToken(SyntaxFactory.CloseBrace(leadingWhitespace, appendNewLine))
                as T;

            return output;
        }

        public static T WithOpenBrace<T>(this T namespaceDeclarationSyntax,
            SyntaxTriviaList leadingWhitespace, bool prependNewLine = true)
            where T : BaseTypeDeclarationSyntax
        {
            var output = namespaceDeclarationSyntax
                .WithOpenBraceToken(SyntaxFactory.OpenBrace(leadingWhitespace, prependNewLine))
                as T;

            return output;
        }
    }
}
