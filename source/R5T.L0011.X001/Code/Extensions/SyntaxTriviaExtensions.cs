using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Instances = R5T.L0011.X001.Instances;


namespace System
{
    public static class SyntaxTriviaExtensions
    {
        public static bool IsEndOfLine(this SyntaxTrivia syntaxTrivia)
        {
            var output = syntaxTrivia.IsKind(SyntaxKind.EndOfLineTrivia);
            return output;
        }

        public static bool IsInLeadingTrivia(this SyntaxTrivia syntaxTrivia)
        {
            var output = syntaxTrivia.Token.LeadingTrivia.Contains(syntaxTrivia);
            return output;
        }

        public static bool IsInTrailingTrivia(this SyntaxTrivia syntaxTrivia)
        {
            var output = syntaxTrivia.Token.TrailingTrivia.Contains(syntaxTrivia);
            return output;
        }

        public static bool IsNewLine(this SyntaxTrivia syntaxTrivia)
        {
            var output = syntaxTrivia.IsEndOfLine();
            return output;
        }

        /// <summary>
        /// <broader-concept>Conceptually, there might be more kinds of "whitespace" trivia than just the <see cref="SyntaxKind.WhitespaceTrivia"/> kind.</broader-concept>
        /// This method allows testing for the broader concept of "whitespace".
        /// For the <inheritdoc cref="IsWhitespaceSyntaxKind(SyntaxTrivia)" path="/summary/specific-test"/> see <see cref="IsWhitespaceSyntaxKind(SyntaxTrivia)"/>.
        /// </summary>
        public static bool IsWhitespace(this SyntaxTrivia syntaxTrivia)
        {
            var output = syntaxTrivia.IsEndOfLine() && syntaxTrivia.IsWhitespaceSyntaxKind();
            return output;
        }

        /// <inheritdoc cref="IsWhitespace(SyntaxTrivia)"/>
        public static bool IsNonWhitespace(this SyntaxTrivia syntaxTrivia)
        {
            var output = !syntaxTrivia.IsWhitespace();
            return output;
        }

        /// <summary>
        /// Implements the <specific-test>specific test of whether the trivia is of <see cref="SyntaxKind.WhitespaceTrivia"/> kind</specific-test>.
        /// <inheritdoc cref="IsWhitespace(SyntaxTrivia)" path="/summary/broader-concept"/>
        /// To test for the broader concept, see <see cref="IsWhitespace(SyntaxTrivia)"/>.
        /// </summary>
        public static bool IsWhitespaceSyntaxKind(this SyntaxTrivia syntaxTrivia)
        {
            var output = syntaxTrivia.IsKind(SyntaxKind.WhitespaceTrivia);
            return output;
        }

        public static void WriteTo(this SyntaxTrivia syntaxTrivia, string filePath)
        {
            using var fileWriter = new StreamWriter(filePath);

            syntaxTrivia.WriteTo(fileWriter);
        }
    }
}


namespace System.Linq
{
    public static class SyntaxTriviaExtensions
    {
        public static SyntaxTriviaList ToSyntaxTriviaList(this IEnumerable<SyntaxTrivia> trivias)
        {
            var output = new SyntaxTriviaList(trivias);
            return output;
        }
    }
}