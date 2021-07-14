using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class MethodDeclarationSyntaxExtensions
    {
        public static string[] GetParameterNames(this MethodDeclarationSyntax method)
        {
            var output = method.ParameterList.Parameters
                .Select(parameter => parameter.Identifier.ToString())
                .ToArray();

            return output;
        }
    }
}
