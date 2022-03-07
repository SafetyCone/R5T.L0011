using System;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class SyntaxListExtensions
    {
        public static int IndexOf_EnsureExists<TNode>(this SyntaxList<TNode> nodes,
            SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            var indexOf = nodes.IndexOf(node =>
            {
                var output = node.HasAnnotation(annotation);
                return output;
            });

            if (IndexHelper.IsNotFound(indexOf))
            {
                throw new Exception("Node not found in syntax list.");
            }

            return indexOf;
        }

        public static int IndexOf_EnsureExists<TNode>(this SyntaxList<TNode> nodes,
            TNode node)
            where TNode : SyntaxNode
        {
            var indexOf = nodes.IndexOf(node);

            if(IndexHelper.IsNotFound(indexOf))
            {
                throw new Exception("Node not found in syntax list.");
            }

            return indexOf;
        }
    }
}
