using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class TypeSyntaxExtensions
    {
        public static string GetTypeName(this TypeSyntax typeSyntax)
        {
            var output = typeSyntax.ToString();
            return output;
        }

        public static bool IsNamed(this TypeSyntax typeSyntax,
            string typeName)
        {
            var typeNameFromSyntax = typeSyntax.GetTypeName();

            var output = typeNameFromSyntax == typeName;
            return output;
        }
    }
}
