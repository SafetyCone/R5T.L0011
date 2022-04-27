using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using R5T.L0011.X000;

using Glossary = R5T.L0011.X000.Glossary;


namespace System
{
    public static partial class SyntaxTokenExtensions
    {
        public static SyntaxToken AddLeadingLeadingTrivia(this SyntaxToken token,
            IEnumerable<SyntaxTrivia> trivias)
        {
            var newLeadingTrivia = token.HasLeadingTrivia
                ? token.LeadingTrivia.AddLeadingTrivia(trivias)
                : new SyntaxTriviaList(trivias);

            var output = token.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static SyntaxToken AddLeadingLeadingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivias)
        {
            var output = token.AddLeadingLeadingTrivia(trivias.AsEnumerable());
            return output;
        }

        public static SyntaxToken AddLeadingLeadingTrivia(this SyntaxToken token,
            SyntaxTriviaList trivia)
        {
            var output = token.AddLeadingLeadingTrivia(trivia.AsEnumerable());
            return output;
        }

        public static SyntaxToken AddTrailingLeadingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivia)
        {
            var newLeadingTrivia = token.HasLeadingTrivia
                ? token.LeadingTrivia.AddTrailingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = token.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static SyntaxToken AddLeadingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivia)
        {
            var output = token.AddLeadingLeadingTrivia(trivia);
            return output;
        }

        public static SyntaxToken AddTrailingTrailingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivia)
        {
            var newTrailingTrivia = token.HasTrailingTrivia
                ? token.TrailingTrivia.AddTrailingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = token.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static SyntaxToken AddLeadingTrailingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivia)
        {
            var newTrailingTrivia = token.HasTrailingTrivia
                ? token.TrailingTrivia.AddLeadingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = token.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static SyntaxToken AddTrailingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivia)
        {
            var output = token.AddTrailingTrailingTrivia(trivia);
            return output;
        }

        public static SyntaxToken AppendNewLine(this SyntaxToken token)
        {
            var output = token.AddTrailingTrailingTrivia(SyntaxFactoryHelper.NewLine());
            return output;
        }

        public static SyntaxTriviaList GetSeparatingLeadingTrivia(this SyntaxToken token)
        {
            var previousToken = token.GetPreviousToken();

            var output = SyntaxTriviaHelper.GetSeparatingTrivia_NoCheck(
                previousToken,
                token);

            return output;
        }

        /// <summary>
        /// Get the beginning blank trivia of the leading trivia of a token.
        /// <inheritdoc cref="Glossary.ForTrivia.BlankTrivia" path="/definition"/>
        /// </summary>
        public static SyntaxTriviaList GetSeparatingBeginningBlankLeadingTrivia(this SyntaxToken token)
        {
            var separatingLeadingTrivia = token.GetSeparatingLeadingTrivia();

            var beginningBlankTrivia = separatingLeadingTrivia.GetBeginningBlankTrivia();
            return beginningBlankTrivia;
        }

        public static SyntaxTriviaList GetSeparatingTrailingTrivia(this SyntaxToken token)
        {
            var nextToken = token.GetNextToken();

            var output = SyntaxTriviaHelper.GetSeparatingTrivia_NoCheck(
                token,
                nextToken);

            return output;
        }

        public static SyntaxToken PrependNewLine(this SyntaxToken token)
        {
            var output = token.AddLeadingLeadingTrivia(SyntaxFactoryHelper.NewLine());
            return output;
        }

        public static SyntaxToken RemoveFromLeadingLeadingTrivia_Checked(this SyntaxToken token,
            IReadOnlyList<SyntaxTrivia> trivias)
        {
            var triviasCount = trivias.Count;

            var leadingTrivia = token.LeadingTrivia;

            var beginningSequenceEqual = leadingTrivia.Take(triviasCount).SequenceEqual(trivias, SyntaxTriviaTextEqualityComparer.Instance);
            if(!beginningSequenceEqual)
            {
                throw new Exception("The two sequences did not start with the same elements.");
            }

            var output = token.RemoveFromLeadingLeadingTrivia_Unchecked(trivias);
            return output;
        }

        public static SyntaxToken RemoveFromLeadingLeadingTrivia_Unchecked(this SyntaxToken token,
            IReadOnlyList<SyntaxTrivia> trivias)
        {
            var triviasCount = trivias.Count;

            var leadingTrivia = token.LeadingTrivia;

            var newLeadingTrivia = leadingTrivia.Skip(triviasCount);

            var output = token.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="RemoveLeadingLeadingTrivia_Checked(SyntaxToken, IEnumerable{SyntaxTrivia})"/> as the default.
        /// </summary>
        public static SyntaxToken RemoveFromLeadingLeadingTrivia(this SyntaxToken token,
            IReadOnlyList<SyntaxTrivia> trivias)
        {
            var output = token.RemoveFromLeadingLeadingTrivia_Checked(trivias);
            return output;
        }

        public static SyntaxToken RemoveTrailingEndingBlankTrivia(this SyntaxToken token)
        {
            var newTrailingTrivia = token.TrailingTrivia.RemoveEndingBlankTrivia();

            var output = token.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static SyntaxToken RemoveLeadingBeginningBlankTrivia(this SyntaxToken token)
        {
            var newLeadingTrivia = token.LeadingTrivia.RemoveBeginningBlankTrivia();

            var output = token.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static SyntaxToken WithoutTrailingTrivia(this SyntaxToken token)
        {
            var output = token.WithTrailingTrivia();
            return output;
        }
    }
}
