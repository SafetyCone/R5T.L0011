using System;
using System.Collections.Generic;
using System.Linq;


namespace R5T.L0011.T004
{
    public static class IUsingDirectivesBlockListExtensions
    {
        /// <summary>
        /// Adds a the values of a using directive block, handling merging of namespace names if the block exists already.
        /// </summary>
        public static void AddNoHassle(this IUsingDirectivesBlockList blockList, IUsingDirectivesBlock block)
        {
            var label = block.Label;

            var listContainsLabel = blockList.ContainsLabel(label);

            var existingBlock = listContainsLabel
                ? blockList.GetBlock(label)
                : blockList.AddNewBlock(label);

            var mergedNamespaces = existingBlock.NamespaceNames.Union(block.NamespaceNames)
                .ToArray();

            var newBlock = UsingDirectivesBlock.New(label, mergedNamespaces);

            blockList.Replace(newBlock);
        }

        /// <summary>
        /// Selects <see cref="AddNoHassle(IUsingDirectivesBlockList, IUsingDirectivesBlock)"/> as the default add method.
        /// </summary>
        public static void Add(this IUsingDirectivesBlockList blockList, IUsingDirectivesBlock block)
        {
            blockList.AddNoHassle(block);
        }

        public static IUsingDirectivesBlock AddNewBlock(this IUsingDirectivesBlockList blockList, string label)
        {
            var block = UsingDirectivesBlock.New(label);

            blockList.Blocks.Add(block);

            return block;
        }

        public static bool ContainsLabel(this IUsingDirectivesBlockList blockList, string label)
        {
            var output = blockList.WhereLabelIs(label).Any();
            return output;
        }

        public static IUsingDirectivesBlock GetBlock(this IUsingDirectivesBlockList blockList, string label)
        {
            var output = blockList.WhereLabelIs(label).Single(); // Throw if more or less than one.
            return output;
        }

        public static void Replace(this IUsingDirectivesBlockList blockList, IUsingDirectivesBlock block)
        {
            var label = block.Label;

            var hasLabel = blockList.ContainsLabel(label);
            if(hasLabel)
            {
                var exisingBlock = blockList.GetBlock(label);

                blockList.Blocks.Remove(exisingBlock);
            }

            blockList.Blocks.Add(block);
        }

        public static void SortBy(this IUsingDirectivesBlockList blockList, string[] orderedLabels)
        {
            var blocksByLabel = blockList.Blocks
                .ToDictionary(
                    x => x.Label);

            var orderedBlocks = new List<IUsingDirectivesBlock>();

            // Add blocks by ordered label (if they exist).
            foreach (var label in orderedLabels)
            {
                if(blocksByLabel.ContainsKey(label))
                {
                    orderedBlocks.Add(blocksByLabel[label]);

                    blocksByLabel.Remove(label);
                }
            }

            // Remaining blocks are added alphabetically by label.
            var remainingLabelsAlphabetically = blocksByLabel.Keys
                .OrderBy(x => x)
                .ToArray();

            foreach (var label in remainingLabelsAlphabetically)
            {
                orderedBlocks.Add(blocksByLabel[label]);
            }

            // Reset the block list.
            blockList.Blocks.Clear();
            blockList.Blocks.AddRange(orderedBlocks);
        }

        public static IEnumerable<IUsingDirectivesBlock> WhereLabelIs(this IUsingDirectivesBlockList blockList, string label)
        {
            var output = blockList.Blocks
                .Where(block => block.Label == label);

            return output;
        }
    }
}
