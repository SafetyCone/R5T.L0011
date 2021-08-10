using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class MemberAccessExpressionSyntaxExtensions
    {
        public static MemberAccessExpressionSyntax WithIndentedDotToken(this MemberAccessExpressionSyntax memberAccess,
            SyntaxTriviaList indentation)
        {
            var dotToken = memberAccess.GetChildDotToken();

            var newDotToken = dotToken.AddLeadingWhitespace(indentation);

            var output = memberAccess.ReplaceToken(dotToken, newDotToken);
            return output;
        }
    }
}
