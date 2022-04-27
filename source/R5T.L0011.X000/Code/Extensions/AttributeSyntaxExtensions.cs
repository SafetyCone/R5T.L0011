using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class AttributeSyntaxExtensions
    {
        public static string GetName(this AttributeSyntax attribute)
        {
            var output = attribute.Name.ToString();
            return output;
        }

        public static WasFound<AttributeArgumentSyntax[]> HasArguments(this AttributeSyntax attribute)
        {
            var output = attribute.ArgumentList?.Arguments.ToArray() ?? Array.Empty<AttributeArgumentSyntax>();

            return WasFound.FromArray(output);
        }

        public static bool NameIs(this AttributeSyntax attribute,
            string name)
        {
            var output = attribute.GetName() == name;
            return output;
        }
    }
}
