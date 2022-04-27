using System;

using Microsoft.CodeAnalysis;

using Glossary = R5T.L0011.X000.Glossary;


namespace Microsoft.CodeAnalysis.CSharp
{
    public static class SyntaxTriviaHelper
    {
        /// <summary>
        /// No check is preformed to ensure that the syntax tokens are consecutive.
        /// </summary>
        public static SyntaxTriviaList GetSeparatingTrivia_NoCheck(
            SyntaxToken firstToken,
            SyntaxToken secondToken)
        {
            var firstTokenTrailingTrivia = firstToken.TrailingTrivia;
            var secondTokenLeadingTrivia = secondToken.LeadingTrivia;

            var output = firstTokenTrailingTrivia.Append(secondTokenLeadingTrivia);
            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForTrivia.BlankTrivia" path="/definition"/>
        /// </summary>
        public static bool IsBlank(SyntaxTrivia syntaxTrivia)
        {
            var output = SyntaxTriviaHelper.IsWhitespace(syntaxTrivia) || SyntaxTriviaHelper.IsNewLine(syntaxTrivia);
            return output;
        }

        public static bool IsNewLine(SyntaxTrivia syntaxTrivia)
        {
            var output = syntaxTrivia.IsKind(SyntaxKind.EndOfLineTrivia);
            return output;
        }

        public static bool IsWhitespace(SyntaxTrivia syntaxTrivia)
        {
            var output = syntaxTrivia.IsKind(SyntaxKind.WhitespaceTrivia);
            return output;
        }

        public static SyntaxTrivia NewLine(string newLineText)
        {
            var output = SyntaxFactoryHelper.NewLine(newLineText);
            return output;
        }

        public static SyntaxTrivia NewLine()
        {
            var output = SyntaxFactoryHelper.NewLine();
            return output;
        }

        public static SyntaxTrivia Tab()
        {
            var output = SyntaxFactoryHelper.Tab();
            return output;
        }
    }
}
