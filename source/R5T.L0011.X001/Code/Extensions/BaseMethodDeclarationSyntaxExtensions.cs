using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class BaseMethodDeclarationSyntaxExtensions
    {
        public static T AddDocumentation<T>(this T baseMethod,
            DocumentationCommentTriviaSyntax documentationComment)
            where T : BaseMethodDeclarationSyntax
        {
            var output = baseMethod
                .AddLeadingLeadingTrivia(
                    SyntaxFactory.Trivia(documentationComment));

            return output;
        }
    }
}
