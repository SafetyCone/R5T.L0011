using System;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.Magyar;

using Instances = R5T.L0011.T001.X001.Instances;


namespace System
{
    public static class SyntaxTriviaListExtensions
    {
        public static SyntaxTriviaList GetNewLineLeadingWhitespace(this SyntaxTriviaList leadingWhitespace)
        {
            var output = leadingWhitespace.Prepend(Instances.SyntaxFactory.NewLine());
            return output;
        }

        /// <summary>
        /// *Only* if the first syntax trivia is a new line, remove the new line and remove whitespace until the next newline.
        /// </summary>
        public static SyntaxTriviaList RemoveFullLeadingNewLine(this SyntaxTriviaList whitespace)
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
