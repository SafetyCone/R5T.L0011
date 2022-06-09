using System;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class AccessorDeclarationSyntaxExtensions
    {
        public static AccessorDeclarationSyntax WithSemicolonToken(this AccessorDeclarationSyntax accessor)
        {
            accessor = accessor.WithSemicolonToken(
                SyntaxTokens.Semicolon());

            return accessor;
        }
    }
}
