using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class ClassDeclarationSyntaxExtensions
    {
        public static ConstructorDeclarationSyntax GetConstructor(this ClassDeclarationSyntax @class)
        {
            var output = @class.Members
                .OfType<ConstructorDeclarationSyntax>()
                .Single();

            return output;
        }

        public static bool HasMethodMembers(this ClassDeclarationSyntax @class)
        {
            var output = @class.Members
                .OfType<MethodDeclarationSyntax>()
                .Any();

            return output;
        }
    }
}
