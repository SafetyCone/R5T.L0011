using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using IndentationDocumentation = R5T.Y0003.Documentation.ForIndentation;
using Glossary = R5T.Y0003.Glossary;


namespace R5T.L0011.F002
{
    public static class IndentationHelper
    {
        /// <summary>
        /// Converts from <inheritdoc cref="Glossary.ForIndentation.Indentation" path="/name"/> to <inheritdoc cref="Glossary.ForIndentation.LineIndentation" path="/name"/>.
        /// <para><inheritdoc cref="IndentationDocumentation.ConversionToIndentationFromLineIndentation" path="/summary"/></para>
        /// </summary>
        public static SyntaxTriviaList FromLineIndentation(SyntaxTriviaList lineIndentation)
        {
            var output = LineIndentationHelper.ToIndentation(lineIndentation);
            return output;
        }

        public static IEnumerable<SyntaxTrivia> GetTabs_Enumerable(int tabCount)
        {
            var tab = SyntaxTriviaHelper.Tab();

            for (int i = 0; i < tabCount; i++)
            {
                yield return tab;
            }
        }

        public static SyntaxTriviaList GetTabs_SyntaxTriviaList(int tabCount)
        {
            var output = new SyntaxTriviaList(
                IndentationHelper.GetTabs_Enumerable(tabCount));

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetTabs_Enumerable(int)"/> as the default.
        /// </summary>
        public static IEnumerable<SyntaxTrivia> GetTabs(int tabCount)
        {
            return IndentationHelper.GetTabs_Enumerable(tabCount);
        }

        public static SyntaxTriviaList IndentByTab(SyntaxTriviaList indentation)
        {
            var output = indentation.Append(
                SyntaxTriviaHelper.Tab());

            return output;
        }

        /// <summary>
        /// Converts from <inheritdoc cref="Glossary.ForIndentation.Indentation" path="/name"/> to <inheritdoc cref="Glossary.ForIndentation.LineIndentation" path="/name"/>.
        /// <para><inheritdoc cref="IndentationDocumentation.ConversionToLineIndentationFromIndentation" path="/summary"/></para>
        /// </summary>
        public static SyntaxTriviaList ToLineIndentation(SyntaxTriviaList indentation)
        {
            var output = LineIndentationHelper.FromIndentation(indentation);
            return output;
        }
    }
}
