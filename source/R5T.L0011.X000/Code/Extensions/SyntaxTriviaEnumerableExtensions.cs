using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;

using Glossary = R5T.L0011.X000.Glossary;


namespace System
{
    public static class SyntaxTriviaEnumerableExtensions
    {
        /// <inheritdoc cref="GetBeginningBlankTrivias(SyntaxTriviaList)"/>
        public static SyntaxTriviaList GetBeginningBlankTrivia(this IEnumerable<SyntaxTrivia> trivias)
        {
            var output = new SyntaxTriviaList(
                trivias.GetBeginningBlankTrivias());

            return output;
        }

        /// <summary>
        /// <para>Get the blank trivia at the beginning of the syntax trivia list, up-to (but not including) the first non-blank trivia encountered.</para>
        /// <inheritdoc cref="Glossary.ForTrivia.BlankTrivia" path="/definition"/>
        /// </summary>
        public static IEnumerable<SyntaxTrivia> GetBeginningBlankTrivias(this IEnumerable<SyntaxTrivia> trivias)
        {
            foreach (var trivia in trivias)
            {
                if (trivia.IsNonBlank())
                {
                    break;
                }

                yield return trivia;
            }
        }

        /// <inheritdoc cref="GetEndingBlankTrivias(SyntaxTriviaList)"/>
        public static SyntaxTriviaList GetEndingBlankTrivia(this SyntaxTriviaList trivias)
        {
            var output = new SyntaxTriviaList(
                trivias.GetEndingBlankTrivias());

            return output;
        }

        /// <summary>
        /// <para>Get the blank trivia at the end of the syntax trivia list, starting from (but not including) the last non-blank trivia encountered.</para>
        /// <inheritdoc cref="Glossary.ForTrivia.BlankTrivia" path="/definition"/>
        /// </summary>
        public static IEnumerable<SyntaxTrivia> GetEndingBlankTrivias(this SyntaxTriviaList trivias)
        {
            // Reverse the trivia, get the beginning blank trivia, then reverse that.
            var output = trivias
                .Reverse()
                .GetBeginningBlankTrivia()
                .Reverse()
                ;

            return output;
        }
    }
}

namespace System.Linq
{
    public static class SyntaxTriviaEnumerableExtensions
    {
        public static SyntaxTriviaList ToSyntaxTriviaList(this IEnumerable<SyntaxTrivia> trivias)
        {
            var output = new SyntaxTriviaList(trivias);
            return output;
        }
    }
}