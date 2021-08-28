using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    /// <summary>
    /// NOTE: you probably want <see cref="BaseMethodDeclarationSyntaxExtensions"/>.
    /// </summary>
    public static class MethodDeclarationSyntaxExtensions
    {
        public static string Name(this MethodDeclarationSyntax method)
        {
            var output = method.Identifier.Text;
            return output;
        }
    }
}
