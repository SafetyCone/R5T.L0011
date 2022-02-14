using System;

using Microsoft.CodeAnalysis;


namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
    public static class TypedSyntaxNode
    {
        public static TypedSyntaxNode<TNode> From<TNode>(this TNode syntaxNode)
            where TNode : SyntaxNode
        {
            var output = new TypedSyntaxNode<TNode>(syntaxNode);
            return output;
        }
    }


    public class TypedSyntaxNode<TNode>
        where TNode : SyntaxNode
    {
        #region Static

        public static implicit operator TNode(TypedSyntaxNode<TNode> typedSyntaxNode)
        {
            return typedSyntaxNode.SyntaxNode;
        }

        #endregion


        public TNode SyntaxNode { get; }


        public TypedSyntaxNode(
            TNode syntaxNode)
        {
            this.SyntaxNode = syntaxNode;
        }
    }
}
