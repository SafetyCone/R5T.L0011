using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;


namespace System
{
    public static class CompilationUnitSyntaxExtensions
    {
        private static ISyntaxFactory CustomSyntaxFactory { get; } = SyntaxFactory.Instance;


        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            string[] namespaceNames)
        {
            var usingDirectives = namespaceNames
                .Select(namespaceName => CustomSyntaxFactory.Using(namespaceName))
                .ToArray();

            var output = compilationUnit.AddUsings(usingDirectives);
            return output;
        }
    }
}
