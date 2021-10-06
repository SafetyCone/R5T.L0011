using System;

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
    }
}
