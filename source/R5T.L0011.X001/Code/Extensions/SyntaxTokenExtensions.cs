using System;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class SyntaxTokenExtensions
    {
        public static SyntaxToken AddLeadingLeadingTrivia(this SyntaxToken syntaxToken, params SyntaxTrivia[] trivia)
        {
            var newLeadingTrivia = syntaxToken.HasLeadingTrivia
                ? syntaxToken.LeadingTrivia.InsertRange(0, trivia)
                : new SyntaxTriviaList(trivia);

            var output = syntaxToken.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static SyntaxToken AddTrailingLeadingTrivia(this SyntaxToken syntaxToken, params SyntaxTrivia[] trivia)
        {
            var newLeadingTrivia = syntaxToken.HasLeadingTrivia
                ? syntaxToken.LeadingTrivia.AddRange(trivia)
                : new SyntaxTriviaList(trivia);

            var output = syntaxToken.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static SyntaxToken AddLeadingTrivia(this SyntaxToken syntaxToken, params SyntaxTrivia[] trivia)
        {
            var output = syntaxToken.AddTrailingLeadingTrivia(trivia);
            return output;
        }

        public static SyntaxToken AddLeadingWhitespace(this SyntaxToken syntaxToken,
            SyntaxTriviaList leadingWhitespace)
        {
            var output = syntaxToken.AddLeadingLeadingTrivia(leadingWhitespace.ToArray());
            return output;
        }

        public static SyntaxToken AddTrailingTrailingTrivia(this SyntaxToken syntaxToken, params SyntaxTrivia[] trivia)
        {
            var newTrailingTrivia = syntaxToken.HasTrailingTrivia
                ? syntaxToken.TrailingTrivia.AddRange(trivia)
                : new SyntaxTriviaList(trivia);

            var output = syntaxToken.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static SyntaxToken AddLeadingTrailingTrivia(this SyntaxToken syntaxToken, params SyntaxTrivia[] trivia)
        {
            var newTrailingTrivia = syntaxToken.HasTrailingTrivia
                ? syntaxToken.TrailingTrivia.InsertRange(0, trivia)
                : new SyntaxTriviaList(trivia);

            var output = syntaxToken.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static SyntaxToken AddTrailingTrivia(this SyntaxToken syntaxToken, params SyntaxTrivia[] trivia)
        {
            var output = syntaxToken.AddTrailingTrailingTrivia(trivia);
            return output;
        }

        public static void WriteTo(this SyntaxToken syntaxToken, string filePath)
        {
            using var fileWriter = new StreamWriter(filePath);

            syntaxToken.WriteTo(fileWriter);
        }
    }
}
