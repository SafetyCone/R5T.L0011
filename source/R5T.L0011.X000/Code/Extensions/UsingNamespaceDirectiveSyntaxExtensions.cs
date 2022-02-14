using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class UsingNamespaceDirectiveSyntaxExtensions
    {
        public static string GetNamespaceName(this UsingNamespaceDirectiveSyntax usingNamespaceDirective)
        {
            var output = usingNamespaceDirective.SyntaxNode.GetNamespaceName();
            return output;
        }

        public static Dictionary<string, UsingNamespaceDirectiveSyntax> GetByNamespaceName(this IEnumerable<UsingNamespaceDirectiveSyntax> usingNamespaceDirectives)
        {
            var output = usingNamespaceDirectives
                .ToDictionary(
                    x => x.SyntaxNode.GetNamespaceName());

            return output;
        }
    }
}
