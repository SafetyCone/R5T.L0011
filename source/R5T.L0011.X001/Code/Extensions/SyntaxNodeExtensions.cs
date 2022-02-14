using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using Instances = R5T.L0011.X001.Instances;


namespace System
{
    public static class SyntaxNodeExtensions
    {
        public static SyntaxList<TNode> ToSyntaxList<TNode>(this IEnumerable<TNode> nodes)
            where TNode : SyntaxNode
        {
            var output = new SyntaxList<TNode>(nodes);
            return output;
        }

        public static bool BeginsWithNewLine(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetLeadingTrivia().BeginsWithNewLine();
            return output;
        }

        public static bool ContainsToken(this SyntaxNode syntaxNode,
            SyntaxToken syntaxToken)
        {
            var nodeSpan = syntaxNode.FullSpan;
            var tokenSpan = syntaxToken.FullSpan;

            var nodeContainsToken = nodeSpan.Start <= tokenSpan.Start && nodeSpan.End >= tokenSpan.End;
            return nodeContainsToken;
        }

        public static bool EndsWithNewLine(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetTrailingTrivia().EndsWithNewLine();
            return output;
        }

        public static IEnumerable<TDescendantSyntaxNodeType> GetChildrenOfType<TDescendantSyntaxNodeType>(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.ChildNodes()
                .OfType<TDescendantSyntaxNodeType>()
                ;

            return output;
        }

        public static SyntaxToken GetChildSemicolonToken<TNode>(this TNode node)
            where TNode : SyntaxNode
        {
            var output = node.ChildTokens()
                .Where(token => token.IsKind(SyntaxKind.SemicolonToken))
                .Single();

            return output;
        }

