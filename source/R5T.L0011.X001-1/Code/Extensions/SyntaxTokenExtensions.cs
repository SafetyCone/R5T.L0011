using System;
using System.Linq;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class SyntaxTokenExtensions
    {
        /// <summary>
        /// Sets the leading separating trivia of the token:
        /// * Accounts for the trailing blank trivia of the prior token to achieve the desired leading trivia indentation.
        /// * Throws an exception if the indentation cannot be achieved because prior token has too much trailing trivia (this is a problem since we cannot modify two nodes at a time). If the compilation unit is standardized, this will not be a problem since all trivia will be leading trivia and there will be now trailing trivia.
        /// * Uses leading trivia indentation (as is standard).
        /// </summary>
        public static SyntaxToken SetLeadingSeparatingTrivia(this SyntaxToken token,
            SyntaxTriviaList desiredLeadingSeparatingTrivia)
        {
            var outputToken = token;

            var existingPreviousTokenTrailingEndingBlankTrivia = outputToken.GetPreviousTokenTrailingEndingBlankTrivia();

            // Does the trailing ending blank trivia of the previous token exceed the desired leading separating trivia?
            var desiredTriviaIsExceeded = existingPreviousTokenTrailingEndingBlankTrivia.Exceeds(desiredLeadingSeparatingTrivia);
            if(desiredTriviaIsExceeded)
            {
                throw new Exception("Unable to set desired leading separating trivia: the existing trailing ending blank trivia of the previous token exceeds the desired leading separating trivia.\nEither standardize the compilation unit so that there is no trailing trivia, or use another method to modify both the token and its previous token at the same time.");
            }

            // Remove the existing leading separating trivia (leading beginning blank trivia) from the token.
            // Get the existing leading beginning blank trivia of the token.
            var existingLeadingBeginningBlankTrivia = outputToken.GetLeadingBeginningBlankTrivia();

            // Remove it.
            outputToken = outputToken.RemoveFromLeadingLeadingTrivia(existingLeadingBeginningBlankTrivia);

            // Get the appendix of the desired leading separating trivia over the existing trailing separating trivia of the previous node.
            // This is the trivia to prepend to the token's leading trivia to get the desired leading separating trivia.
            var appendixOfDesiredSeparatingTrivia = desiredLeadingSeparatingTrivia.GetTrailingAppendix(existingPreviousTokenTrailingEndingBlankTrivia);

            outputToken = outputToken.AddLeadingLeadingTrivia(appendixOfDesiredSeparatingTrivia);

            return outputToken;
        }

        /// <inheritdoc cref="SetLeadingSeparatingTrivia(SyntaxToken, SyntaxTriviaList)"/>.
        public static SyntaxToken SetIndentation2(this SyntaxToken token,
            SyntaxTriviaList desiredIndentation)
        {
            var output = token.SetLeadingSeparatingTrivia(desiredIndentation);
            return output;
        }
    }
}
