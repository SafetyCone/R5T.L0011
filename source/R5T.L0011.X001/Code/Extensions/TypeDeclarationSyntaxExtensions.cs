using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class TypeDeclarationSyntaxExtensions
    {
        public static bool HasMembers(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var output = typeDeclarationSyntax.Members.Count > 0;
            return output;
        }
    }
}
