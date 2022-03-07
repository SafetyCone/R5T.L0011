using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using R5T.Magyar;

using Glossary = R5T.L0011.X000.Glossary;


namespace System
{
    public static class SyntaxTokenExtensions
    {
        public static bool BeginsWithNewLine(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.LeadingTrivia.BeginsWithNewLine();
            return output;
        }

        public static bool EndsWithNewLine(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.TrailingTrivia.EndsWithNewLine();
            return output;
        }

        public static bool IsIdentifierToken(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.IdentifierToken);
            return output;
        }

        public static bool IsNextToken(this SyntaxToken syntaxToken,
            SyntaxToken nextToken,
            bool includeDocumentationComments = false)
        {
            var actualNextToken = syntaxToken.GetNextToken(
                includeDocumentationComments);

            var output = nextToken == actualNextToken;
            return output;
        }

        public static bool IsPreviousToken(this SyntaxToken syntaxToken,
            SyntaxToken previousToken,
            bool includeDocumentationComments = false)
        {
            var actualPreviousToken = syntaxToken.GetPreviousToken(
                includeDocumentationComments);

            var output = previousToken == actualPreviousToken;
            return output;
        }

        public static bool IsStatic(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.StaticKeyword);
            return output;
        }

        public static bool IsText(this SyntaxToken syntaxToken,
            string text)
        {
            var output = syntaxToken.Text == text;
            return output;
        }

        /// <summary>
        /// Modifies whitespace on first and second nodes such that the separating whitespace of the indentation is leading trivia on the second token.
        /// </summary>
        public static (SyntaxToken modifiedFirst, SyntaxToken modifiedSecond) SetSeparatingWhitespace_Leading(this SyntaxToken first,
            SyntaxToken second,
            SyntaxTriviaList indentation)
        {
            // Get the current separating trivia.
            var separatingTrivia = first.GetTrailingSeparatingTrivia_Unchecked(second);

            var trimmedSeparatingTrivia = separatingTrivia.TrimWhitespaceStart();

            var leadingTrivia = new SyntaxTriviaList(indentation.Concat(trimmedSeparatingTrivia));

            // Erase trailing trivia from the first token.
            var modifiedFirst = first.WithTrailingTrivia();

            // Use the leading trivia for the second.
            var modifiedSecond = second.WithLeadingTrivia(leadingTrivia);

            return (modifiedFirst, modifiedSecond);
        }

        /// <summary>
        /// Modifies whitespace on first and second nodes such that the separating whitespace of the indentation is leading trivia on the second token.
        /// </summary>
        public static (SyntaxToken modifiedFirst, SyntaxToken modifiedSecond) SetSeparatingTrivia_Leading(this SyntaxToken first,
            SyntaxToken second,
            SyntaxTriviaList separatingTrivia)
        {
            // Erase trailing trivia from the first token.
            var modifiedFirst = first.WithTrailingTrivia();

            // Use the leading trivia for the second.
            var modifiedSecond = second.WithLeadingTrivia(separatingTrivia);

            return (modifiedFirst, modifiedSecond);
        }

        /// <summary>
        /// Chooses <see cref="SetSeparatingWhitespace_Leading(SyntaxToken, SyntaxToken, SyntaxTriviaList)"/> as the default.
        /// </summary>
        public static (SyntaxToken modifiedFirst, SyntaxToken modifiedSecond) SetSeparatingWhitespace(this SyntaxToken first,
            SyntaxToken second,
            SyntaxTriviaList indentation)
        {
            var output = first.SetSeparatingWhitespace_Leading(
                second,
                indentation);

            return output;
        }

        public static SyntaxToken SetIndentation(this SyntaxToken syntaxToken,
            SyntaxTriviaList indentation)
        {
            var output = syntaxToken.WithLeadingTrivia(indentation.ToArray());
            return output;
        }

        /// <summary>
        /// For use in the obnoxious cases where an open brace or something already includes a new line.
        /// </summary>
        public static SyntaxToken SetIndentationWithoutNewLine(this SyntaxToken syntaxToken,
            SyntaxTriviaList indentation)
        {
            var actualIndentation = indentation.RemoveAt(0);

            var output = syntaxToken.WithLeadingTrivia(actualIndentation.ToArray());
            return output;
        }

        public static SyntaxToken Indent(this SyntaxToken syntaxToken,
            SyntaxTriviaList indentation)
        {
            var output = syntaxToken.AddLeadingLeadingTrivia(indentation.ToArray());
            return output;
        }

        /// <summary>
        /// For use in the obnoxious cases where an open brace or something already includes a new line.
        /// </summary>
        public static SyntaxToken IndentWithoutNewLine(this SyntaxToken syntaxToken,
            SyntaxTriviaList indentation)
        {
            var actualIndentation = indentation.RemoveAt(0);

            var output = syntaxToken.AddLeadingLeadingTrivia(actualIndentation.ToArray());
            return output;
        }

        

        public static SyntaxToken AddLeadingWhitespace(this SyntaxToken syntaxToken,
            SyntaxTriviaList leadingWhitespace)
        {
            var output = syntaxToken.AddLeadingLeadingTrivia(leadingWhitespace.ToArray());
            return output;
        }

        

        public static WasFound<SyntaxNode> GetParent(this SyntaxToken syntaxToken)
        {
            var output = WasFound.From(syntaxToken.Parent);
            return output;
        }

        public static WasFound<SyntaxNodeOrToken> GetPriorSiblingNodeOrToken(this SyntaxToken syntaxToken)
        {
            var parentWasFound = syntaxToken.GetParent();
            if(parentWasFound)
            {
                var parent = parentWasFound.Result;

                var childNodesAndTokens = parent.ChildNodesAndTokens();

                var indexOfSyntaxToken = childNodesAndTokens.IndexOfChildInNodesAndTokens(syntaxToken);
                if(indexOfSyntaxToken == 0)
                {
                    return WasFound.NotFound<SyntaxNodeOrToken>();
                }
                else
                {
                    var priorSibling = childNodesAndTokens[indexOfSyntaxToken - 1];

                    return WasFound.From(priorSibling);
                }
            }
            else
            {
                return WasFound.NotFound<SyntaxNodeOrToken>();
            }
        }

        /// <summary>
        /// Gets the separating trivia ahead of the token (between the token and previous token).
        /// <inheritdoc cref="Glossary.ForTrivia.SeparatingTrivia"/>
        /// </summary>
        private static SyntaxTriviaList GetLeadingSeparatingTrivia_Unchecked(this SyntaxToken syntaxToken,
            SyntaxToken previousToken)
        {
            var separatingTrivia = new SyntaxTriviaList(previousToken.TrailingTrivia.Concat(syntaxToken.LeadingTrivia));
            return separatingTrivia;
        }

        /// <summary>
        /// Gets the separating trivia ahead of the token (between the token and previous token).
        /// <inheritdoc cref="Glossary.ForTrivia.SeparatingTrivia"/>
        /// </summary>
        public static SyntaxTriviaList GetLeadingSeparatingTrivia(this SyntaxToken syntaxToken,
            SyntaxToken previousToken,
            bool includeDocumentationComments = false)
        {
            syntaxToken.VerifyIsPreviousToken(previousToken, includeDocumentationComments);

            // Now that we know these two tokens are contiguous, get any leading and trailing syntax.
            var output = syntaxToken.GetLeadingSeparatingTrivia_Unchecked(previousToken);
            return output;
        }

        /// <summary>
        /// Gets the separating trivia ahead of the token (between the token and previous token).
        /// <inheritdoc cref="Glossary.ForTrivia.SeparatingTrivia"/>
        /// </summary>
        public static SyntaxTriviaList GetLeadingSeparatingTrivia(this SyntaxToken syntaxToken,
           bool includeDocumentationComments = false)
        {
            var previousToken = syntaxToken.GetPreviousToken();

            var output = syntaxToken.GetLeadingSeparatingTrivia(
                previousToken,
                includeDocumentationComments);
            return output;
        }

        /// <summary>
        /// Gets the separating trivia after the token (between the token and next token).
        /// <inheritdoc cref="Glossary.ForTrivia.SeparatingTrivia"/>
        /// </summary>
        private static SyntaxTriviaList GetTrailingSeparatingTrivia_Unchecked(this SyntaxToken syntaxToken,
            SyntaxToken nextToken)
        {
            var trailingTrivia = syntaxToken.HasTrailingTrivia
                ? syntaxToken.TrailingTrivia
                : new SyntaxTriviaList()
                ;

            var leadingTrivia = nextToken.HasLeadingTrivia
                ? nextToken.LeadingTrivia
                : new SyntaxTriviaList()
                ;

            var separatingTrivia = new SyntaxTriviaList(trailingTrivia.Concat(leadingTrivia));
            return separatingTrivia;
        }

        public static SyntaxTriviaList GetNonWhitespaceLeadingTrivia(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.HasNonWhitespaceLeadingTrivia().Result;
            return output;
        }

        public static WasFound<SyntaxTriviaList> HasNonWhitespaceLeadingTrivia(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetLeadingTrivia().HasNonWhitespaceTrivia();
            return output;
        }

        /// <summary>
        /// Gets the separating trivia after the token (between the token and next token).
        /// <inheritdoc cref="Glossary.ForTrivia.SeparatingTrivia"/>
        /// </summary>
        public static SyntaxTriviaList GetTrailingSeparatingTrivia(this SyntaxToken syntaxToken,
            SyntaxToken nextToken,
            bool includeDocumentationComments = false)
        {
            syntaxToken.VerifyIsNextToken(nextToken, includeDocumentationComments);

            // Now that we know these two tokens are contiguous, get any leading and trailing syntax.
            var output = syntaxToken.GetTrailingSeparatingTrivia_Unchecked(nextToken);
            return output;
        }

        /// <summary>
        /// Gets the separating trivia ahead of the token (between the token and previous token).
        /// <inheritdoc cref="Glossary.ForTrivia.SeparatingTrivia"/>
        /// </summary>
        public static SyntaxTriviaList GetTrailingSeparatingTrivia(this SyntaxToken syntaxToken,
           bool includeDocumentationComments = false)
        {
            var nextToken = syntaxToken.GetNextToken();

            var output = syntaxToken.GetTrailingSeparatingTrivia(
                nextToken,
                includeDocumentationComments);
            return output;
        }

        public static WasFound<SyntaxNodeOrToken> GetNextSiblingNodeOrToken(this SyntaxToken syntaxToken)
        {
            var parentWasFound = syntaxToken.GetParent();
            if (parentWasFound)
            {
                var parent = parentWasFound.Result;

                var childNodesAndTokens = parent.ChildNodesAndTokens();

                var indexOfSyntaxToken = childNodesAndTokens.IndexOfChildInNodesAndTokens(syntaxToken);
                if (indexOfSyntaxToken == childNodesAndTokens.LastIndex())
                {
                    return WasFound.NotFound<SyntaxNodeOrToken>();
                }
                else
                {
                    var nextSibling = childNodesAndTokens[indexOfSyntaxToken + 1];

                    return WasFound.From(nextSibling);
                }
            }
            else
            {
                return WasFound.NotFound<SyntaxNodeOrToken>();
            }
        }

        public static void VerifyIsNextToken(this SyntaxToken syntaxToken,
            SyntaxToken nextToken,
            bool includeDocumentationComments = false)
        {
            var isNextToken = syntaxToken.IsNextToken(nextToken, includeDocumentationComments);
            if(!isNextToken)
            {
                throw new Exception("Verification of token as next token failed.");
            }
        }

        public static void VerifyIsPreviousToken(this SyntaxToken syntaxToken,
            SyntaxToken previousToken,
            bool includeDocumentationComments = false)
        {
            var isNextToken = syntaxToken.IsPreviousToken(previousToken, includeDocumentationComments);
            if (!isNextToken)
            {
                throw new Exception("Verification of token as previous token failed.");
            }
        }

        public static SyntaxToken WithoutTrailingTrivia(this SyntaxToken token)
        {
            var output = token.WithTrailingTrivia();
            return output;
        }

        public static void WriteTo(this SyntaxToken syntaxToken, string filePath)
        {
            using var fileWriter = new StreamWriter(filePath);

            syntaxToken.WriteTo(fileWriter);
        }
    }
}
