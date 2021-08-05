using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class StatementSyntaxExtensions
    {
        public static SyntaxToken GetChildSemicolonToken(this StatementSyntax statement)
        {
            var output = statement.ChildTokens()
                .Where(token => token.IsKind(SyntaxKind.SemicolonToken))
                .Single();

            return output;
        }
        
        public static string GetTextAsString(this StatementSyntax statement)
        {
            var output = statement.ToString();
            return output;
        }

        public static T WithSemicolonLeadingWhitespace<T>(this T statement,
            SyntaxTriviaList leadingWhitespace)
            where T : StatementSyntax
        {
            var semicolonToken = statement.GetChildSemicolonToken();

            var newSemicolonToken = semicolonToken.AddLeadingWhitespace(leadingWhitespace);

            var output = statement.ReplaceToken(semicolonToken, newSemicolonToken);
            return output;
        }
    }
}
