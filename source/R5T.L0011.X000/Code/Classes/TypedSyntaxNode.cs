using System;

using Microsoft.CodeAnalysis;


namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
    /// <summary>
    /// A type wrapper around a syntax node.
    /// Useful for distinguising between types of nodes that you consider to be different despite have the same syntax node type according to the Roslyn framework.
    /// For example: a using-namespace declaration and a using-name alias declaration are both typed as <see cref="UsingDirectiveSyntax"/>s.
    /// </summary>
    /// <typeparam name="TNode">The underlying syntax node type.</typeparam>
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


    /// <summary>
    /// Static helper class for <see cref="TypedSyntaxNode{TNode}"/>.
    /// </summary>
    public static class TypedSyntaxNode
    {
        public static TypedSyntaxNode<TNode> From<TNode>(this TNode syntaxNode)
            where TNode : SyntaxNode
        {
            var output = new TypedSyntaxNode<TNode>(syntaxNode);
            return output;
        }
    }
}
