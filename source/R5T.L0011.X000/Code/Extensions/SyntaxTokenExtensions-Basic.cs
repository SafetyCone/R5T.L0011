using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static partial class SyntaxTokenExtensions
    {
        /// <summary>
        /// Calls <see cref="GetLeadingBeginningBlankTrivia(SyntaxToken)"/>.
        /// </summary>
        public static SyntaxTriviaList GetLineIndentation(this SyntaxToken token)
        {
            var output = token.GetLeadingBeginningBlankTrivia();
            return output;
        }

        public static SyntaxTriviaList GetLeadingBeginningBlankTrivia(this SyntaxToken token)
        {
            var output = token.LeadingTrivia.GetBeginningBlankTrivia();
            return output;
        }

        public static SyntaxTriviaList GetPreviousTokenTrailingEndingBlankTrivia(this SyntaxToken token)
        {
            var previousToken = token.GetPreviousToken();

            var output = previousToken.TrailingTrivia.GetEndingBlankTrivia();
            return output;
        }

        public static SyntaxTriviaList GetPreviousTokenTrailingTrivia(this SyntaxToken token)
        {
            var previousToken = token.GetPreviousToken();

            var output = previousToken.TrailingTrivia;
            return output;
        }

        /// <summary>
        /// Gets the actual text of the token.
        /// </summary>
        public static string GetText(this SyntaxToken token)
        {
            var output = token.ValueText;
            return output;
        }

        public static bool IsFirstTokenInCompilationUnit(this SyntaxToken token)
        {
            var previousToken = token.GetPreviousToken();

            var output = previousToken.IsNone();
            return output;
        }

        public static bool IsNone(this SyntaxToken token)
        {
            var output = token.IsKind(SyntaxKind.None);
            return output;
        }

        public static bool IsNotNone(this SyntaxToken token)
        {
            var isNone = token.IsNone();

            var output = !isNone;
            return output;
        }
    }
}
