using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    using N8;


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

        public static T SetBracesLineIndentation<T>(this T typeDeclaration,
            SyntaxTriviaList indentation)
            where T : BaseTypeDeclarationSyntax
        {
            typeDeclaration.VerifyHasBraces();

            typeDeclaration = typeDeclaration.SetBracesLineIndentation(
                typeDeclaration.OpenBraceToken,
                typeDeclaration.CloseBraceToken,
                indentation);

            return typeDeclaration;
        }

        public static T SetBracesLineIndentation<T>(this T typeDeclaration,
            SyntaxToken openBraceToken,
            SyntaxToken closeBraceToken,
            SyntaxTriviaList indentation)
            where T : BaseTypeDeclarationSyntax
        {
            typeDeclaration = typeDeclaration.SetDescendantBracesLineIndentation(
                openBraceToken,
                closeBraceToken,
                indentation);

            return typeDeclaration;
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
