using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class MemberAccessExpressionSyntaxExtensions
    {
        public static MemberAccessExpressionSyntax WithDotTokenLeadingWhitespace(this MemberAccessExpressionSyntax memberAccess,
            SyntaxTriviaList leadingWhitespace)
        {
            var dotToken = memberAccess.GetChildDotToken();

            var newDotToken = dotToken.AddLeadingWhitespace(leadingWhitespace);

            var output = memberAccess.ReplaceToken(dotToken, newDotToken);
            return output;
        }
    }
}
