using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class StatementSyntaxExtensions
    {
        public static string GetTextAsString(this StatementSyntax statement)
        {
            var output = statement.ToString();
            return output;
        }

        public static bool HasBaseMemberAccessExpression(this StatementSyntax statement,
            out MemberAccessExpressionSyntax baseMemberAccessExpression)
        {
            if (statement is ExpressionStatementSyntax expressionStatement)
            {
                if (expressionStatement.Expression is InvocationExpressionSyntax invocationExpression)
                {
                    var output = invocationExpression.HasBaseMemberAccessExpression(
                        out baseMemberAccessExpression);

                    return output;
                }
            }

            baseMemberAccessExpression = default;

            return false;

            // Else, invalidation operation.
            throw new InvalidOperationException("Invocation expression had no base simple member access expression.");
        }
    }
}
