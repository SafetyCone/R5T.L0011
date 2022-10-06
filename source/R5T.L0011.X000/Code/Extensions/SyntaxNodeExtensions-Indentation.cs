using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;

using Glossary = R5T.L0011.X000.Glossary;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        /// <summary>
        /// Adds indentation to the start of each line in the node. (Not just the first line of the node, see <see cref="IndentStartLine{TNode}(TNode, SyntaxTriviaList)"/>.)
        /// </summary>
        public static TSyntaxNode IndentBlock<TSyntaxNode>(this TSyntaxNode node,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            // Get end-of-line trivias.
            var endOfLineTrivias = node.GetEndOfLineTrivias();

            // Foreach end-of-line trivia, get its next token (the first token on the next line), and unless its token type is None(), accumulate the token.
            var firstTokensOfLines = endOfLineTrivias
                .Select(xTrivia =>
                {
                    var isInLeadingTrivia = xTrivia.IsInLeadingTrivia();
                    if(isInLeadingTrivia)
                    {
                        return xTrivia.Token;
                    }
                    else
                    {
                        return xTrivia.Token.GetNextToken();
                    }
                })
                .Where(xToken => xToken.IsNotNone())
                // Add the first token to the list, since we will want to indent that too.
                .Append(node.GetFirstToken_HandleDocumentationComments())
                .ToArray();

            // Annotate tokens.
            node = node.AnnotateTokens(
                firstTokensOfLines,
                out var annotationsByFirstTokensOfLines);

            // Now indent each token by annotation.
            foreach (var annotation in annotationsByFirstTokensOfLines.Values)
            {
                node = annotation.ModifyToken(
                    node,
                    xToken => xToken.IndentStartLine(indentation));
            }

            return node;
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

        /// <summary>
        /// Adds indentation to the start line of the node. (Not every line of the node, see <see cref="IndentBlock{TSyntaxNode}(TSyntaxNode, SyntaxTriviaList)"/>.)
        /// <inheritdoc cref="Glossary.ForTrivia.StartLine" path="/definition"/>
        /// </summary>
        public static TNode IndentStartLine<TNode>(this TNode node,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            // Get the first token, then indent the start line of the first token.
            var firstToken = node.GetFirstToken_HandleDocumentationComments();

            var indentedFirstToken = firstToken.IndentStartLine(indentation);

            var output = node.ReplaceToken_Better(firstToken, indentedFirstToken);
            return output;
        }

        /// <summary>
        /// Adds indentation to the start line of the node. (Not every line of the node, see <see cref="IndentBlock{TSyntaxNode}(IEnumerable{TSyntaxNode}, SyntaxTriviaList)"/>.)
        /// <inheritdoc cref="Glossary.ForTrivia.StartLine" path="/definition"/>
        /// 
        /// </summary>
        public static IEnumerable<TNode> IndentStartLine<TNode>(this IEnumerable<TNode> nodes,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var output = nodes
                .Select(xSyntaxNode => xSyntaxNode.IndentStartLine(indentation))
                ;

            return output;
        }

        public static TNode SetLineIdentation_ForNodeAnnotation<TNode>(this TNode node,
            SyntaxAnnotation descendantNodeAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var output = node.SetLeadingSeparatingSpacing_ForNodeAnnotation(
                descendantNodeAnnotation,
                leadingSeparatingSpacing);

            return output;
        }

        public static TNode SetLeadingSeparatingSpacing_ForNodeAnnotation<TNode>(this TNode node,
            SyntaxAnnotation descendantNodeAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            // Get the node of interest.
            var descendantNode = node.GetAnnotatedNode(descendantNodeAnnotation);

            var output = node.SetLeadingSeparatingSpacing(
                descendantNode,
                leadingSeparatingSpacing);

            return output;
        }

        public static TNode SetLeadingSeparatingSpacing<TNode>(this TNode node,
            SyntaxNode descendantNode,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            // Get the first token of the node.
            var firstToken = descendantNode.GetFirstToken_HandleDocumentationComments();

            var output = node.SetLeadingSeparatingSpacing(
                firstToken,
                leadingSeparatingSpacing);

            return output;
        }

        /// <summary>
        /// Sets the line indentation for the first line of the annotated syntax node.
        /// </summary>
        public static TNode SetLineIndentation<TNode>(this TNode node,
            SyntaxToken token,
            SyntaxTriviaList lineIndentation)
            where TNode : SyntaxNode
        {
            var output = node.SetLeadingSeparatingSpacing(
                token,
                lineIndentation);

            return output;
        }

        public static TNode SetLineIndentation_ForTokenAnnotation<TNode>(this TNode node,
            SyntaxAnnotation descendentTokenAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var output = node.SetLeadingSeparatingSpacing_ForTokenAnnotation(
                descendentTokenAnnotation,
                leadingSeparatingSpacing);

            return output;
        }

        public static TNode SetLeadingSeparatingSpacing_ForTokenAnnotation<TNode>(this TNode node,
            SyntaxAnnotation descendentTokenAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            // Get the previous token.
            var token = node.GetAnnotatedToken(descendentTokenAnnotation);

            var output = node.SetLeadingSeparatingSpacing(
                token,
                leadingSeparatingSpacing);

            return output;
        }

        public static TNode SetLeadingSeparatingSpacing<TNode>(this TNode node,
            SyntaxToken token,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            // Get the previous token.
            var previousToken = token.GetPreviousToken();

            // If the previous token is none, then we just set the leading trivia.
            if (previousToken.IsNone())
            {
                var output = node.SetLeadingSeparatingSpacing_NoPreviousNode(
                    token,
                    leadingSeparatingSpacing);

                return output;
            }
            else
            {
                var output = node.SetLeadingSeparatingSpacing(
                    token,
                    previousToken,
                    leadingSeparatingSpacing);

                return output;
            }
        }

        public static TNode SetLeadingSeparatingSpacing<TNode>(this TNode node,
            SyntaxToken token,
            SyntaxToken previousToken,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var initialToken = token;
            var initialPreviousToken = previousToken;

            // Remove all trailing ending blank trivia from the previous token.
            previousToken = previousToken.RemoveTrailingEndingBlankTrivia();

            // Remove all leading beginning blank trivia from the token.
            token = token.RemoveLeadingBeginningBlankTrivia();

            // Prepend the desire leading separating spacing to the leading trivia of the token.
            token = token.AddLeadingLeadingTrivia(leadingSeparatingSpacing);

            // Replace both tokens within the compilation unit.
            node = node.ReplaceTokens_Better(new[]
            {
                (initialToken, token),
                (initialPreviousToken, previousToken)
            });

            // Return the compilation unit.
            return node;
        }

        public static TNode SetLeadingSeparatingSpacing_NoPreviousNode<TNode>(this TNode node,
            SyntaxToken token,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var initialToken = token;

            // Remove all leading beginning blank trivia from the token.
            token = token.RemoveLeadingBeginningBlankTrivia();

            // Prepend the desire leading separating spacing to the leading trivia of the token.
            token = token.AddLeadingLeadingTrivia(leadingSeparatingSpacing);

            node = node.ReplaceToken(
                initialToken,
                token);

            return node;
        }

        public static TNode SetLineIndentation_ForTriviaAnnotation<TNode>(this TNode node,
            SyntaxAnnotation descendentTriviaAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var output = node.SetLeadingSeparatingSpacing_ForTriviaAnnotation(
                descendentTriviaAnnotation,
                leadingSeparatingSpacing);

            return output;
        }

        public static TNode SetLeadingSeparatingSpacing_ForTriviaAnnotation<TNode>(this TNode node,
            SyntaxAnnotation descendentTriviaAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var trivia = node.GetAnnotatedTriviaSingle(descendentTriviaAnnotation);

            var output = node.SetLeadingSeparatingSpacing(
                trivia,
                leadingSeparatingSpacing);

            return output;
        }

        public static TNode SetLeadingSeparatingSpacing<TNode>(this TNode node,
            SyntaxTrivia trivia,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var triviaToken = trivia.Token;

            var output = node.SetLeadingSeparatingSpacing(
                triviaToken,
                leadingSeparatingSpacing);

            return output;
        }
    }
}


namespace N8
{
    public static class SyntaxNodeExtensions
    {
        /// <summary>
        /// CAUTION: node must contain the open and close braces as descendants.
        /// This is available here on the <see cref="SyntaxNode"/>-level for use in extensions to all the different node types that have interior open and close braces (e.g. MethodDeclaration, ClassDeclaration, etc.).
        /// </summary>
        public static TNode SetDescendantBracesLineIndentation<TNode>(this TNode node,
            SyntaxToken openBraceToken,
            SyntaxToken closeBraceToken,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var braceTokens = new[]
            {
                openBraceToken,
                closeBraceToken,
            };

            node = node.AnnotateTokens(
                braceTokens,
                out var annotationsByBraceToken);

            foreach (var braceAnnotation in annotationsByBraceToken.Values)
            {
                node = node.SetLineIndentation_ForTokenAnnotation(
                    braceAnnotation,
                    indentation);
            }

            return node;
        }
    }
}
