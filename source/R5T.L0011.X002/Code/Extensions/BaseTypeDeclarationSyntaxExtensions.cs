using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class BaseTypeDeclarationSyntaxExtensions
    {
        public static WasFound<BaseTypeSyntax[]> HasBaseTypes(this BaseTypeDeclarationSyntax baseTypeDeclarationSyntax)
        {
            var hasBaseTypesList = baseTypeDeclarationSyntax.HasBaseTypesList();
            if(!hasBaseTypesList)
            {
                return WasFound.NotFound<BaseTypeSyntax[]>();
            }

            var output = WasFound.Found(
                baseTypeDeclarationSyntax.BaseList.Types.ToArray());

            return output;
        }

        public static bool HasBaseTypesList(this BaseTypeDeclarationSyntax baseTypeDeclarationSyntax)
        {
            var output = baseTypeDeclarationSyntax.BaseList is object;
            return output;
        }

        public static bool HasBaseTypeWithNamespacedTypeName(this BaseTypeDeclarationSyntax baseTypeDeclarationSyntax,
            string namespacedTypeName)
        {
            var containingNamespaceName = baseTypeDeclarationSyntax.GetContainingNamespaceName();

            var output = baseTypeDeclarationSyntax.HasBaseTypeWithNamespacedTypeName(
                containingNamespaceName,
                namespacedTypeName);

            return output;
        }

        public static bool HasBaseTypeWithNamespacedTypeName(this BaseTypeDeclarationSyntax baseTypeDeclarationSyntax,
            string baseTypeContainingNamespaceName,
            string namespacedTypeName)
        {
            // Perform initial check: are there any base types to begin with?
            var hasBaseTypesList = baseTypeDeclarationSyntax.HasBaseTypesList();
            if (!hasBaseTypesList)
            {
                return false;
            }
            // Else, now we know there is a base list.

            // Get all possible containing namespaces.
            var containingNamespaceNames = Instances.NamespaceName.EnumerateNamespaceAndSubNamespaces(baseTypeContainingNamespaceName);

            foreach (var baseTypeSyntax in baseTypeDeclarationSyntax.BaseList.Types)
            {
                var baseTypeTypeNameFragment = baseTypeSyntax.Type.GetTypeName_HandlingTypeParameters();

                // Foreach possible containing namespace, test the base type syntax type name fragment.
                foreach (var containingNamespaceName in containingNamespaceNames)
                {
                    var baseTypeNamespacedTypeName = Instances.NamespaceName.CombineTokens(containingNamespaceName, baseTypeTypeNameFragment);
                    
                    if(baseTypeNamespacedTypeName == namespacedTypeName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool HasBaseTypeWithName_NoContainingNamespace(this BaseTypeDeclarationSyntax baseTypeDeclarationSyntax,
            string typeName)
        {
            var hasBaseTypesList = baseTypeDeclarationSyntax.HasBaseTypesList();
            if(!hasBaseTypesList)
            {
                return false;
            }

            // Ok, now we know there is a base list.
            foreach (var baseTypeSyntax in baseTypeDeclarationSyntax.BaseList.Types)
            {
                var hasTypeName = baseTypeSyntax.Type.IsNamed(typeName);
                if(hasTypeName)
                {
                    return true;
                }
            }

            return false;
        }

        public static T WithBraces<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList indentation)
            where T : BaseTypeDeclarationSyntax
        {
            var modified = baseTypeDeclarationSyntax
                .WithOpenBraceToken(Instances.SyntaxFactory.OpenBrace(indentation))
                .WithCloseBraceToken(Instances.SyntaxFactory.CloseBrace(indentation))
                ;

            var output = modified as T;
            return output;
        }

        public static T WithCloseBrace<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList indentation)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithCloseBraceToken(Instances.SyntaxFactory.CloseBrace(indentation))
                as T;

            return output;
        }

        public static T WithCloseBrace2<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList leadingWhitespace, bool appendNewLine = false)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithCloseBraceToken(Instances.SyntaxFactory.CloseBrace2(leadingWhitespace, appendNewLine))
                as T;

            return output;
        }

        public static T WithOpenBrace<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList indentation)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithOpenBraceToken(Instances.SyntaxFactory.OpenBrace(indentation))
                as T;

            return output;
        }

        public static T WithOpenBrace2<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList leadingWhitespace, bool prependNewLine = true)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithOpenBraceToken(Instances.SyntaxFactory.OpenBrace2(leadingWhitespace, prependNewLine))
                as T;

            return output;
        }
    }
}
