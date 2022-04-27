using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T004;


namespace System
{
    public static class CompilationUnitSyntaxExtensions
    {
        public static UsingDirectivesSpecification GetUsingDirectivesSpecification(this CompilationUnitSyntax compilationUnit)
        {
            var usings = compilationUnit.GetUsings();

            var output = usings.GetUsingDirectivesSpecification();
            return output;
        }

        public static UsingDirectivesSpecification GetUsingDirectivesSpecification(this CompilationUnitSyntax compilationUnit,
            NamespaceDeclarationSyntax @namespace)
        {
            var compilationUsings = compilationUnit.GetUsings();
            var namespaceUsings = @namespace.GetUsingDirectives();

            var combinedUsings = compilationUsings.AppendRange(namespaceUsings);

            var output = combinedUsings.GetUsingDirectivesSpecification();
            return output;
        }
    }
}
