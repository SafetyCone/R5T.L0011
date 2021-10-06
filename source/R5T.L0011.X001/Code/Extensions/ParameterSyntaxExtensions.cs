using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class ParameterSyntaxExtensions
    {
        public static string GetTypeName(this ParameterSyntax parameter)
        {
            var output = parameter.Type.ToString();
            return output;
        }

        public static bool IsExtensionParameter(this ParameterSyntax parameter)
        {
            var output = parameter.Modifiers
                .Where(xModifier => xModifier.IsKind(SyntaxKind.ThisKeyword))
                .Any();

            return output;
        }
    }
}
