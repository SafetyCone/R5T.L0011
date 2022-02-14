using System;

using Microsoft.CodeAnalysis;


namespace R5T.L0011.X000
{
    public static class SyntaxTriviaHelper
    {
        /// <summary>
        /// No check is preformed to ensure that the syntax tokens are consecutive.
        /// </summary>
        public static SyntaxTriviaList GetSeparatingTrivia_NoCheck(
            SyntaxToken firstToken,
            SyntaxToken secondToken)
        {
            var firstTokenTrailingTrivia = firstToken.TrailingTrivia;
            var secondTokenLeadingTrivia = secondToken.LeadingTrivia;

            var output = firstTokenTrailingTrivia.Append(secondTokenLeadingTrivia);
            return output;
        }
    }
}
