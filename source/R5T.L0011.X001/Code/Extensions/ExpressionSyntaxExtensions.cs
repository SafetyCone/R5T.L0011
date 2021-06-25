using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class ExpressionSyntaxExtensions
    {
        public static SyntaxToken GetChildDotToken(this ExpressionSyntax expression)
        {
            var output = expression.ChildTokens()
                .Where(token => token.IsKind(SyntaxKind.DotToken))
                .Single();

            return output;
        }
    }
}
