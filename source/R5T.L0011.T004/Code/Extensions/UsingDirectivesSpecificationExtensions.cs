using System;
using System.Collections.Generic;
using System.Linq;

using R5T.Magyar;

using R5T.L0011.T004;


namespace System
{
    public static class UsingDirectivesSpecificationExtensions
    {
        public static UsingDirectivesSpecification AddAliases(this UsingDirectivesSpecification usingDirectivesSpecification,
            params (string DestinationName, string SourceNameExpression)[] nameAliasPairs)
        {
            var nameAliases = nameAliasPairs
                .Select(x => new NameAlias
                {
                    DestinationName = x.DestinationName,
                    SourceNameExpression = x.SourceNameExpression,
                });

            usingDirectivesSpecification.AddAliases(nameAliases);

            return usingDirectivesSpecification;
        }

        public static UsingDirectivesSpecification AddAliases(this UsingDirectivesSpecification usingDirectivesSpecification,
            params NameAlias[] nameAliases)
        {
            usingDirectivesSpecification.AddAliases(nameAliases.AsEnumerable());

            return usingDirectivesSpecification;
        }

        public static UsingDirectivesSpecification AddAliases(this UsingDirectivesSpecification usingDirectivesSpecification,
            IEnumerable<NameAlias> nameAliases)
        {
            foreach (var nameAlias in nameAliases)
            {
                var hasAliasAlready = usingDirectivesSpecification.HasNameAliasFor(nameAlias.DestinationName);
                if(hasAliasAlready)
                {
                    var existingNameAlias = usingDirectivesSpecification.NameAliases.Where(x => x.DestinationName == nameAlias.DestinationName).Single();

                    // Set the source expression, if not the same.
                    var sourceExpressionsAreTheSame = existingNameAlias.SourceNameExpression == nameAlias.SourceNameExpression;
                    if (!sourceExpressionsAreTheSame)
                    {
                        existingNameAlias.SourceNameExpression = nameAlias.SourceNameExpression;
                    }
                }
                else
                {
                    // Can just add.
                    usingDirectivesSpecification.NameAliases.Add(nameAlias);
                }
            }

            return usingDirectivesSpecification;
        }

        public static UsingDirectivesSpecification AddUsingNamespaceName(this UsingDirectivesSpecification usingDirectivesSpecification,
            string namespaceName)
        {
            usingDirectivesSpecification.AddUsingNamespaceNames(namespaceName);

            return usingDirectivesSpecification;
        }

        public static UsingDirectivesSpecification AddUsingNamespaceNames(this UsingDirectivesSpecification usingDirectivesSpecification,
            IEnumerable<string> namespaceNames)
        {
            // Only add new.
            var newNamespaceNames = namespaceNames.Except(usingDirectivesSpecification.UsingNamespaceNames);

            usingDirectivesSpecification.UsingNamespaceNames.AddRange(newNamespaceNames);

            return usingDirectivesSpecification;
        }

        public static UsingDirectivesSpecification AddUsingNamespaceNames(this UsingDirectivesSpecification usingDirectivesSpecification,
            params string[] namespaceNames)
        {
            usingDirectivesSpecification.AddUsingNamespaceNames(namespaceNames.AsEnumerable());

            return usingDirectivesSpecification;
        }

        public static NamespaceNameSet GetNamespaceNameSet(this UsingDirectivesSpecification usingDirectivesSpecification)
        {
            var namespaceNames = NamespaceNameSet.New();

            namespaceNames.AddRange(usingDirectivesSpecification.UsingNamespaceNames);

            return namespaceNames;
        }

        public static IEnumerable<string> GetUsingNamespaceNamesIncludingEmpty(this UsingDirectivesSpecification usingDirectivesSpecification)
        {
            var output = usingDirectivesSpecification.UsingNamespaceNames.Append(String.Empty);
            return output;
        }

        public static WasFound<NameAlias> HasNameAliasFor(this UsingDirectivesSpecification usingDirectivesSpecification,
            string name)
        {
            var nameAliasOrDefault = usingDirectivesSpecification.NameAliases
                .Where(xNameAlias => xNameAlias.DestinationName == name)
                .SingleOrDefault();

            var output = WasFound.From(nameAliasOrDefault);
            return output;
        }
    }
}
