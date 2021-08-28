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
    }
}
