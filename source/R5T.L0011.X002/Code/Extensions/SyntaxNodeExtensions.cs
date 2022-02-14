using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.L0011.T004;

using Instances = R5T.L0011.X002.Instances;

using LineSeparatorTriviaDocumentation = R5T.L0011.X001.Documentation;


namespace System
{
    public static class SyntaxNodeExtensions
    {
        public static T Annotate<T>(this T node,
            out SyntaxAnnotation annotation)
            where T : SyntaxNode
        {
            annotation = Instances.SyntaxFactory.Annotation();

            var output = node.WithAdditionalAnnotations(annotation);
            return output;
        }

        public static (T, TChild, SyntaxAnnotation) AnnotateChild<T, TChild>(this T node,
            TChild childNode)
            where T : SyntaxNode
            where TChild : SyntaxNode
        {
            var annotation = Instances.SyntaxFactory.Annotation();

            var outputChildSyntaxNode = childNode.WithAdditionalAnnotations(annotation);

            var outputSyntaxNode = node.ReplaceNode(childNode, outputChildSyntaxNode);

            return (outputSyntaxNode, outputChildSyntaxNode, annotation);
        }

        public static TNode AnnotateToken<TNode>(this TNode node,
            SyntaxToken descendantToken,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            annotation = Instances.SyntaxFactory.Annotation();

            var newToken = descendantToken.WithAdditionalAnnotations(annotation);

            var outputSyntaxNode = node.ReplaceToken(descendantToken, newToken);

            return outputSyntaxNode;
        }

        public static TNode AnnotateNode<TNode>(this TNode node,
            SyntaxNode descendantNode,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            annotation = Instances.SyntaxFactory.Annotation();

            var newNode = descendantNode.WithAdditionalAnnotations(annotation);

            var outputSyntaxNode = node.ReplaceNode_Better(descendantNode, newNode);

            return outputSyntaxNode;
        }

        public static TNode AnnotateNode<TNode, TDescendantNode>(this TNode node,
            TDescendantNode descendantNode,
            out SyntaxAnnotation annotation,
            out TDescendantNode annotatedDescendantNode)
            where TNode : SyntaxNode
            where TDescendantNode : SyntaxNode
        {
            annotation = Instances.SyntaxFactory.Annotation();

            annotatedDescendantNode = descendantNode.WithAdditionalAnnotations(annotation);

            var outputSyntaxNode = node.ReplaceNode_Better(descendantNode, annotatedDescendantNode);

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
                (_, possiblyRewrittenNode) =>
                {
                    var outputNode = possiblyRewrittenNode.Annotate(out var annotation);

                    tempAnotationsByInputNodes.Add(possiblyRewrittenNode, annotation);

                    return outputNode;
                });

            annotationsByInputNode = tempAnotationsByInputNodes;

            return outputSyntaxNode;
        }

        public static TNode AnnotateTokens<TNode>(this TNode node,
            IEnumerable<SyntaxToken> syntaxTokens,
            out Dictionary<SyntaxToken, SyntaxAnnotation> annotationsByInputTokens)
            where TNode : SyntaxNode
        {
            // Temporary variable is required for use in anonymous method below.
            var tempAnotationsByInputTokens = new Dictionary<SyntaxToken, SyntaxAnnotation>();

            var outputSyntaxNode = node.ReplaceTokens(
                syntaxTokens,
                (originalToken, _) =>
                {
                    var outputTrivia = originalToken.Annotate(out var annotation);

                    tempAnotationsByInputTokens.Add(originalToken, annotation);

                    return outputTrivia;
                });

            annotationsByInputTokens = tempAnotationsByInputTokens;

            return outputSyntaxNode;
        }

