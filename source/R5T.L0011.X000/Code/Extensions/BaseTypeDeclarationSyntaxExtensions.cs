using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class BaseTypeDeclarationSyntaxExtensions
    {
        public static bool HasCloseBrace(this BaseTypeDeclarationSyntax baseType)
        {
            var output = !baseType.CloseBraceToken.IsMissing;
            return output;
        }

        public static bool HasOpenBrace(this BaseTypeDeclarationSyntax baseType)
        {
            var output = !baseType.OpenBraceToken.IsMissing;
            return output;
        }
    }
}
