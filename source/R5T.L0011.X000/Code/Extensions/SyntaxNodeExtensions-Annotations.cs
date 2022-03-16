using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.Magyar;

using R5T.L0011.X000;

using Glossary = R5T.L0011.X000.Glossary;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        public static SyntaxNode GetAnnotatedNode(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var hasAnnotatedNode = syntaxNode.HasAnnotatedNode(annotation);
            if (!hasAnnotatedNode)
            {
                throw new Exception("No node with annotation found.");
            }

            return hasAnnotatedNode.Result;
        }

        public static TChildNode GetAnnotatedNode<TChildNode>(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
            where TChildNode : SyntaxNode
        {
            var hasAnnotatedNode = syntaxNode.HasAnnotatedNode<TChildNode>(annotation);
            if (!hasAnnotatedNode)
            {
                throw new Exception($"No node of type {typeof(TChildNode)} with annotation found.");
            }

            return hasAnnotatedNode.Result;
        }

        public static IEnumerable<TChildNode> GetAnnotatedNodes<TChildNode>(this SyntaxNode syntaxNode,
            SyntaxAnnotation syntaxAnnotation)
            where TChildNode : SyntaxNode
        {
            var annotatedNodes = syntaxNode.GetAnnotatedNodes(syntaxAnnotation);

            var output = annotatedNodes
                .Cast<TChildNode>()
                ;

            return output;
        }

        /// <summary>
        /// Gets annotations whose kind is the default value.
        /// <inheritdoc cref="AnnotationKindHelper.Default" path="/summary/default-value-is"/>
        /// </summary>
        public static IEnumerable<SyntaxAnnotation> GetAnnotations(this SyntaxNode node)
        {
            var output = node.GetAnnotations(AnnotationKindHelper.Default);
            return output;
        }

        public static SyntaxToken GetAnnotatedToken(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var hasAnnotatedToken = syntaxNode.HasAnnotatedToken(annotation);
            if (!hasAnnotatedToken)
            {
                throw new Exception("No token with annotation found.");
            }

            return hasAnnotatedToken.Result;
        }

        /// <summary>
        /// Awkward naming since "trivia" is both singular and plural, thus the Microsoft methods returning and enumerable do not allow differentiation between singular and plural.
        /// </summary>
        public static SyntaxTrivia GetAnnotatedTriviaSingle(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var hasAnnotatedTrivia = syntaxNode.HasAnnotatedTrivia(annotation);
            if (!hasAnnotatedTrivia)
            {
                throw new Exception("No trivia with annotation found.");
            }

            return hasAnnotatedTrivia.Result;
        }

        /// <inheritdoc cref="GetAnnotatedTriviaSingle(SyntaxNode, SyntaxAnnotation)"/>
        public static IEnumerable<SyntaxTrivia> GetAnnotatedTriviaEnumerable(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var output = syntaxNode.GetAnnotatedTrivia(annotation);
            return output;
        }

        public static WasFound<SyntaxNode> HasAnnotatedNode_First(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var annotatedNodeOrDefault = syntaxNode.GetAnnotatedNodes(annotation)
                .FirstOrDefault();

            var output = WasFound.From(annotatedNodeOrDefault);
            return output;
        }

        public static WasFound<SyntaxNode> HasAnnotatedNode_Single(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var annotatedNodeOrDefault = syntaxNode.GetAnnotatedNodes(annotation)
                .SingleOrDefault();

            var output = WasFound.From(annotatedNodeOrDefault);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasAnnotatedNode_Single(SyntaxNode, SyntaxAnnotation)"/> as the default.
        /// </summary>
        public static WasFound<SyntaxNode> HasAnnotatedNode(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var output = syntaxNode.HasAnnotatedNode_Single(annotation);
            return output;
        }

        public static WasFound<TChildNode> HasAnnotatedNode_First<TChildNode>(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
            where TChildNode : SyntaxNode
        {
            var annotatedNodeOrDefault = syntaxNode.GetAnnotatedNodes<TChildNode>(annotation)
                .FirstOrDefault();

            var output = WasFound.From(annotatedNodeOrDefault);
            return output;
        }

        public static WasFound<TChildNode> HasAnnotatedNode_Single<TChildNode>(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
            where TChildNode : SyntaxNode
        {
            var annotatedNodeOrDefault = syntaxNode.GetAnnotatedNodes<TChildNode>(annotation)
                .SingleOrDefault();

            var output = WasFound.From(annotatedNodeOrDefault);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasAnnotatedNode{TChildNode}(SyntaxNode, SyntaxAnnotation)"/> as the default.
        /// </summary>
        public static WasFound<TChildNode> HasAnnotatedNode<TChildNode>(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
            where TChildNode : SyntaxNode
        {
            var output = syntaxNode.HasAnnotatedNode_Single<TChildNode>(annotation);
            return output;
        }

        public static WasFound<SyntaxToken> HasAnnotatedToken_First(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var annotatedTokenOrDefault = syntaxNode.GetAnnotatedTokens(annotation)
                .FirstOrDefault();

            var output = WasFound.From(annotatedTokenOrDefault);
            return output;
        }

        public static WasFound<SyntaxToken> HasAnnotatedToken_Single(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var annotatedTokenOrDefault = syntaxNode.GetAnnotatedTokens(annotation)
                .SingleOrDefault();

            var output = WasFound.From(annotatedTokenOrDefault);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasAnnotatedToken_Single(SyntaxNode, SyntaxAnnotation)"/> as the default.
        /// </summary>
        public static WasFound<SyntaxToken> HasAnnotatedToken(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var output = syntaxNode.HasAnnotatedToken_Single(annotation);
            return output;
        }

        public static WasFound<SyntaxTrivia> HasAnnotatedTrivia_Single(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var triviaOrDefault = syntaxNode.GetAnnotatedTrivia(annotation)
                .SingleOrDefault();

            var output = WasFound.From(triviaOrDefault);
            return output;
        }

        public static WasFound<SyntaxTrivia> HasAnnotatedTrivia_First(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var triviaOrDefault = syntaxNode.GetAnnotatedTrivia(annotation)
                .FirstOrDefault();

            var output = WasFound.From(triviaOrDefault);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasAnnotatedTrivia_Single(SyntaxNode, SyntaxAnnotation)"/> as the default.
        /// </summary>
        public static WasFound<SyntaxTrivia> HasAnnotatedTrivia(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var output = syntaxNode.HasAnnotatedTrivia_Single(annotation);
            return output;
        }

        public static TNode ReplaceNode_Better<TNode>(this TNode node,
            SyntaxNode oldDescendantNode,
            SyntaxNode newDescendantNode)
            where TNode : SyntaxNode
        {
            var nodeWasFound = false;

            var output = node.ReplaceNodes(
                EnumerableHelper.From(oldDescendantNode),
                (originalNode, possiblyRewrittenNode) =>
                {
                    if(originalNode == oldDescendantNode)
                    {
                        nodeWasFound = true;
                    }

                    return newDescendantNode;
                });

            if(!nodeWasFound)
            {
                throw new Exception("Node was not found in call to replace node.");
            }

            return output;
        }

        /// <inheritdoc cref="Microsoft.CodeAnalysis.SyntaxNodeExtensions.ReplaceNodes{TRoot, TNode}(TRoot, IEnumerable{TNode}, Func{TNode, TNode, SyntaxNode})"/>
        public static TRootNode ReplaceNodes_Better<TRootNode, TNode>(this TRootNode rootNode,
            IEnumerable<TNode> oldNodes,
            Func<TNode, TNode, SyntaxNode> computeReplacementNode)
            where TRootNode : SyntaxNode
            where TNode : SyntaxNode
        {
            var nodesHash = new HashSet<SyntaxNode>(oldNodes);

            var output = rootNode.ReplaceNodes(
                oldNodes,
                (originalNode, possiblyRewrittenNode) =>
                {
                    nodesHash.Remove(originalNode);

                    var outputNode = computeReplacementNode(originalNode, possiblyRewrittenNode);
                    return outputNode;
                });

            if(nodesHash.Any())
            {
                throw new Exception("Some nodes to replace were not found.");
            }

            return output;
        }

        public static TNode RemoveAnnotation<TNode>(this TNode node,
            SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            var output = node.WithoutAnnotations(annotation);
            return output;
        }
    }
}