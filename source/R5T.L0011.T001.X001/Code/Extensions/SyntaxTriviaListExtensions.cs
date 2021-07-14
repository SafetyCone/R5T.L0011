using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

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

        /// <summary>
        /// *Only* if the first syntax trivia is a new line, remove the new line and remove whitespace until the next newline.
        /// </summary>
        public static SyntaxTriviaList RemoveLeadingNewLine(this SyntaxTriviaList whitespace)
        {
            var firstTriviaIsNewLine = whitespace.First().IsNewLine();
            if(firstTriviaIsNewLine)
            {
                var firstIndexToTake = 1;
                foreach (var trivia in whitespace.SkipFirst())
                {
                    if (trivia.IsNewLine())
                    {
                        break;
                    }
                    else
                    {
                        firstIndexToTake++;
                    }
                }

                var output = new SyntaxTriviaList(whitespace.Skip(firstIndexToTake));
                return output;
            }
            else
            {
                return whitespace;
            }
        }
    }
}
