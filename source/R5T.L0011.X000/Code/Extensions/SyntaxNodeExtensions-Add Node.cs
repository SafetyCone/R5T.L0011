using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        /// <summary>
        /// Method establishing a framework for adding a node to a parent node.
        /// Framework retrns a new parent node instance modified to include the added node, and an annotation to the added node to make it easy to find and modify.
        /// The annotation is untyped, requiring the caller to specify the type of the node.
        /// </summary>
        public static (TParentNode, SyntaxAnnotation) AddNode_UntypedAnnotation<TParentNode, TNode>(this TParentNode parentNode,
            TNode node,
            Func<TNode, TNode> preAdd,
            Func<TParentNode, TNode, TParentNode> add,
            Func<TParentNode, SyntaxAnnotation, TParentNode> postAdd)
            where TParentNode : SyntaxNode
            where TNode : SyntaxNode
        {
            node = preAdd(node);

            node = node.Annotate(out var annotation);

            parentNode = add(parentNode, node);

            parentNode = postAdd(parentNode, annotation);

            return (parentNode, annotation);
        }

        /// <inheritdoc cref="AddNode_UntypedAnnotation{TParentNode, TNode}(TParentNode, TNode, Func{TNode, TNode}, Func{TParentNode, TNode, TParentNode}, Func{TParentNode, SyntaxAnnotation, TParentNode})"/>
        public static async Task<(TParentNode, SyntaxAnnotation)> AddNode_UntypedAnnotation<TParentNode, TNode>(this TParentNode parentNode,
            TNode node,
            Func<TNode, Task<TNode>> preAdd,
            Func<TParentNode, TNode, Task<TParentNode>> add,
            Func<TParentNode, SyntaxAnnotation, Task<TParentNode>> postAdd)
            where TParentNode : SyntaxNode
            where TNode : SyntaxNode
        {
            node = await preAdd(node);

            node = node.Annotate(out var annotation);

            parentNode = await add(parentNode, node);

            parentNode = await postAdd(parentNode, annotation);

            return (parentNode, annotation);
        }
    }
}