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


        public static T WithBraces<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList indentation)
            where T : BaseTypeDeclarationSyntax
        {
            var modified = baseTypeDeclarationSyntax
                .WithOpenBraceToken(SyntaxFactory.OpenBrace(indentation))
                .WithCloseBraceToken(SyntaxFactory.CloseBrace(indentation))
                ;

            var output = modified as T;
            return output;
        }

        public static T WithCloseBrace<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList indentation)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithCloseBraceToken(SyntaxFactory.CloseBrace(indentation))
                as T;

            return output;
        }

        public static T WithCloseBrace2<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList leadingWhitespace, bool appendNewLine = false)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithCloseBraceToken(SyntaxFactory.CloseBrace2(leadingWhitespace, appendNewLine))
                as T;

            return output;
        }

        public static T WithOpenBrace<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList indentation)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithOpenBraceToken(SyntaxFactory.OpenBrace(indentation))
                as T;

            return output;
        }

        public static T WithOpenBrace2<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList leadingWhitespace, bool prependNewLine = true)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithOpenBraceToken(SyntaxFactory.OpenBrace2(leadingWhitespace, prependNewLine))
                as T;

            return output;
        }
    }
}
