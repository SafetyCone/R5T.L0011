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

            var usingNamespaceDirectives = usingDirectives.GetUsingNamespaceDirectives();

            var usingNamespaceNames = usingNamespaceDirectives
                .Select(xUsingNamespaceDirective => xUsingNamespaceDirective.GetNamespaceName())
                ;

            output.UsingNamespaceNames.AddRange(usingNamespaceNames);

            var nameAliasDirectives = usingDirectives.GetNameAliasDirectives();

            var nameAliases = nameAliasDirectives
                .Select(xNameAlias =>
                {
                    var destinationName = xNameAlias.GetNameEquals();
                    var sourceNameExpression = xNameAlias.GetQualifiedName();

                    return new NameAlias
                    {
                        DestinationName = destinationName,
                        SourceNameExpression = sourceNameExpression,
                    };
                });

            output.NameAliases.AddRange(nameAliases);

            return output;
        }
    }
}
