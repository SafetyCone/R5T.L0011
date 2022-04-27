using System;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using R5T.Magyar;

using Glossary = R5T.L0011.X000.Glossary;


namespace System
{
    public static partial class SyntaxTriviaExtensions
    {
        /// <summary>
        /// Text is the value of <see cref="SyntaxTrivia.ToFullString"/>.
        /// </summary>
        public static string GetText(this SyntaxTrivia trivia)
        {
            var output = trivia.ToFullString();
            return output;
        }

        public static WasFound<SyntaxNode> HasStructuredTrivia(this SyntaxTrivia trivia)
        {
            var exists = trivia.HasStructure;

            var result = trivia.GetStructure();

            var output = WasFound.From(exists, result);
            return output;
        }

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


namespace R5T.L0011.X000.N8
{
    public static class SyntaxTriviaExtensions
    {
        public static SyntaxTriviaList ToSyntaxTriviaList(this SyntaxTrivia trivia)
        {
            var output = new SyntaxTriviaList(trivia);
            return output;
        }
    }
}
