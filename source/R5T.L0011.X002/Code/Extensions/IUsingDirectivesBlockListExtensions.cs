using System;

using R5T.L0011.T004;
using R5T.L0011.X002;

namespace System
{
    public static class IUsingDirectivesBlockListExtensions
    {
        private static IUsingDirectivesBlockLabel BlockLabel { get; } = UsingDirectivesBlockLabel.Instance;
        private static IUsingDirectivesBlockSortOrder SortOrder { get; } = UsingDirectivesBlockSortOrder.Instance;


        public static IUsingDirectivesBlockList AddTo(this IUsingDirectivesBlockList blockList, string label, params string[] namespaceNames)
        {
            var block = UsingDirectivesBlock.New(label, namespaceNames);

            blockList.Add(block);

            return blockList;
        }

        public static IUsingDirectivesBlockList AddToSystem(this IUsingDirectivesBlockList blockList, params string[] namespaceNames)
        {
            var output = blockList.AddTo(BlockLabel.System(), namespaceNames);
            return output;
        }

        public static IUsingDirectivesBlockList AddToProject(this IUsingDirectivesBlockList blockList, params string[] namespaceNames)
        {
            var output = blockList.AddTo(BlockLabel.ProjectNamespaces(), namespaceNames);
            return output;
        }

        public static IUsingDirectivesBlockList AddToUncategorized(this IUsingDirectivesBlockList blockList, params string[] namespaceNames)
        {
            var output = blockList.AddTo(BlockLabel.Uncategorized(), namespaceNames);
            return output;
        }

        public static IUsingDirectivesBlockList SortByDefault(this IUsingDirectivesBlockList blockList)
        {
            var orderedLabels = SortOrder.Default();

            blockList.SortBy(orderedLabels);

            return blockList;
        }
    }
}
