using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class TypeDeclarationSyntaxExtensions
    {
        public static IEnumerable<NamespaceDeclarationSyntax> GetContainingNamespacesInsideToOutside(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var output = typeDeclarationSyntax.GetParentsInsideToOutside()
                .OfType<NamespaceDeclarationSyntax>();

            return output;
        }

        public static IEnumerable<NamespaceDeclarationSyntax> GetContainingNamespacesOutsideToInside(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var output = typeDeclarationSyntax.GetContainingNamespacesInsideToOutside()
                .Reverse()
                ;

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetContainingNamespacesInsideToOutside(TypeDeclarationSyntax)"/> as the default.
        /// </summary>
        public static IEnumerable<NamespaceDeclarationSyntax> GetContainingNamespaces(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var output = typeDeclarationSyntax.GetParentsInsideToOutside()
                .OfType<NamespaceDeclarationSyntax>();

            return output;
        }

        public static NamespaceDeclarationSyntax GetContainingNamespace(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var output = typeDeclarationSyntax.GetContainingNamespacesInsideToOutside()
                .First();

            return output;
        }

        public static bool HasMembers(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var output = typeDeclarationSyntax.Members.Count > 0;
            return output;
        }
    }
}
