﻿using System;

using Microsoft.CodeAnalysis;

using R5T.L0011.F002;

using SyntaxTriviaHelper = Microsoft.CodeAnalysis.CSharp.SyntaxTriviaHelper;


namespace R5T.L0011.Z002
{
    public static class LineIndentations
    {
        public static SyntaxTriviaList ByTabCount(int tabCount)
        {
            var output = LineIndentationHelper.FromIndentation(
                Indentations.ByTabCount(tabCount));

            return output;
        }

        public static SyntaxTriviaList NewLine()
        {
            var output = new SyntaxTriviaList(
                SyntaxTriviaHelper.NewLine());

            return output;
        }

        public static SyntaxTriviaList BlankLine()
        {
            var newLine = SyntaxTriviaHelper.NewLine();

            // Two blank lines is generated by two new line characters.
            var output = new SyntaxTriviaList(
                newLine,
                newLine);

            return output;
        }

        public static SyntaxTriviaList TwoBlankLines()
        {
            var newLine = SyntaxTriviaHelper.NewLine();

            // Two blank lines is generated by three new line characters.
            var output = new SyntaxTriviaList(
                newLine,
                newLine,
                newLine);

            return output;
        }
    }
}
