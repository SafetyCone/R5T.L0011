using System;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class BaseTypeDeclarationSyntaxExtensions
    {
        public static T AddDocumentation<T>(this T baseType,
            DocumentationCommentTriviaSyntax documentationComment)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseType
                .AddLeadingLeadingTrivia(
                    SyntaxFactory.Trivia(documentationComment));
            
            return output;
        }

        public static T EnsureBraceSpacing<T>(this T typeDeclaration)
            where T : BaseTypeDeclarationSyntax
        {
            var openBraceToken = typeDeclaration.OpenBraceToken;
            var closeBraceToken = typeDeclaration.CloseBraceToken;

            var openBraceTrailingSeparatingTrivia = openBraceToken.GetTrailingSeparatingTrivia();
            var closeBraceLeadingSeparatingTrivia = closeBraceToken.GetLeadingSeparatingTrivia();

            var openBraceSingleNewLineSeparatingTrivia = openBraceTrailingSeparatingTrivia.GetBeginningWithSingleNewLineTrivia();
            var closeBraceSingleNewLineSeparatingTrivia = closeBraceLeadingSeparatingTrivia.GetBeginningWithSingleNewLineTrivia();

            var outputTypeDeclaration = typeDeclaration;

            outputTypeDeclaration = outputTypeDeclaration.SetTrailingSeparatingTrivia(
                // Use the current open brace token to ensure it is found.
                outputTypeDeclaration.OpenBraceToken,
                openBraceSingleNewLineSeparatingTrivia);

            outputTypeDeclaration = outputTypeDeclaration.SetLeadingSeparatingTrivia(
                // Use the current close brace token to ensure it is found.
                outputTypeDeclaration.CloseBraceToken,
                closeBraceSingleNewLineSeparatingTrivia);

            return outputTypeDeclaration;
        }
    }
}
