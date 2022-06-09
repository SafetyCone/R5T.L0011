using System;

using Microsoft.CodeAnalysis;


namespace R5T.L0011
{
    public class AnnotatedNode<TNode>
        where TNode : SyntaxNode
    {
        #region Static

        public static explicit operator TNode(AnnotatedNode<TNode> annotatedNode)
        {
            return annotatedNode.Node;
        }

        public static explicit operator SyntaxAnnotation(AnnotatedNode<TNode> annotatedNode)
        {
            return annotatedNode.Annotation;
        }

        #endregion


        public TNode Node { get; }
        public SyntaxAnnotation Annotation { get; }


        public AnnotatedNode(
            TNode node,
            SyntaxAnnotation annotation)
        {
            this.Node = node;
            this.Annotation = annotation;
        }

        public void Deconstruct(out TNode node, out SyntaxAnnotation annotation)
        {
            node = this.Node;
            annotation = this.Annotation;
        }
    }


    public static class AnnotatedNode
    {
        public static AnnotatedNode<TNode> From<TNode>(
            TNode node,
            SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            var output = new AnnotatedNode<TNode>(
                node,
                annotation);

            return output;
        }

        public static AnnotatedNode<TNode> From<TNode>(TNode node)
            where TNode : SyntaxNode
        {
            node = node.Annotate(out var annotation);

            var output = AnnotatedNode.From(
                node,
                annotation);

            return output;
        }
    }
}
