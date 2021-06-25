using System;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.L0011.T001;


namespace System
{
    public static class SyntaxTokenExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static SyntaxToken AppendComment(this SyntaxToken syntaxToken, string text)
        {
            var comment = SyntaxFactory.Comment(text);

            var output = syntaxToken.AddTrailingTrivia(comment);
            return output;
        }

        public static SyntaxToken AppendNewLineLeadingWhitespace(this SyntaxToken syntaxToken, SyntaxTriviaList leadingWhitespace)
        {
            var output = syntaxToken.AddTrailingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }
    }
}