        public static SyntaxToken GetDescendantSemicolonToken<TNode>(this TNode node)
            where TNode : SyntaxNode
        {
            var output = node.DescendantTokens()
                .Where(token => token.IsKind(SyntaxKind.SemicolonToken))
                .Single();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetDescendantSemicolonToken{TNode}(TNode)"/> as the default.
        /// </summary>
        public static SyntaxToken GetSemicolonToken<TNode>(this TNode node)
            where TNode : SyntaxNode
        {
            var output = node.GetDescendantSemicolonToken();
            return output;
        }

        public static TChild GetChildAsType_SingleOrDefault<TChild>(this SyntaxNode syntaxNode)
            where TChild : class
        {
            var output = syntaxNode.ChildNodes().SingleOrDefault() as TChild;
            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetChildAsType_SingleOrDefault{TChild}(SyntaxNode)"/> as the default.
        /// </summary>
        public static TChild GetChildAsType<TChild>(this SyntaxNode syntaxNode)
            where TChild : class
        {
            var output = syntaxNode.GetChildAsType_SingleOrDefault<TChild>();
            return output;
        }

        public static TNode SetIndentation<TNode>(this TNode syntaxNode,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var output = syntaxNode.WithLeadingTrivia(indentation.ToArray());
            return output;
        }

        public static TNode SetIndentationPreservingNonWhitespaceTrivia<TNode>(this TNode syntaxNode,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var nonWhitespaceLeadingTrivia = syntaxNode.GetNonWhitespaceLeadingTrivia();

            var leadingTrivia = indentation.AppendRange(nonWhitespaceLeadingTrivia);

            var output = syntaxNode.WithLeadingTrivia(leadingTrivia.ToArray());
            return output;
        }

        public static TNode SetIndentationWithoutLeadingNewLine<TNode>(this TNode syntaxNode,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var actualIndentation = indentation.RemoveAt(0);

            var output = syntaxNode.WithLeadingTrivia(actualIndentation.ToArray());
            return output;
        }

        public static SyntaxNode SetLeadingIndentationOfDescendent(this SyntaxNode parentSyntaxNode,
            SyntaxNode childSyntaxNode,
            SyntaxTriviaList indentation,
            bool includeDocumentationComments = false)
        {
            var childFirstSyntaxToken = childSyntaxNode.GetFirstToken(includeDocumentationComments: includeDocumentationComments);

            var output = parentSyntaxNode.SetLeadingIndentationOfDescendent(
                childFirstSyntaxToken,
                indentation,
                includeDocumentationComments);

            return output;
        }

        /// <summary>
        /// Returns the parent node, with the leading trivia of the child set to the input indentation.
        /// </summary>
        public static SyntaxNode SetLeadingIndentationOfDescendent(this SyntaxNode syntaxNode,
            SyntaxToken childSyntaxToken,
            SyntaxTriviaList indentation,
            bool includeDocumentationComments = false)
        {
            var previousSyntaxToken = childSyntaxToken.GetPreviousToken(includeDocumentationComments: includeDocumentationComments);

            // Verify the previousl syntax token is in this parent node (required for proper return of modification).
            syntaxNode.VerifyContainsToken(previousSyntaxToken);

            var leadingSeparatingTrivia = childSyntaxToken.GetLeadingSeparatingTrivia(previousSyntaxToken, includeDocumentationComments);

            var changesRequired = leadingSeparatingTrivia != indentation;
            if(changesRequired)
            {
                var output = syntaxNode.SetSeparatingWhitespaceBetweenDescendentTokens(previousSyntaxToken, childSyntaxToken, indentation);
                return output;
            }
            else
            {
                // No changes required, just return the parent node.
                return syntaxNode;
            }
        }

        public static TNode SetSeparatingWhitespaceBetweenDescendentTokens_Leading<TNode>(this TNode syntaxNode,
            SyntaxToken firstDescendent,
            SyntaxToken secondDescendent,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            syntaxNode.VerifyContainsToken(firstDescendent);
            syntaxNode.VerifyContainsToken(secondDescendent);

            var (modifiedFirst, modifiedSecond) = firstDescendent.SetSeparatingWhitespace_Leading(
                secondDescendent,
                indentation);

            var outputSyntaxNode = syntaxNode.ReplaceTokens(
                EnumerableHelper.From(firstDescendent, secondDescendent),
                (originalToken, _) =>
                {
                    if (originalToken == firstDescendent)
                    {
                        return modifiedFirst;
                    }

                    if (originalToken == secondDescendent)
                    {
                        return modifiedSecond;
                    }

                    throw new Exception();
                });

            return outputSyntaxNode;
        }

        /// <summary>
        /// Chooses <see cref="SetSeparatingWhitespaceBetweenDescendentTokens_Leading(SyntaxNode, SyntaxToken, SyntaxToken, SyntaxTriviaList)"/> as the default.
        /// </summary>
        public static TNode SetSeparatingWhitespaceBetweenDescendentTokens<TNode>(this TNode syntaxNode,
            SyntaxToken firstDescendent,
            SyntaxToken secondDescendent,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var output = syntaxNode.SetSeparatingWhitespaceBetweenDescendentTokens_Leading(firstDescendent, secondDescendent, indentation);
            return output;
        }

        public static TNode SetSeparatingWhitespaceBetweenDescendents<TNode>(this TNode syntaxNode,
            SyntaxToken firstDescendent,
            SyntaxNode secondDescendent,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var secondDescendentToken = secondDescendent.GetFirstToken(); // Get the first token of the second descendent.

            var output = syntaxNode.SetSeparatingWhitespaceBetweenDescendentTokens_Leading(firstDescendent, secondDescendentToken, indentation);
            return output;
        }

        public static TNode SetSeparatingWhitespaceBetweenDescendents<TNode>(this TNode syntaxNode,
            SyntaxNode firstDescendent,
            SyntaxToken secondDescendent,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var firstDescendentToken = firstDescendent.GetLastToken(); // Get the last token of the first descendent.

            var output = syntaxNode.SetSeparatingWhitespaceBetweenDescendentTokens(firstDescendentToken, secondDescendent, indentation);
            return output;
        }

        public static bool StartsWithNewLine(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetLeadingTrivia().StartsWithNewLine();
            return output;
        }

        public static IEnumerable<TNode> Indent<TNode>(this IEnumerable<TNode> syntaxNodes,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var output = syntaxNodes
                .Select(xSyntaxNode => xSyntaxNode.Indent(indentation))
                ;

            return output;
        }

        public static TNode Indent<TNode>(this TNode syntaxNode,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var output = syntaxNode.AddLeadingLeadingTrivia(indentation.ToArray());
            return output;
        }

        public static TNode IndentWithoutNewLine<TNode>(this TNode syntaxNode,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var actualIndentation = indentation.RemoveAt(0);

            var output = syntaxNode.AddLeadingLeadingTrivia(actualIndentation.ToArray());
            return output;
        }

        public static IEnumerable<TNode> AddLeadingWhitespace<TNode>(this IEnumerable<TNode> syntaxNodes,
            SyntaxTriviaList leadingWhitespace)
            where TNode : SyntaxNode
        {
            var output = syntaxNodes
                .Select(syntaxNode =>
                {
                    var syntaxNodeOutput = syntaxNode.AddLeadingWhitespace(leadingWhitespace);
                    return syntaxNodeOutput;
                });

            return output;
        }

        public static TNode AddLeadingWhitespace<TNode>(this TNode syntaxNode,
            SyntaxTriviaList leadingWhitespace)
            where TNode : SyntaxNode
        {
            var output = syntaxNode.AddLeadingLeadingTrivia(leadingWhitespace.ToArray());
            return output;
        }

        public static IEnumerable<TDescendantSyntaxNodeType> GetDescendantsOfType<TNode, TDescendantSyntaxNodeType>(this TNode syntaxNode)
            where TNode : SyntaxNode
        {
            var output = syntaxNode.DescendantNodes()
                .OfType<TDescendantSyntaxNodeType>()
                ;

            return output;
        }

        public static IEnumerable<InterfaceDeclarationSyntax> GetInterfaces<TNode>(this TNode syntaxNode)
            where TNode : SyntaxNode
        {
            var output = syntaxNode.DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>()
                ;

            return output;
        }

        public static WasFound<SyntaxNode> GetParent(this SyntaxNode syntaxNode)
        {
            var output = WasFound.From(syntaxNode.Parent);
            return output;
        }

        private static void GetParentsInsideToOutside_Internal(this SyntaxNode syntaxNode, List<SyntaxNode> parentAccumulator)
        {
            if(syntaxNode.HasParent())
            {
                parentAccumulator.Add(syntaxNode.Parent);

                syntaxNode.Parent.GetParentsInsideToOutside_Internal(parentAccumulator);
            }
            // Else, return.
        }

        public static IEnumerable<SyntaxNode> GetParentsInsideToOutside(this SyntaxNode syntaxNode)
        {
            var parentAccumulator = new List<SyntaxNode>();

            syntaxNode.GetParentsInsideToOutside_Internal(parentAccumulator);

            return parentAccumulator;
        }

        public static IEnumerable<SyntaxNode> GetParentsOutsideToInside(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetParentsInsideToOutside()
                .Reverse()
                ;

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetParentsInsideToOutside(SyntaxNode)"/> as the default.
        /// </summary>
        public static SyntaxNode[] GetParents(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetParentsInsideToOutside().Now();
            return output;
        }

        public static WasFound<SyntaxNodeOrToken> GetPriorSiblingNodeOrToken(this SyntaxNode syntaxNode)
        {
            var parentWasFound = syntaxNode.GetParent();
            if (parentWasFound)
            {
                var parent = parentWasFound.Result;

                var childNodesAndTokens = parent.ChildNodesAndTokens();

                var indexOfSyntaxToken = childNodesAndTokens.IndexOfChildInNodesAndTokens(syntaxNode);
                if (indexOfSyntaxToken == 0)
                {
                    return WasFound.NotFound<SyntaxNodeOrToken>();
                }
                else
                {
                    var priorSibling = childNodesAndTokens[indexOfSyntaxToken - 1];

                    return WasFound.From(priorSibling);
                }
            }
            else
            {
                return WasFound.NotFound<SyntaxNodeOrToken>();
            }
        }

        public static WasFound<SyntaxNodeOrToken> GetNextSiblingNodeOrToken(this SyntaxNode syntaxNode)
        {
            var parentWasFound = syntaxNode.GetParent();
            if (parentWasFound)
            {
                var parent = parentWasFound.Result;

                var childNodesAndTokens = parent.ChildNodesAndTokens();

                var indexOfSyntaxToken = childNodesAndTokens.IndexOfChildInNodesAndTokens(syntaxNode);
                if (indexOfSyntaxToken == childNodesAndTokens.LastIndex())
                {
                    return WasFound.NotFound<SyntaxNodeOrToken>();
                }
                else
                {
                    var nextSibling = childNodesAndTokens[indexOfSyntaxToken + 1];

                    return WasFound.From(nextSibling);
                }
            }
            else
            {
                return WasFound.NotFound<SyntaxNodeOrToken>();
            }
        }

        public static TNode ModifyWith<TNode>(this TNode syntaxNode, ModifierSynchronous<TNode> modifier)
            where TNode : SyntaxNode
        {
            var output = modifier is object
                ? modifier(syntaxNode)
                : syntaxNode;

            return output;
        }

        public static WasFound<int> IndexOfChildInNodesAndTokens(this SyntaxNode syntaxNode, SyntaxNode childSyntaxNode)
        {
            var childNodesAndTokens = syntaxNode.ChildNodesAndTokens();

            var output = childNodesAndTokens.IndexOfChildInNodesAndTokens(childSyntaxNode);
            return output;
        }

        public static WasFound<int> IndexOfChildInNodesAndTokens(this SyntaxNode syntaxNode, SyntaxToken childSyntaxToken)
        {
            var childNodesAndTokens = syntaxNode.ChildNodesAndTokens();

            var output = childNodesAndTokens.IndexOfChildInNodesAndTokens(childSyntaxToken);
            return output;
        }

        public static TNode ModifyIf<TNode>(this TNode node,
            bool condition,
            Func<TNode, TNode> modifier)
        {
            var output = condition
                ? modifier(node)
                : node
                ;

            return output;
        }

        public static TNode ModifyWith<TNode, TData>(this TNode syntaxNode, ModifierSynchronousWith<TNode, TData> modifier, TData data)
            where TNode : SyntaxNode
        {
            var output = modifier is object
                ? modifier(syntaxNode, data)
                : syntaxNode;

            return output;
        }

        public static async Task<TNode> ModifyWith<TNode>(this TNode syntaxNode,
            Func<TNode, Task<TNode>> action = default)
            where TNode : SyntaxNode
        {
            var output = action is object
                ? await action(syntaxNode)
                : syntaxNode;

            return output;
        }

        public static TNode ModifyWith<TNode>(this TNode syntaxNode, SyntaxTriviaList lineLeadingWhitespace,
            ModifierWithIndentationSynchronous<TNode> modifier)
            where TNode : SyntaxNode
        {
            var output = modifier is object
                ? modifier(syntaxNode, lineLeadingWhitespace)
                : syntaxNode;

            return output;
        }

        public static TNode PrependLeadingWhitespace<TNode>(this TNode syntaxNode,
            SyntaxAnnotation annotation,
            SyntaxTriviaList leadingWhitespace)
            where TNode : SyntaxNode
        {
            var output = syntaxNode.ReplaceNodesAndTokens(
                theSyntaxNode => theSyntaxNode.GetAnnotatedNodesAndTokens(annotation),
                node => node.AddLeadingWhitespace(leadingWhitespace),
                token => token.AddLeadingWhitespace(leadingWhitespace));

            return output;
        }

        public static TNode ReplaceNodeSynchronous<TNode, TChildSyntaxNode>(this TNode syntaxNode,
            Func<TNode, TChildSyntaxNode> childNodeSelector,
            Func<TChildSyntaxNode, TChildSyntaxNode> childNodeModifier)
            where TNode : SyntaxNode
            where TChildSyntaxNode : SyntaxNode
        {
            var childNode = childNodeSelector(syntaxNode);

            var modifiedChildNode = childNodeModifier(childNode);

            var output = syntaxNode.ReplaceNode(childNode, modifiedChildNode);
            return output;
        }

        public static TNode ReplaceNodes<TNode>(this TNode syntaxNode,
            Func<TNode, Dictionary<SyntaxNode, SyntaxNode>> replacementsGenerator)
            where TNode : SyntaxNode
        {
            var replacements = replacementsGenerator(syntaxNode);

            var output = syntaxNode.ReplaceNodes(replacements.Keys, (original, modified) =>
            {
                var replacement = replacements[original];
                return replacement;
            });

            return output;
        }

        public static TNode ReplaceNodesAndTokens<TNode>(this TNode syntaxNode,
            Func<TNode, IEnumerable<SyntaxNode>> syntaxNodeSelector,
            Func<TNode, IEnumerable<SyntaxToken>> syntaxTokenSelector,
            Func<SyntaxNode, SyntaxNode> nodeTransformer,
            Func<SyntaxToken, SyntaxToken> tokenTransformer)
            where TNode : SyntaxNode
        {
            var modifiedSyntaxNode = syntaxNode;

            var nodes = syntaxNodeSelector(modifiedSyntaxNode);

            modifiedSyntaxNode = modifiedSyntaxNode.ReplaceNodes(nodes, (originalNode, node) =>
            {
                return nodeTransformer(node);
            });

            var tokens = syntaxTokenSelector(modifiedSyntaxNode);

            return modifiedSyntaxNode.ReplaceTokens(tokens, (originalToken, token) =>
            {
                return tokenTransformer(token);
            });
        }

        public static TNode ReplaceNodesAndTokens<TNode>(this TNode syntaxNode,
            Func<TNode, IEnumerable<SyntaxNodeOrToken>> selector,
            Func<SyntaxNode, SyntaxNode> nodeTransformer,
            Func<SyntaxToken, SyntaxToken> tokenTransformer)
            where TNode : SyntaxNode
        {
            IEnumerable<SyntaxNode> GetNodes(TNode compilationUnit)
            {
                return selector(compilationUnit)
                    .Where(x => x.IsNode)
                    .Select(x => x.AsNode());
            }

            IEnumerable<SyntaxToken> GetTokens(TNode compilationUnit)
            {
                return selector(compilationUnit)
                    .Where(x => x.IsToken)
                    .Select(x => x.AsToken());
            }

            return syntaxNode.ReplaceNodesAndTokens(GetNodes, GetTokens, nodeTransformer, tokenTransformer);
        }

        public static void VerifyNonNull(this SyntaxNode syntaxNode, string message)
        {
            if(syntaxNode is null)
            {
                throw new ArgumentNullException(message);
            }
        }

        public static void VerifyContainsToken(this SyntaxNode syntaxNode,
            SyntaxToken syntaxToken)
        {
            var nodeContainsToken = syntaxNode.ContainsToken(syntaxToken);
            if(!nodeContainsToken)
            {
                throw new Exception("Syntax node does not contain syntax token.");
            }
        }

        public static TNode WithIndentedSemicolon<TNode>(this TNode node,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var oldSemicolonToken = node.GetSemicolonToken();

            var newSemicolonToken = oldSemicolonToken.Indent(indentation);

            var output = node.ReplaceToken(oldSemicolonToken, newSemicolonToken);
            return output;
        }

        public static TNode WithSemicolonIndentation<TNode>(this TNode node,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var oldSemicolonToken = node.GetSemicolonToken();

            var newSemicolonToken = oldSemicolonToken.WithLeadingTrivia(indentation);

            var output = node.ReplaceToken(oldSemicolonToken, newSemicolonToken);
            return output;
        }

        public static async Task WriteTo(this SyntaxNode syntaxNode, string filePath)
        {
            var text = syntaxNode.ToFullString();

            using var fileWriter = new StreamWriter(filePath);

            await fileWriter.WriteAsync(text);
        }

        public static void WriteToSynchronous(this SyntaxNode syntaxNode, string filePath)
        {
            using var fileWriter = new StreamWriter(filePath);

            syntaxNode.WriteTo(fileWriter);
        }
    }
}
