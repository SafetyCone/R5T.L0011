using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class UsingDirectiveSyntaxExtensions
    {
        public static string GetQualifiedName(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.Name.ToString();
            return output;
        }

        public static string GetNamespaceName(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.GetQualifiedName();
            return output;
        }

        public static string GetNameEquals(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.Alias
                .GetChildOfType<IdentifierNameSyntax>()
                .Identifier.ToString();

            return output;
        }

        public static string GetNameAlias(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.GetNameEquals();
            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNamespaceDirectives(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsUsingNamespaceDirective())
                ;

            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetNameAliasDirectives(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsNameAliasDirective())
                ;

            return output;
        }

        public static bool HasNameEqualsChildNode(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.HasChildOfType<NameEqualsSyntax>();
            return output;
        }

        public static bool IsNameAliasDirective(this UsingDirectiveSyntax usingDirective)
        {
            // If the node has a NameEquals child node, then it is a name alias directive. Else, it is a using namespace directive.
            var output = usingDirective.HasNameEqualsChildNode();
            return output;
        }

        public static bool IsUsingNamespaceDirective(this UsingDirectiveSyntax usingDirective)
        {
            // If the node has a NameEquals child node, then it is a name alias directive. Else, it is a using namespace directive.
            var output = !usingDirective.HasNameEqualsChildNode();
            return output;
        }
    }
}
