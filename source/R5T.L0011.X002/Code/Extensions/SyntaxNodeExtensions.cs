﻿using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.L0011.T004;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class SyntaxNodeExtensions
    {
        public static T Annotate<T>(this T syntaxNode, out SyntaxAnnotation annotation)
            where T : SyntaxNode
        {
            annotation = Instances.SyntaxFactory.Annotation();

            var output = syntaxNode.WithAdditionalAnnotations(annotation);
            return output;
        }

        public static (T, TChild, SyntaxAnnotation) AnnotateChild<T, TChild>(this T syntaxNode,
            TChild childSyntaxNode)
            where T : SyntaxNode
            where TChild : SyntaxNode
        {
            var annotation = Instances.SyntaxFactory.Annotation();

            var outputChildSyntaxNode = childSyntaxNode.WithAdditionalAnnotations(annotation);

            var outputSyntaxNode = syntaxNode.ReplaceNode(childSyntaxNode, outputChildSyntaxNode);

            return (outputSyntaxNode, outputChildSyntaxNode, annotation);
        }

        public static TSyntaxNode AddLineStart<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddLeadingLeadingTrivia(leadingWhitespace.ToArray());
            return output;
        }

        public static TSyntaxNode AddLineStart2<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddLeadingLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static TSyntaxNode AppendBlankLine<TSyntaxNode>(this TSyntaxNode syntaxNode)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddTrailingTrailingTrivia(Instances.SyntaxFactory.NewLine());
            return output;
        }

        public static TSyntaxNode AppendBlankLine<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddTrailingTrailingTrivia(indentation.ToArray());
            return output;
        }

        public static TSyntaxNode AppendBlankLine2<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddTrailingTrailingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static TSyntaxNode InsertLineStart<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddTrailingLeadingTrivia(indentation.ToArray());
            return output;
        }

        public static TSyntaxNode InsertLineStart2<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddTrailingLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static TSyntaxNode PrependBlankLine<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList leadingWhitespace,
            bool actuallyPrependTheLine = true)
            where TSyntaxNode : SyntaxNode
        {
            if(!actuallyPrependTheLine)
            {
                return syntaxNode;
            }

            var output = syntaxNode.AddLeadingLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static TSyntaxNode PrependBlankLine<TSyntaxNode>(this TSyntaxNode syntaxNode)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.PrependBlankLine(new SyntaxTriviaList());
            return output;
        }

        public static TSyntaxNode ModifyWith<TSyntaxNode>(this TSyntaxNode syntaxNode, SyntaxNodeModifier<TSyntaxNode> modifier,
            SyntaxTriviaList indentation,
            INamespaceNameSet namespaceNames)
            where TSyntaxNode : SyntaxNode
        {
            var output = modifier is object
                ? modifier(syntaxNode, indentation, namespaceNames)
                : syntaxNode;

            return output;
        }

        public static TSyntaxNode WrapWithRegion<TSyntaxNode>(this TSyntaxNode syntaxNode,
            string regionName,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var region = Instances.SyntaxFactory.Region(indentation, regionName);

            var endRegion = Instances.SyntaxFactory.EndRegion(indentation);

            var output = syntaxNode
                .AddLeadingLeadingTrivia(region)
                .AddTrailingTrailingTrivia(endRegion)
                ;

            return output;
        }

        public static TSyntaxNode RemoveLeadingBlankLine<TSyntaxNode>(this TSyntaxNode syntaxNode)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.WithLeadingTrivia(syntaxNode.GetLeadingTrivia().RemoveLeadingNewLine());
            return output;
        }

        public static TSyntaxNode IndentBlock<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var tabination = indentation.GetTabination();

            // Get all descendant end-of-line trivias in the block.
            var endOfLineTrivias = syntaxNode.DescendantTrivia()
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
            var output = syntaxNode.ReplaceTokens(parentTokens,
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

            return output;

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
        public static TSyntaxNode IndentForInsertion<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode
                .IndentWithoutNewLine(indentation)
                .AppendBlankLine()
                ;

            return output;
        }
    }
}
