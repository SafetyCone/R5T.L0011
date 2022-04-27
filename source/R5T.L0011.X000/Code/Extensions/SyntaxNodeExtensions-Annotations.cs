using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using R5T.Magyar;

using R5T.L0011.X000;

using Glossary = R5T.L0011.X000.Glossary;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        public static T Annotate<T>(this T node,
            out SyntaxAnnotation annotation)
            where T : SyntaxNode
        {
            annotation = SyntaxFactoryHelper.Annotation();

            var output = node.WithAdditionalAnnotations(annotation);
            return output;
        }

        public static TNode AnnotateNode<TNode>(this TNode node,
            SyntaxNode descendantNode,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            annotation = SyntaxFactoryHelper.Annotation();

            var newDescendantNode = descendantNode.WithAdditionalAnnotations(annotation);

            var outputSyntaxNode = node.ReplaceNode_Better(descendantNode, newDescendantNode);

            return outputSyntaxNode;
        }

        public static TNode AnnotateNodes<TNode, TDescendantNode>(this TNode node,
            IEnumerable<TDescendantNode> descendantNodes,
            out Dictionary<TDescendantNode, SyntaxAnnotation> annotationsByInputNode)
            where TNode : SyntaxNode
            where TDescendantNode : SyntaxNode
        {
            // Temporary variable is required for use in anonymous method below.
            var tempAnotationsByInputNodes = new Dictionary<TDescendantNode, SyntaxAnnotation>();

            var outputSyntaxNode = node.ReplaceNodes_Better(
                descendantNodes,
                (originalNode, possiblyRewrittenNode) =>
                {
                    var outputNode = possiblyRewrittenNode.Annotate(out var annotation);

                    tempAnotationsByInputNodes.Add(originalNode, annotation);

                    return outputNode;
                });

            annotationsByInputNode = tempAnotationsByInputNodes;

            return outputSyntaxNode;
        }

        public static TNode AnnotateToken<TNode>(this TNode node,
            SyntaxToken descendantToken,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            annotation = SyntaxFactoryHelper.Annotation();

            var newDescendantToken = descendantToken.WithAdditionalAnnotations(annotation);

            var outputSyntaxNode = node.ReplaceToken_Better(descendantToken, newDescendantToken);

            return outputSyntaxNode;
        }

        public static TNode AnnotateTokens<TNode>(this TNode node,
            IEnumerable<SyntaxToken> syntaxTokens,
            out Dictionary<SyntaxToken, SyntaxAnnotation> annotationsByInputTokens)
            where TNode : SyntaxNode
        {
            // Temporary variable is required for use in anonymous method below.
            var tempAnotationsByInputTokens = new Dictionary<SyntaxToken, SyntaxAnnotation>();

            var outputSyntaxNode = node.ReplaceTokens_Better(
                syntaxTokens,
                (originalToken, possiblyRewrittenToken) =>
                {
                    var outputToken = possiblyRewrittenToken.Annotate(out var annotation);

                    tempAnotationsByInputTokens.Add(originalToken, annotation);

                    return outputToken;
                });

            annotationsByInputTokens = tempAnotationsByInputTokens;

            return outputSyntaxNode;
        }

        public static TNode AnnotateTrivia<TNode>(this TNode node,
            SyntaxTrivia descendantTrivia,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            annotation = SyntaxFactoryHelper.Annotation();

            var newTrivia = descendantTrivia.WithAdditionalAnnotations(annotation);

            var outputSyntaxNode = node.ReplaceTrivia_Better(descendantTrivia, newTrivia);

            return outputSyntaxNode;
        }

        public static TNode AnnotateTrivias<TNode>(this TNode node,
            IEnumerable<SyntaxTrivia> trivias,
            out Dictionary<SyntaxTrivia, SyntaxAnnotation> annotationsByInputTrivias)
            where TNode : SyntaxNode
        {
            // Temporary variable is required for use in anonymous method below.
            var tempAannotationsByInputTrivias = new Dictionary<SyntaxTrivia, SyntaxAnnotation>();

            var outputSyntaxNode = node.ReplaceTrivias_Better(
                trivias,
                (originalTrivia, _) =>
                {
                    var outputTrivia = originalTrivia.Annotate(out var annotation);

                    tempAannotationsByInputTrivias.Add(originalTrivia, annotation);

                    return outputTrivia;
                });

            annotationsByInputTrivias = tempAannotationsByInputTrivias;

            return outputSyntaxNode;
        }

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

        public static TNode ModifyToken<TNode>(this TNode node,
            SyntaxAnnotation annotation,
            Func<SyntaxToken, SyntaxToken> tokenModifer)
            where TNode : SyntaxNode
        {
            var originalToken = annotation.GetToken(node);

            var modifiedToken = tokenModifer(originalToken);

            var output = node.ReplaceToken_Better(originalToken, modifiedToken);
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

        public static TRootNode ReplaceNodes_Better<TRootNode, TNode>(this TRootNode rootNode,
            IDictionary<TNode, TNode> replacements)
            where TRootNode : SyntaxNode
            where TNode : SyntaxNode
        {
            var output = rootNode.ReplaceNodes_Better(
                replacements.Keys,
                (originalNode, _) => replacements[originalNode]);

            return output;
        }

        public static TRootNode ReplaceNodes_Better<TRootNode, TNode>(this TRootNode rootNode,
            IEnumerable<(TNode OriginalNode, TNode ReplacementNode)> replacements)
            where TRootNode : SyntaxNode
            where TNode : SyntaxNode
        {
            var replacementsDictionary = replacements
                .ToDictionary(
                    x => x.OriginalNode,
                    x => x.ReplacementNode);

            var output = rootNode.ReplaceNodes_Better(replacementsDictionary);
            return output;
        }

        public static TNode ReplaceToken_Better<TNode>(this TNode node,
            SyntaxToken oldDescendantToken,
            SyntaxToken newDescendantToken)
            where TNode : SyntaxNode
        {
            var tokenWasFound = false;

            var output = node.ReplaceTokens(
                EnumerableHelper.From(oldDescendantToken),
                (originalToken, possiblyRewrittenToken) =>
                {
                    if (originalToken == oldDescendantToken)
                    {
                        tokenWasFound = true;
                    }

                    return newDescendantToken;
                });

            if (!tokenWasFound)
            {
                throw new Exception("Token was not found in call to replace Token.");
            }

            return output;
        }

        public static TNode ReplaceTokens_Better<TNode>(this TNode node,
            IEnumerable<SyntaxToken> oldTokens,
            Func<SyntaxToken, SyntaxToken, SyntaxToken> computeReplacementToken)
            where TNode : SyntaxNode
        {
            var tokensHash = new HashSet<SyntaxToken>(oldTokens);

            var output = node.ReplaceTokens(
                oldTokens,
                (originalToken, possiblyRewrittenToken) =>
                {
                    tokensHash.Remove(originalToken);

                    var outputToken = computeReplacementToken(originalToken, possiblyRewrittenToken);
                    return outputToken;
                });

            if (tokensHash.Any())
            {
                throw new Exception("Some tokens to replace were not found.");
            }

            return output;
        }

        public static TNode ReplaceTokens_Better<TNode>(this TNode node,
            IDictionary<SyntaxToken, SyntaxToken> replacements)
            where TNode : SyntaxNode
        {
            var output = node.ReplaceTokens_Better(
                replacements.Keys,
                (originalToken, _) => replacements[originalToken]);

            return output;
        }

        public static TNode ReplaceTokens_Better<TNode>(this TNode node,
            IEnumerable<(SyntaxToken, SyntaxToken)> replacements)
            where TNode : SyntaxNode
        {
            var replacementsDictionary = replacements
                .ToDictionary(
                    x => x.Item1,
                    x => x.Item2);

            var output = node.ReplaceTokens_Better(replacementsDictionary);
            return output;
        }

        public static TNode ReplaceTrivia_Better<TNode>(this TNode node,
            SyntaxTrivia oldDescendantTrivia,
            SyntaxTrivia newDescendantTrivia)
            where TNode : SyntaxNode
        {
            var triviaWasFound = false;

            var output = node.ReplaceTrivia(
                EnumerableHelper.From(oldDescendantTrivia),
                (originalTrivia, possiblyRewrittenTrivia) =>
                {
                    if (originalTrivia == oldDescendantTrivia)
                    {
                        triviaWasFound = true;
                    }

                    return newDescendantTrivia;
                });

            if (!triviaWasFound)
            {
                throw new Exception("Trivia was not found in call to replace trivia.");
            }

            return output;
        }

        public static TNode ReplaceTrivias_Better<TNode>(this TNode node,
            IEnumerable<SyntaxTrivia> oldTrivias,
            Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> computeReplacementTrivia)
            where TNode : SyntaxNode
        {
            var triviasHash = new HashSet<SyntaxTrivia>(oldTrivias);

            var output = node.ReplaceTrivia(
                oldTrivias,
                (originalTrivia, possiblyRewrittenTrivia) =>
                {
                    triviasHash.Remove(originalTrivia);

                    var outputTrivia = computeReplacementTrivia(originalTrivia, possiblyRewrittenTrivia);
                    return outputTrivia;
                });

            if (triviasHash.Any())
            {
                throw new Exception("Some trivias to replace were not found.");
            }

            return output;
        }

        public static TNode ReplaceTrivias_Better<TNode>(this TNode node,
            IDictionary<SyntaxTrivia, SyntaxTrivia> replacements)
            where TNode : SyntaxNode
        {
            var output = node.ReplaceTrivias_Better(
                replacements.Keys,
                (originalTrivia, _) => replacements[originalTrivia]);

            return output;
        }

        public static TNode ReplaceTrivias_Better<TNode>(this TNode node,
            IEnumerable<(SyntaxTrivia, SyntaxTrivia)> replacements)
            where TNode : SyntaxNode
        {
            var replacementsDictionary = replacements
                .ToDictionary(
                    x => x.Item1,
                    x => x.Item2);

            var output = node.ReplaceTrivias_Better(replacementsDictionary);
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