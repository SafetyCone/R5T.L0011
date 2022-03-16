using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class TypeParameterConstraintClauseSyntaxExtensions
    {
        public static string ToTextStandard(this TypeParameterConstraintClauseSyntax typeParameterConstraintClause)
        {
            // Just use the result of ToString().
            var output = typeParameterConstraintClause.ToString();
            return output;
        }
    }
}
