using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using Instances = R5T.L0011.T001.X001.Instances;


namespace System
{
    public static class CompilationUnitSyntaxExtensions
    {
        public static CompilationUnitSyntax AddUsingAfter(this CompilationUnitSyntax compilationUnit,
            string namespaceName,
            string priorNamespaceName)
        {
            var usingDirective = Instances.SyntaxFactory.Using_WithoutLeadingNewLine(namespaceName);

            var priorUsingDirective = compilationUnit.GetUsing(priorNamespaceName);

            var output = compilationUnit.InsertNodesAfter(priorUsingDirective, EnumerableHelper.From(usingDirective));
            return output;
        }
    }
}
