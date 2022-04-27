using System;

using Microsoft.CodeAnalysis;

using R5T.T0126;


namespace System
{
    public static class SyntaxNodeExtensions
    {
        /// <summary>
        /// Sets the line indentation for the first line of the annotated syntax node.
        /// </summary>
        public static TNode SetLineIndentation<TNode>(this TNode node,
            ISyntaxNodeAnnotation descendantNodeAnnotation,
            SyntaxTriviaList lineIndentation)
            where TNode : SyntaxNode
        {
            var output = node.SetLeadingSeparatingSpacing(
                descendantNodeAnnotation,
                lineIndentation);

            return output;
        }

        public static TNode SetLeadingSeparatingSpacing<TNode>(this TNode node,
            ISyntaxNodeAnnotation descendantNodeAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var output = node.SetLeadingSeparatingSpacing_ForNodeAnnotation(
                descendantNodeAnnotation.SyntaxAnnotation,
                leadingSeparatingSpacing);

            return output;
        }

        public static TNode SetLineIndentation<TNode>(this TNode node,
            ISyntaxTokenAnnotation tokenAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var output = node.SetLeadingSeparatingSpacing(
                tokenAnnotation,
                leadingSeparatingSpacing);

            return output;
        }

        public static TNode SetLeadingSeparatingSpacing<TNode>(this TNode node,
            ISyntaxTokenAnnotation tokenAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var output = node.SetLeadingSeparatingSpacing_ForTokenAnnotation(
                tokenAnnotation.SyntaxAnnotation,
                leadingSeparatingSpacing);

            return output;
        }

        public static TNode SetLineIndentation<TNode>(this TNode node,
            ISyntaxTriviaAnnotation triviaAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var output = node.SetLeadingSeparatingSpacing(
                triviaAnnotation,
                leadingSeparatingSpacing);

            return output;
        }

        public static TNode SetLeadingSeparatingSpacing<TNode>(this TNode node,
            ISyntaxTriviaAnnotation triviaAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
            where TNode : SyntaxNode
        {
            var output = node.SetLineIndentation_ForTriviaAnnotation(
                triviaAnnotation.SyntaxAnnotation,
                leadingSeparatingSpacing);

            return output;
        }
    }
}