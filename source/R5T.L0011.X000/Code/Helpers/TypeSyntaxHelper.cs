using System;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace R5T.L0011.X000
{
    public static class TypeSyntaxHelper
    {
        public static PredefinedTypeSyntax GetVoid()
        {
            var output = SyntaxFactory.PredefinedType(
                SyntaxTokens.Void());

            return output;
        }
    }
}
