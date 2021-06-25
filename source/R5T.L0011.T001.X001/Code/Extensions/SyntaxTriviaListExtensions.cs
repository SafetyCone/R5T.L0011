using System;

using Microsoft.CodeAnalysis;

using R5T.L0011.T001;


namespace System
{
    public static class SyntaxTriviaListExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static SyntaxTriviaList Append(this SyntaxTriviaList trivias, SyntaxTrivia trivia)
        {
            var output = trivias.Add(trivia);
            return output;
        }

        public static SyntaxTriviaList AppendNewLine(this SyntaxTriviaList trivia)
        {
            var output = trivia.Append(SyntaxFactory.NewLine());
            return output;
        }

        public static SyntaxTriviaList GetNewLineLeadingWhitespace(this SyntaxTriviaList leadingWhitespace)
        {
            var output = leadingWhitespace.Prepend(SyntaxFactory.NewLine());
            return output;
        }

        public static SyntaxTriviaList Prepend(this SyntaxTriviaList trivias, SyntaxTrivia trivia)
        {
            var output = trivias.Insert(0, trivia);
            return output;
        }

        public static SyntaxTriviaList PrependNewLine(this SyntaxTriviaList trivia,
            bool actuallyPrependNewLine = true)
        {
            if(!actuallyPrependNewLine)
            {
                return trivia;
            }

            var output = trivia.Prepend(SyntaxFactory.NewLine());
            return output;
        }
    }
}
