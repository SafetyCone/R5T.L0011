using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class SyntaxAnnotationExtensions
    {
        public static TOut Get_ForNode<TRootNode, TNode, TOut>(this SyntaxAnnotation annotation,
            TRootNode rootNode,
            Func<TNode, TOut> selector)
            where TRootNode : SyntaxNode
            where TNode : SyntaxNode
        {
            var node = annotation.GetNode<TNode>(rootNode);

            var output = selector(node);
            return output;
        }

        public static TOut Get_ForToken<TRootNode, TOut>(this SyntaxAnnotation annotation,
            TRootNode rootNode,
            Func<SyntaxToken, TOut> selector)
            where TRootNode : SyntaxNode
        {
            var token = annotation.GetToken(rootNode);

            var output = selector(token);
            return output;
        }

        public static TOut Get_ForTrivia<TRootNode, TOut>(this SyntaxAnnotation annotation,
            TRootNode rootNode,
            Func<SyntaxTrivia, TOut> selector)
            where TRootNode : SyntaxNode
        {
            var trivia = annotation.GetTrivia(rootNode);

            var output = selector(trivia);
            return output;
        }

        public static SyntaxNode GetNode(this SyntaxAnnotation annotation,
            SyntaxNode node)
        {
            var output = node.GetAnnotatedNode(annotation);
            return output;
        }

        public static TNode GetNode<TNode>(this SyntaxAnnotation annotation,
            SyntaxNode node)
            where TNode : SyntaxNode
        {
            var output = node.GetAnnotatedNode<TNode>(annotation);
            return output;
        }

        public static SyntaxToken GetToken(this SyntaxAnnotation annotation,
            SyntaxNode node)
        {
            var output = node.GetAnnotatedToken(annotation);
            return output;
        }

        public static SyntaxTrivia GetTrivia(this SyntaxAnnotation annotation,
            SyntaxNode node)
        {
            var output = node.GetAnnotatedTriviaSingle(annotation);
            return output;
        }

        public static TNode ModifyNodeAsNode<TNode>(this SyntaxAnnotation annotation,
            TNode node,
            Func<SyntaxNode, SyntaxNode> tokenModifer)
            where TNode : SyntaxNode
        {
            var originalNode = annotation.GetNode(node);

            var modifiedNode = tokenModifer(originalNode);

            var output = node.ReplaceNode_Better(originalNode, modifiedNode);
            return output;
        }

        public static async Task<TNode> ModifyNodeAsNode<TNode>(this SyntaxAnnotation annotation,
            TNode node,
            Func<SyntaxNode, Task<SyntaxNode>> tokenModifer)
            where TNode : SyntaxNode
        {
            var originalNode = annotation.GetNode(node);

            var modifiedNode = await tokenModifer(originalNode);

            var output = node.ReplaceNode_Better(originalNode, modifiedNode);
            return output;
        }

        public static TRootNode ModifyNode<TRootNode, TNode>(this SyntaxAnnotation annotation,
            TRootNode node,
            Func<TNode, TNode> tokenModifer)
            where TRootNode : SyntaxNode
            where TNode : SyntaxNode
        {
            var originalNode = annotation.GetNode<TNode>(node);

            var modifiedNode = tokenModifer(originalNode);

            var output = node.ReplaceNode_Better(originalNode, modifiedNode);
            return output;
        }

        public static async Task<TRootNode> ModifyNode<TRootNode, TNode>(this SyntaxAnnotation annotation,
            TRootNode node,
            Func<TNode, Task<TNode>> tokenModifer)
            where TRootNode : SyntaxNode
            where TNode : SyntaxNode
        {
            var originalNode = annotation.GetNode<TNode>(node);

            var modifiedNode = await tokenModifer(originalNode);

            var output = node.ReplaceNode_Better(originalNode, modifiedNode);
            return output;
        }

        public static TNode ModifyToken<TNode>(this SyntaxAnnotation annotation,
            TNode node,
            Func<SyntaxToken, SyntaxToken> tokenModifer)
            where TNode : SyntaxNode
        {
            var originalToken = annotation.GetToken(node);

            var modifiedToken = tokenModifer(originalToken);

            var output = node.ReplaceToken_Better(originalToken, modifiedToken);
            return output;
        }

        public static async Task<TNode> ModifyToken<TNode>(this SyntaxAnnotation annotation,
            TNode node,
            Func<SyntaxToken, Task<SyntaxToken>> tokenModifer)
            where TNode : SyntaxNode
        {
            var originalToken = annotation.GetToken(node);

            var modifiedToken = await tokenModifer(originalToken);

            var output = node.ReplaceToken_Better(originalToken, modifiedToken);
            return output;
        }

        public static TNode ModifyTrivia<TNode>(this SyntaxAnnotation annotation,
            TNode node,
            Func<SyntaxTrivia, SyntaxTrivia> triviaModifer)
            where TNode : SyntaxNode
        {
            var originalTrivia = annotation.GetTrivia(node);

            var modifiedTrivia = triviaModifer(originalTrivia);

            var output = node.ReplaceTrivia_Better(originalTrivia, modifiedTrivia);
            return output;
        }

        public static async Task<TNode> ModifyTrivia<TNode>(this SyntaxAnnotation annotation,
            TNode node,
            Func<SyntaxTrivia, Task<SyntaxTrivia>> triviaModifer)
            where TNode : SyntaxNode
        {
            var originalTrivia = annotation.GetTrivia(node);

            var modifiedTrivia = await triviaModifer(originalTrivia);

            var output = node.ReplaceTrivia_Better(originalTrivia, modifiedTrivia);
            return output;
        }
    }
}
