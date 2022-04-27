using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T004;


namespace System
{
    public static class UsingDirectiveSyntaxExtensions
    {
        public static UsingDirectivesSpecification GetUsingDirectivesSpecification(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = new UsingDirectivesSpecification();

            var usingNamespaceDirectives = usingDirectives.GetUsingNamespaceDirectiveSyntaxes();

            var usingNamespaceNames = usingNamespaceDirectives
                .Select(xUsingNamespaceDirective => xUsingNamespaceDirective.GetNamespaceName())
                ;

            output.UsingNamespaceNames.AddRange(usingNamespaceNames);

            var nameAliasDirectives = usingDirectives.GetUsingNameAliasDirectiveSyntaxes();

            var nameAliases = nameAliasDirectives
                .Select(xNameAlias =>
                {
                    var (destinationName, sourceNameExpression) = xNameAlias.GetNameAliasValues();

                    var output = NameAlias.From(destinationName, sourceNameExpression);
                    return output;
                });

            output.NameAliases.AddRange(nameAliases);

            return output;
        }
    }
}
