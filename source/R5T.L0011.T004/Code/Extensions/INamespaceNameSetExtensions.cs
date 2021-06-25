using System;
using System.Collections.Generic;
using System.Linq;

using R5T.Magyar;

using R5T.L0011.T004;


namespace System
{
    public static class INamespaceNameSetExtensions
    {
        public static INamespaceNameSet Add(this INamespaceNameSet set, string namespaceName)
        {
            set.AddValue(namespaceName);

            return set; 
        }

        public static T AddRange<T>(this T set, params string[] namespaceNames)
            where T : INamespaceNameSet
        {
            foreach (var namespaceName in namespaceNames)
            {
                set.Add(namespaceName);
            }

            return set;
        }

        public static INamespaceNameSet Remove(this INamespaceNameSet set, string namespaceName)
        {
            set.RemoveValue(namespaceName);

            return set;
        }

        public static Dictionary<string, string[]> Label(this INamespaceNameSet set, ILabeler<string> namespaceLabeler)
        {
            var output = set.NamespaceNames
                .GroupBy(
                    namespaceName => namespaceLabeler.Label(namespaceName))
                .ToDictionary(
                    x => x.Key,
                    x => x.ToArray());

            return output;
        }

        public static IUsingDirectivesBlockList GetBlocks(this INamespaceNameSet set, ILabeler<string> namespaceLabeler)
        {
            var labeledNamespaceNames = set.Label(namespaceLabeler);

            var output = UsingDirectivesBlockList.New();

            foreach (var label in labeledNamespaceNames.Keys)
            {
                var namespaceNamesInInitialOrder = labeledNamespaceNames[label];

                var namespaceNames = namespaceNamesInInitialOrder
                    .OrderBy(x => x)
                    .ToArray();

                var block = UsingDirectivesBlock.New(label, namespaceNames);

                output.Add(block);
            }

            return output;
        }
    }
}
