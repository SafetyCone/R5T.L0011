using System;

using Microsoft.CodeAnalysis;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        /// <inheritdoc cref="SyntaxTokenExtensions.SetLeadingSeparatingTrivia(SyntaxToken, SyntaxTriviaList)"/>
        public static TNode SetLeadingSeparatingTrivia<TNode>(this TNode node,
            SyntaxTriviaList desiredLeadingSeparatingTrivia)
            where TNode : SyntaxNode
        {
            var firstToken = node.GetFirstToken();

            var newFirstToken = firstToken.SetLeadingSeparatingTrivia(desiredLeadingSeparatingTrivia);

            var output = node.ReplaceToken(firstToken, newFirstToken);
            return output;
        }

        /// <inheritdoc cref="SetLeadingSeparatingTrivia{TNode}(TNode, SyntaxTriviaList)"/>.
        public static TNode SetIndentation2<TNode>(this TNode token,
            SyntaxTriviaList desiredIndentation)
            where TNode : SyntaxNode
        {
            var output = token.SetLeadingSeparatingTrivia(desiredIndentation);
            return output;
        }
    }
}
