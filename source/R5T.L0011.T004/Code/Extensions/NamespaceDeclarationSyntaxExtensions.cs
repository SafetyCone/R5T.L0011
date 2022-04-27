using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T004;


namespace System
{
    public static class NamespaceDeclarationSyntaxExtensions
    {
        public static UsingDirectivesSpecification GetUsingDirectivesSpecification(this NamespaceDeclarationSyntax @namespace)
        {
            var usings = @namespace.GetUsingDirectives();

            var output = usings.GetUsingDirectivesSpecification();
            return output;
        }

        public static UsingDirectivesSpecification GetUsingDirectivesSpecification(this NamespaceDeclarationSyntax @namespace,
            CompilationUnitSyntax compilationUnit)  
        {
            var compilationUsings = compilationUnit.GetUsings();
            var namespaceUsings = @namespace.GetUsingDirectives();

            var combinedUsings = compilationUsings.AppendRange(namespaceUsings);

            var output = combinedUsings.GetUsingDirectivesSpecification();
            return output;
        }
    }
}
