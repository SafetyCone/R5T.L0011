using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Glossary = R5T.L0011.X000.Glossary;


namespace System
{
    public static class SyntaxTriviaExtensions
    {
        /// <summary>
        /// <inheritdoc cref="Glossary.BlankTrivia" path="/definition"/>
        /// </summary>
        public static bool IsBlank(this SyntaxTrivia trivia)
        {
            var output = trivia.IsEndOfLine() || trivia.IsWhitespaceSyntaxKind();
            return output;
        }

        public static bool IsEndOfLine(this SyntaxTrivia trivia)
        {
            var output = trivia.IsKind(SyntaxKind.EndOfLineTrivia);
            return output;
        }

        public static bool IsInLeadingTrivia(this SyntaxTrivia trivia)
        {
            var output = trivia.Token.LeadingTrivia.Contains(trivia);
            return output;
        }

        public static bool IsInTrailingTrivia(this SyntaxTrivia trivia)
        {
            var output = trivia.Token.TrailingTrivia.Contains(trivia);
            return output;
        }

        public static bool IsNewLine(this SyntaxTrivia trivia)
        {
            var output = trivia.IsEndOfLine();
            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.BlankTrivia" path="/definition"/>
        /// </summary>
        public static bool IsNonBlank(this SyntaxTrivia trivia)
        {
            var isBlank = trivia.IsBlank();

            var output = !isBlank;
            return output;
        }

        /// <inheritdoc cref="IsWhitespace(SyntaxTrivia)"/>
        public static bool IsNonWhitespace(this SyntaxTrivia trivia)
        {
            var output = !trivia.IsWhitespace();
            return output;
        }

        /// <summary>
        /// <broader-concept>Conceptually, there might be more kinds of "whitespace" trivia than just the <see cref="SyntaxKind.WhitespaceTrivia"/> kind.</broader-concept>
        /// This method allows testing for the broader concept of "whitespace".
        /// For the <inheritdoc cref="IsWhitespaceSyntaxKind(SyntaxTrivia)" path="/summary/specific-test"/> see <see cref="IsWhitespaceSyntaxKind(SyntaxTrivia)"/>.
        /// </summary>
        public static bool IsWhitespace(this SyntaxTrivia trivia)
        {
            var output = trivia.IsEndOfLine() || trivia.IsWhitespaceSyntaxKind();
            return output;
        }

        /// <summary>
        /// Implements the <specific-test>specific test of whether the trivia is of <see cref="SyntaxKind.WhitespaceTrivia"/> kind</specific-test>.
        /// <inheritdoc cref="IsWhitespace(SyntaxTrivia)" path="/summary/broader-concept"/>
        /// To test for the broader concept, see <see cref="IsWhitespace(SyntaxTrivia)"/>.
        /// </summary>
        public static bool IsWhitespaceSyntaxKind(this SyntaxTrivia trivia)
        {
            var output = trivia.IsKind(SyntaxKind.WhitespaceTrivia);
            return output;
        }

        public static void WriteTo(this SyntaxTrivia trivia,
            string filePath)
        {
            using var fileWriter = new StreamWriter(filePath);

            trivia.WriteTo(fileWriter);
        }
    }
}
