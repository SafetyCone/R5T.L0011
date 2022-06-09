using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Instances = R5T.L0011.X005.Instances;


namespace System
{
    public static class NamespaceDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Get the fule name for a namespace (handling namespace nesting).
        /// For example, if namespace "Z" was nested inside "X.Y", this method would return the ful "X.Y.Z".
        /// </summary>
        public static string GetFullName(this NamespaceDeclarationSyntax namespaceDeclaration)
        {
            var namespaceLineage = namespaceDeclaration.GetContainingNamespacesOutsideToInside()
                .Append(namespaceDeclaration)
                ;

            var namespaceNameFragments = namespaceLineage
                .Select(x => x.GetName())
                ;

            var fullNamespaceName = Instances.NamespaceName.CombineTokens(namespaceNameFragments);
            return fullNamespaceName;
        }
    }
}