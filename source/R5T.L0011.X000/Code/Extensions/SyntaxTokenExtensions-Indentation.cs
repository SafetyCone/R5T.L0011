using System;
using System.Linq;

using Microsoft.CodeAnalysis;

using Glossary = R5T.L0011.X000.Glossary;


namespace System
{
    public static partial class SyntaxTokenExtensions
    {
        /// <summary>
        /// Adds indentation before the token.
        /// Note: there is no IndentBlock() for a token, since a token can only exist on a single line.
        /// <inheritdoc cref="Glossary.ForTrivia.StartLine" path="/definition"/>
        /// If there is a new line in the leading trivia of the token, indentation is added after the last new line. Else it is added to the start of the leading trivia.
        /// </summary>
        public static SyntaxToken IndentStartLine(this SyntaxToken token,
            SyntaxTriviaList indentation)
        {
            // A token always has leading trivia, is just might be empty.
            var leadingTrivia = token.LeadingTrivia;

            var indexOfLastNewLine = leadingTrivia.LastIndexWhere(
                x => x.IsNewLine());

            if(IndexHelper.IsFound(indexOfLastNewLine))
            {
                var newLeadingTrivia = leadingTrivia
                    // Include the new line.
                    .Take(indexOfLastNewLine + 1)
                    // Insert the indentation.
                    .Concat(indentation)
                    // Take the rest.
                    .Concat(
                        leadingTrivia.Skip(indexOfLastNewLine + 1))
                    .ToSyntaxTriviaList();

                var output = token.WithLeadingTrivia(newLeadingTrivia);
                return output;
            }
            else
            {
                var output = token.AddLeadingLeadingTrivia(indentation.ToArray());
                return output;
            }
        }
    }
}
