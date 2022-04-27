using System;
using System.Linq;

using Microsoft.CodeAnalysis;

using SyntaxTriviaHelper = Microsoft.CodeAnalysis.CSharp.SyntaxTriviaHelper;

using IndentationDocumentation = R5T.Y0003.Documentation.ForIndentation;
using Glossary = R5T.Y0003.Glossary;


namespace R5T.L0011.F002
{
    public static class LineIndentationHelper
    {
        /// <summary>
        /// Converts from <inheritdoc cref="Glossary.ForIndentation.Indentation" path="/name"/> to <inheritdoc cref="Glossary.ForIndentation.LineIndentation" path="/name"/>.
        /// <para><inheritdoc cref="IndentationDocumentation.ConversionToLineIndentationFromIndentation" path="/summary"/></para>
        /// </summary>
        public static SyntaxTriviaList FromIndentation(SyntaxTriviaList indentation)
        {
            var output = indentation.Prepend(
                SyntaxTriviaHelper.NewLine());

            return output;
        }

        public static SyntaxTriviaList IndentByTab(SyntaxTriviaList indentation)
        {
            var output = IndentationHelper.IndentByTab(indentation);
            return output;
        }

        /// <summary>
        /// Converts from <inheritdoc cref="Glossary.ForIndentation.LineIndentation" path="/name"/> to <inheritdoc cref="Glossary.ForIndentation.Indentation" path="/name"/>.
        /// <para><inheritdoc cref="IndentationDocumentation.ConversionToIndentationFromLineIndentation" path="/summary"/></para>
        /// </summary>
        public static SyntaxTriviaList ToIndentation(SyntaxTriviaList lineIndentation)
        {
            var newLine = SyntaxTriviaHelper.NewLine();

            var indexOfLastNewLine = lineIndentation.LastIndexOf(newLine);

            var startIndex = IndexHelper.IsFound(indexOfLastNewLine)
                ? indexOfLastNewLine + 1
                : 0
                ;

            var output = lineIndentation.FromIndex(startIndex)
                .ToSyntaxTriviaList();

            return output;
        }
    }
}
