using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class InvocationExpressionSyntaxExtensions
    {
        public static bool HasBaseMemberAccessExpression(this InvocationExpressionSyntax invocationExpression,
            out MemberAccessExpressionSyntax baseMemberAccessExpression)
        {
            if (invocationExpression.Expression is MemberAccessExpressionSyntax memberAccessExpression)
            {
                if (memberAccessExpression.IsKind(SyntaxKind.SimpleMemberAccessExpression))
                {
                    // Base case.
                    if (memberAccessExpression.Expression is IdentifierNameSyntax)
                    {
                        baseMemberAccessExpression = memberAccessExpression;

                        return true;
                    }

                    // Recursion
                    if (memberAccessExpression.Expression is InvocationExpressionSyntax subInvocationExpression)
                    {
                        var output = subInvocationExpression.HasBaseMemberAccessExpression(
                            out baseMemberAccessExpression);

                        return output;
                    }
                }
            }

            baseMemberAccessExpression = default;

            return false;
        }
    }
}
