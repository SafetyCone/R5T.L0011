using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.L0011.X000;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        public static TNode AddLeadingLeadingTrivia<TNode>(this TNode node,
            IEnumerable<SyntaxTrivia> trivias)
            where TNode : SyntaxNode
        {
            var newLeadingTrivia = node.HasLeadingTrivia
                ? node.GetLeadingTrivia().AddLeadingTrivia(trivias)
                : new SyntaxTriviaList(trivias);

            var output = node.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static TNode AddLeadingLeadingTrivia<TNode>(this TNode node,
            params SyntaxTrivia[] trivias)
            where TNode : SyntaxNode
        {
            var output = node.AddLeadingLeadingTrivia(trivias.AsEnumerable());
            return output;
        }

        public static TNode AddLeadingLeadingTrivia<TNode>(this TNode node,
            SyntaxTriviaList trivia)
            where TNode : SyntaxNode
        {
            var output = node.AddLeadingLeadingTrivia(trivia.AsEnumerable());
            return output;
        }

        public static TNode AddLeadingTrivia<TNode>(this TNode syntaxNode, params SyntaxTrivia[] trivia)
            where TNode : SyntaxNode
        {
            var output = syntaxNode.AddLeadingLeadingTrivia(trivia);
            return output;
        }

        public static TNode AddTrailingLeadingTrivia<TNode>(this TNode node,
            params SyntaxTrivia[] trivia)
            where TNode : SyntaxNode
        {
            var newLeadingTrivia = node.HasLeadingTrivia
                ? node.GetLeadingTrivia().AddTrailingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = node.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static TNode AddTrailingTrailingTrivia<TNode>(this TNode node,
            params SyntaxTrivia[] trivia)
            where TNode : SyntaxNode
        {
            var newTrailingTrivia = node.HasTrailingTrivia
                ? node.GetTrailingTrivia().AddTrailingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = node.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static TNode AddLeadingTrailingTrivia<TNode>(this TNode node,
            params SyntaxTrivia[] trivia)
            where TNode : SyntaxNode
        {
            var newTrailingTrivia = node.HasTrailingTrivia
                ? node.GetTrailingTrivia().AddLeadingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = node.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static TNode AddTrailingTrivia<TNode>(this TNode node,
            params SyntaxTrivia[] trivia)
            where TNode : SyntaxNode
        {
            var output = node.AddTrailingTrailingTrivia(trivia);
            return output;
        }

        public static TNode AppendNewLine<TNode>(this TNode node)
            where TNode : SyntaxNode
        {
            var output = node.AddTrailingTrailingTrivia(SyntaxFactoryHelper.NewLine());
            return output;
        }

        public static SyntaxTrivia[] GetEndOfLineTrivias(this SyntaxNode node)
        {
            var output = node.DescendantTrivia()
                .SelectMany(xTrivia =>
                {
                    var hasStructuredTrivia = xTrivia.HasStructuredTrivia();
                    if(hasStructuredTrivia)
                    {
                        var output = hasStructuredTrivia.Result.GetEndOfLineTrivias_NoStructuredTriviaDescent();
                        return output;
                    }
                    else
                    {
                        return EnumerableHelper.From(xTrivia);
                    }
                })
                .Where(xTrivia => xTrivia.IsEndOfLine())
                .ToArray();

            return output;
        }

        /// <summary>
        /// Gets end of line trivias *without* descending into structured trivia.
        /// This is less useful, since you generally want every new line in a node, even if it is part of structured trivia.
        /// </summary>
        public static SyntaxTrivia[] GetEndOfLineTrivias_NoStructuredTriviaDescent(this SyntaxNode node)
        {
            var output = node.DescendantTrivia()
                .Where(xTrivia => xTrivia.IsEndOfLine())
                .ToArray();

            return output;
        }

        public static SyntaxTriviaList GetSeparatingLeadingTrivia(this SyntaxNode node)
        {
            var firstToken = node.GetFirstToken();

            var output = firstToken.GetSeparatingLeadingTrivia();
            return output;
        }

        public static SyntaxTriviaList GetSeparatingTrailingTrivia(this SyntaxNode node)
        {
            var lastToken = node.GetLastToken();

            var output = lastToken.GetSeparatingTrailingTrivia();
            return output;
        }

        /// <summary>
        /// Determines whether the node has external trivia.
        /// <inheritdoc cref="Glossary.ForTrivia.ExternalTrivia" path="/definition"/>
        /// </summary>
        public static bool HasExternalTrivia(this SyntaxNode node)
        {
            var output = node.HasLeadingTrivia || node.HasTrailingTrivia;
            return output;
        }

        /// <summary>
        /// Removes all trailing trivia from all descendant tokens and prepends it to the leading trivia of the following node.
        /// </summary>
        public static TNode MoveDescendantTrailingTriviaToLeadingTrivia<TNode>(this TNode node)
            where TNode : SyntaxNode
        {
            var descendantTokensWithTrailingTrivia = node.DescendantTokens()
                .Where(xToken => xToken.HasTrailingTrivia)
                // Make sure to evaluate now, so that tokens are not found as they are being annotated.
                .Now();

            node = node.AnnotateTokens(
                descendantTokensWithTrailingTrivia,
                out var annotationsByToken);

            foreach (var tokenAnnotation in annotationsByToken.Values)
            {
                var originalToken = node.GetAnnotatedToken(tokenAnnotation);

                var token = originalToken;

                var originalNextToken = token.GetNextToken();

                var nextToken = originalNextToken;

                if (nextToken.IsNone())
                {
                    // Unable to move trailing trivia of last token to leading trivia of next token, since no next token exists!
                    continue;
                }

                var tokenTrailingTrivia = token.TrailingTrivia;

                token = token.WithoutTrailingTrivia();

                var nextTokenLeadingTrivia = nextToken.LeadingTrivia;

                var newNextTokenLeadingTrivia = nextTokenLeadingTrivia.Prepend(tokenTrailingTrivia);

                nextToken = nextToken.WithLeadingTrivia(newNextTokenLeadingTrivia);

                node = node.ReplaceTokens_Better(new[]
                {
                    (originalToken, token),
                    (originalNextToken, nextToken)
                });
            }

            return node;
        }

        /// <summary>
        /// A blank line is generated by two consecutive new lines.
        /// </summary>
        public static TNode PrependBlankLine<TNode>(this TNode node)
            where TNode : SyntaxNode
        {
            var output = node.AddLeadingLeadingTrivia(
                SyntaxFactoryHelper.NewLine(),
                SyntaxFactoryHelper.NewLine());

            return output;
        }

        public static TNode PrependNewLine<TNode>(this TNode node)
            where TNode : SyntaxNode
        {
            var output = node.AddLeadingLeadingTrivia(SyntaxFactoryHelper.NewLine());
            return output;
        }

        public static TNode PrependNewLineIf<TNode>(this TNode node,
            bool condition)
            where TNode : SyntaxNode
        {
            var output = condition
                ? node.PrependNewLine()
                : node
                ;

            return output;
        }

        public static TNode SetIndentation_Best<TNode>(this TNode node,
            SyntaxTriviaList indentation)
            where TNode : SyntaxNode
        {
            var output = node.WithLeadingTrivia(node.GetLeadingTrivia()
                .SetBeginningBlankTrivia(
                    indentation));

            return output;
        }

        /// <summary>
        /// Ensures the node has not external trivia.
        /// <inheritdoc cref="Glossary.ForTrivia.ExternalTrivia" path="/definition"/>
        /// </summary>
        public static void VerifyHasNoExternalTrivia(this SyntaxNode node)
        {
            var hasExternalTrivia = node.HasExternalTrivia();
            if(hasExternalTrivia)
            {
                throw new Exception("Node had external trivia.");
            }
        }
    }
}