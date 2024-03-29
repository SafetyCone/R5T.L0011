﻿using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using R5T.Magyar;

using Documentation = R5T.L0011.X001.Documentation;


namespace System
{
    public static class SyntaxTriviaListExtensions
    {
        /// <summary>
        /// If the trivia is empty, returns a new line. Else, returns the trivia.
        /// </summary>
        public static SyntaxTriviaList EnsureAtLeastNewLine(this SyntaxTriviaList trivia)
        {
            var isEmpty = trivia.IsEmpty();
            if(isEmpty)
            {
                var output = SyntaxTriviaList.Create(SyntaxTriviaHelper.NewLine());
                return output;
            }

            return trivia;
        }

        /// <summary>
        /// Determines if line separator trivia was found.
        /// <inheritdoc cref="Documentation.LineSeparatorTrivia" path="/definition"/>
        /// </summary>
        public static WasFound<SyntaxTriviaList> HasLineSeparatorTrivia(this SyntaxTriviaList trivia)
        {
            // Start at the first token, with a not-found index value.
            // When a new line token is encountered, and if the index value is not-found, set the index value. Else, continue to ensure we get the index of the first new line token.
            // If non-whitespace trivia is found, reset the index to not-found.
            // Get all trivia from the index onwards.

            var indexOfLineSeparatorNewLine = IndexHelper.NotFound;
            for (int iTriviaIndex = 0; iTriviaIndex < trivia.Count; iTriviaIndex++)
            {
                var currentTrivia = trivia[iTriviaIndex];
                if(currentTrivia.IsNewLine() && IndexHelper.IsNotFound(indexOfLineSeparatorNewLine))
                {
                    indexOfLineSeparatorNewLine = iTriviaIndex;

                    continue;
                }

                if(currentTrivia.IsNonWhitespace())
                {
                    indexOfLineSeparatorNewLine = IndexHelper.NotFound;

                    continue;
                }

                // If not a new line or is whitespace, continue.
            }

            var hasLineSeparatorNewLine = IndexHelper.IsFound(indexOfLineSeparatorNewLine);

            var lineSeparatorTrivia = hasLineSeparatorNewLine
                ? Linq.SyntaxTriviaEnumerableExtensions.ToSyntaxTriviaList(trivia.FromIndex(indexOfLineSeparatorNewLine)) // Explicit spefication of extension method due to duplicate in Microsft.CodeAnalysis.CSharp.
                : default
                ;

            var output = WasFound.From(lineSeparatorTrivia);
            return output;
        }

        public static WasFound<SyntaxTriviaList> HasNonWhitespaceTrivia(this SyntaxTriviaList trivia)
        {
            var staticWhitespaceTrimmedTrivia = trivia.TrimWhitespaceStart();

            var exists = staticWhitespaceTrimmedTrivia.Any();

            var output = WasFound.From(exists, staticWhitespaceTrimmedTrivia);
            return output;
        }

        public static bool StartsWith(this SyntaxTriviaList trivia,
            SyntaxTriviaList startingTrivia)
        {
            var triviaStartingTrivia = trivia.Take(startingTrivia.Count);

            var sequenceEqual = startingTrivia.SequenceEqual(triviaStartingTrivia, SyntaxTriviaTextComparer.Instance);

            return sequenceEqual;
        }

        public static SyntaxTriviaList TrimWhitespaceStart(this SyntaxTriviaList trivia)
        {
            var nonWhitespaceEncountered = false;

            var startWhitespaceTrimmedTrivias = trivia
                .Where(xTrivia =>
                {
                    // Keep everything after non-whitespace is encountered.
                    if (nonWhitespaceEncountered)
                    {
                        return true;
                    }

                    var isNonWhitespace = xTrivia.IsNonWhitespace();

                    if (isNonWhitespace)
                    {
                        nonWhitespaceEncountered = true;
                    }

                    return isNonWhitespace;
                })
                ;

            var output = new SyntaxTriviaList(startWhitespaceTrimmedTrivias);
            return output;
        }
    }
}
