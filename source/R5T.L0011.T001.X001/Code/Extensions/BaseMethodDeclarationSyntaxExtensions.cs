using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;


namespace System
{
    public static class BaseMethodDeclarationSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static T AddParameter<T>(this T method,
            string name, string typeName,
            SyntaxTriviaList leadingWhitespace)
            where T : BaseMethodDeclarationSyntax
        {
            var parameter = SyntaxFactory.Parameter(name, typeName)
                .NormalizeWhitespace()
                .AddLeadingLeadingTrivia(leadingWhitespace.ToArray());

            var output = method.AddParameterListParameters(parameter) as T;
            return output;
        }

        public static T AddInitialBody<T>(this T method,
            SyntaxTriviaList leadingWhitespace)
            where T : BaseMethodDeclarationSyntax
        {
            var output = method
                .WithBody(SyntaxFactory.Body()
                    .WithOpenBraceToken(SyntaxFactory.OpenBrace(leadingWhitespace))
                    .WithCloseBraceToken(SyntaxFactory.CloseBrace(leadingWhitespace)))
                as T;

            return output;
        }
    }
}