        public static TNode AnnotateTrivias<TNode>(this TNode node,
            IEnumerable<SyntaxTrivia> trivias,
            out Dictionary<SyntaxTrivia, SyntaxAnnotation> annotationsByInputTrivias)
            where TNode : SyntaxNode
        {
            // Temporary variable is required for use in anonymous method below.
            var tempAannotationsByInputTrivias = new Dictionary<SyntaxTrivia, SyntaxAnnotation>();

            var outputSyntaxNode = node.ReplaceTrivia(
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

        public static TSyntaxNode AddLineStart<TSyntaxNode>(this TSyntaxNode node,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = node.AddLeadingLeadingTrivia(leadingWhitespace.ToArray());
            return output;
        }

        public static TSyntaxNode AddLineStart2<TSyntaxNode>(this TSyntaxNode node,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = node.AddLeadingLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static TSyntaxNode AppendBlankLine<TSyntaxNode>(this TSyntaxNode node)
            where TSyntaxNode : SyntaxNode
        {
            var output = node.AddTrailingTrailingTrivia(Instances.SyntaxFactory.NewLine());
            return output;
        }

        public static TSyntaxNode AppendBlankLine<TSyntaxNode>(this TSyntaxNode node,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var output = node.AddTrailingTrailingTrivia(indentation.ToArray());
            return output;
        }

        /// <summary>
        /// Old and bad.
        /// </summary>
        public static TSyntaxNode AppendBlankLine2<TSyntaxNode>(this TSyntaxNode node,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = node.AddTrailingTrailingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static TSyntaxNode InsertLineStart<TSyntaxNode>(this TSyntaxNode node,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var output = node.AddTrailingLeadingTrivia(indentation.ToArray());
            return output;
        }

        public static TSyntaxNode InsertLineStart2<TSyntaxNode>(this TSyntaxNode node,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = node.AddTrailingLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static TSyntaxNode PrependBlankLine<TSyntaxNode>(this TSyntaxNode node,
            SyntaxTriviaList leadingWhitespace,
            bool actuallyPrependTheLine = true)
            where TSyntaxNode : SyntaxNode
        {
            if(!actuallyPrependTheLine)
            {
                return node;
            }

            var output = node.AddLeadingLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static IEnumerable<TSyntaxNode> PrependBlankLine<TSyntaxNode>(this IEnumerable<TSyntaxNode> nodes)
            where TSyntaxNode : SyntaxNode
        {
            var output = nodes
                .Select(xSyntaxNode => xSyntaxNode.PrependBlankLine())
                ;

            return output;
        }

        public static TSyntaxNode ModifyWith<TSyntaxNode>(this TSyntaxNode node,
            SyntaxNodeModifier<TSyntaxNode> modifier,
            SyntaxTriviaList indentation,
            INamespaceNameSet namespaceNames)
            where TSyntaxNode : SyntaxNode
        {
            var output = modifier is object
                ? modifier(node, indentation, namespaceNames)
                : node;

            return output;
        }

        /// <summary>
        /// Due to the immutable nature of syntax nodes, it can be very tricky to find and modify two nodes 
        /// </summary>
        public static TNode ModifyDescendantTokensSimultaneouslySynchronous<TNode>(this TNode node,
            SyntaxAnnotation token1Annotation,
            Func<SyntaxToken, SyntaxToken> token1Modifier,
            SyntaxAnnotation token2Annotation,
            Func<SyntaxToken, SyntaxToken> token2Modifier)
            where TNode : SyntaxNode
        {
            var outputNode = node;

            var token1 = outputNode.GetAnnotatedToken(token1Annotation);

            var newToken1 = token1Modifier(token1); // Allow modifier to decide whether to remove annotations.

            outputNode = outputNode.ReplaceToken(token1, newToken1);

            var token2 = outputNode.GetAnnotatedToken(token2Annotation);

            var newToken2 = token2Modifier(token2);

            outputNode = outputNode.ReplaceToken(token2, newToken2);

            return outputNode;
        }

        /// <summary>
        /// Removes all trailing trivia from all descendant tokens and prepends it to the leading trivia of the following node.
        /// </summary>
        public static TNode MoveDescendantTrailingTriviaToLeadingTrivia<TNode>(this TNode node)
            where TNode: SyntaxNode
        {
            var outputNode = node;

            var descendantTokensWithTrailingTrivia = node.DescendantTokens()
                .Where(xToken => xToken.HasTrailingTrivia);

            outputNode = outputNode.AnnotateTokens(descendantTokensWithTrailingTrivia,
                out var annotationsByToken);

            foreach (var tokenAnnotation in annotationsByToken.Values)
            {
                var token = outputNode.GetAnnotatedToken(tokenAnnotation);

                var nextToken = token.GetNextToken();

                outputNode = outputNode.AnnotateToken(nextToken,
                    out var nextTokenAnnotation);

                var tokenTrailingTrivia = token.TrailingTrivia;

                outputNode = outputNode.ModifyDescendantTokensSimultaneouslySynchronous(
                    tokenAnnotation,
                    originalToken =>
                    {
                        var outputToken = originalToken.WithoutTrailingTrivia();
                        return outputToken;
                    },
                    nextTokenAnnotation,
                    originalNextToken =>
                    {
                        var outputNextToken = originalNextToken.AddLeadingLeadingTrivia(tokenTrailingTrivia);
                        return outputNextToken;
                    });
            }

            return outputNode;
        }

        ///// <summary>
        ///// For all descendant tokens, moves all line separator trivia found in trailing trivia to the leading trivia of the next node.
        ///// <para><inheritdoc cref="LineSeparatorTriviaDocumentation.LineSeparatorTrivia" path="/definition"/></para>
        ///// </summary>
        //public static TNode MoveDescendantLineSeparatorTriviaToLeadingTrivia<TNode>(this TNode node)
        //    where TNode : SyntaxNode
        //{

        //}

        public static TNode MoveAllNewLinesToLeadingTrivia<TNode>(this TNode node)
            where TNode : SyntaxNode
        {
            var outputNode = node;

            // Find all new line tokens in trailing trivia.
            var trailingNewLineTrivias = outputNode.DescendantTrivia()
                .Where(xTrivia => xTrivia.IsNewLine() && xTrivia.IsInTrailingTrivia())
                ;

            // For each new line that is in a trailing trivia, remove it from it's token's trailing trivia and move it to the start of the next token's leading trivia.
            // Annotate each trivia so they can be found.
            outputNode = outputNode.AnnotateTrivias(trailingNewLineTrivias,
                out var annotationsByTrailingNewLineTrivias);

            foreach (var trailingNewLineTrivia in trailingNewLineTrivias)
            {
                var trailingNewLineTriviaAnnotation = annotationsByTrailingNewLineTrivias[trailingNewLineTrivia];

                var currentCopyOfTrivia = outputNode.GetAnnotatedTriviaSingle(trailingNewLineTriviaAnnotation);

                var token = currentCopyOfTrivia.Token;

                var newToken = token.WithTrailingTrivia(token.TrailingTrivia.Remove(currentCopyOfTrivia))
                    .Annotate(out var tokenAnnotation); // Annotate for finding after replacement.

                outputNode = outputNode.ReplaceToken(token, newToken);

                var newTokenInNewTree = outputNode.GetAnnotatedToken(tokenAnnotation);

                var nextToken = newTokenInNewTree.GetNextToken();

                var newNextToken = nextToken.AddLeadingLeadingTrivia(
                    currentCopyOfTrivia
                        .WithoutAnnotations()) // Remove annotations from the trivia.
                    .WithoutAnnotations(); // Remove annotations from the token.

                outputNode = outputNode.ReplaceToken(nextToken, newNextToken);
            }

            return outputNode;
        }

        public static TSyntaxNode WrapWithRegion<TSyntaxNode>(this TSyntaxNode node,
            string regionName,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var region = Instances.SyntaxFactory.Region(indentation, regionName);

            var endRegion = Instances.SyntaxFactory.EndRegion(indentation);

            var output = node
                .AddLeadingLeadingTrivia(region)
                .AddTrailingTrailingTrivia(endRegion)
                ;

            return output;
        }

        public static TSyntaxNode RemoveLeadingBlankLine<TSyntaxNode>(this TSyntaxNode node)
            where TSyntaxNode : SyntaxNode
        {
            var output = node.WithLeadingTrivia(node.GetLeadingTrivia().RemoveLeadingNewLine());
            return output;
        }

        public static IEnumerable<TSyntaxNode> IndentBlock<TSyntaxNode>(this IEnumerable<TSyntaxNode> syntaxNodes,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNodes
                .Select(xSyntaxNode => xSyntaxNode.IndentBlock(indentation))
                ;

            return output;
        }

        public static TSyntaxNode IndentBlock<TSyntaxNode>(this TSyntaxNode node,
            SyntaxTriviaList indentation,
            bool prependNewLineToFirstToken = true)
            where TSyntaxNode : SyntaxNode
        {
            var outputNode = node;

            // Get tabination of indentation for later use.
            var tabination = indentation.GetTabination();

            // Prepend a new line to the leading trivia of the first token if desired (the new line will cause tabination to be added later, just like with all the other new lines).
            // Else, just add tabination.
            var firstToken = outputNode.GetFirstToken();

            if (prependNewLineToFirstToken)
            {
                var newFirstToken = firstToken.PrependNewLine();

                outputNode = outputNode.ReplaceToken(firstToken, newFirstToken);
            }
            else
            {
                var newFirstToken = firstToken.AddLeadingLeadingTrivia(tabination);

                outputNode = outputNode.ReplaceToken(firstToken, newFirstToken);
            }            

            // Now add tabination after each new line found.
            // Get all descendant end-of-line trivias in the block.
            var endOfLineTrivias = outputNode.DescendantTrivia()
                .Where(xTrivia => xTrivia.IsEndOfLine())
                .ToArray();

            // For each end-of-line trivia, get it's parent, and accumulate unique parents.
            var parentTokens = new HashSet<SyntaxToken>();
            foreach (var endOfLineTrivia in endOfLineTrivias)
            {
                var parentToken = endOfLineTrivia.Token;

                parentTokens.Add(parentToken);
            }

            // For each parent of an end-of-line trivia, suffix all end-of-lines trivias with the tabination.
            outputNode = outputNode.ReplaceTokens(parentTokens,
                (originalToken, token) =>
                {
                    var outputToken = token;

                    var leadingTrivia = outputToken.LeadingTrivia;

                    for (int iIndex = 0; iIndex < leadingTrivia.Count; iIndex++)
                    {
                        var currentTrivia = leadingTrivia[iIndex];

                        if (currentTrivia.IsEndOfLine())
                        {
                            leadingTrivia = leadingTrivia.InsertRange(iIndex + 1, tabination);
                        }
                    }

                    outputToken = outputToken.WithLeadingTrivia(leadingTrivia);

                    var trailingTrivia = outputToken.TrailingTrivia;

                    for (int iIndex = 0; iIndex < trailingTrivia.Count; iIndex++)
                    {
                        var currentTrivia = trailingTrivia[iIndex];

                        if (currentTrivia.IsEndOfLine())
                        {
                            trailingTrivia = trailingTrivia.InsertRange(iIndex + 1, tabination);
                        }
                    }

                    outputToken = outputToken.WithTrailingTrivia(trailingTrivia);

                    return outputToken;
                });

            return outputNode;

            //var output = syntaxNode;

            //var trivia = output.DescendantTrivia().First();
            //trivia.Token.

            //var nodesWithTrailingEndOfLine = output.DescendantNodes()
            //    .Where(xNode =>
            //    {
            //        if(xNode.HasTrailingTrivia)
            //        {
            //            var trailingTrivia = xNode.GetTrailingTrivia();
            //            if(trailingTrivia
            //                .Where(xTrivia => xTrivia.IsEndOfLine())
            //                .Any())
            //            {
            //                return true;
            //            }
            //        }

            //        return false;
            //    })
            //    .ToArray();

            //output = output.ReplaceNodes(
            //    nodesWithTrailingEndOfLine,
            //    (existingNode, modifiedNode) =>
            //    {
            //        var outputNode = modifiedNode.WithTrailingTrivia(indentation);
            //        return outputNode;
            //    });

            //var tokensWithTrailingEndOfLine = output.DescendantTokens()
            //    .Where(xToken =>
            //    {
            //        if (xToken.HasTrailingTrivia)
            //        {
            //            if (xToken.TrailingTrivia
            //                .Where(xTrivia => xTrivia.IsEndOfLine())
            //                .Any())
            //            {
            //                return true;
            //            }
            //        }

            //        return false;
            //    })
            //    .ToArray();

            //output = output.ReplaceTokens(
            //    tokensWithTrailingEndOfLine,
            //    (existingToken, modifiedToken) =>
            //    {
            //        var outputNode = modifiedToken.WithTrailingTrivia(indentation);
            //        return outputNode;
            //    });

            //return output;
        }

        /// <summary>
        /// The Roslyn parser always assumes whitespace is trailing.
        /// Thus when inserting a node when the prior node is correctly indented, the prior node will have the trailing newline, so the inserted node only needs tabination. But it then needs its own new line.
        /// </summary>
        public static TSyntaxNode IndentForInsertion<TSyntaxNode>(this TSyntaxNode node,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var output = node
                .IndentWithoutNewLine(indentation)
                .AppendBlankLine()
                ;

            return output;
        }
    }
}
