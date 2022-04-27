using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Glossary = R5T.L0011.X000.Glossary;


namespace System
{
    public static class SyntaxTriviaEnumerableExtensions
    {
        public static IEnumerable<SyntaxTrivia> GetBeginningWithSingleNewLineTrivias(this IEnumerable<SyntaxTrivia> syntaxTrivias)
        {
            // Start with a single new line.
            yield return SyntaxTriviaHelper.NewLine();

            var noNonNewLinesFoundYet = true;
            foreach (var syntaxTrivia in syntaxTrivias)
            {
                if(noNonNewLinesFoundYet && SyntaxTriviaHelper.IsNewLine(syntaxTrivia))
                {
                    continue;
                }

                noNonNewLinesFoundYet = true;

                yield return syntaxTrivia;
            }
        }

        public static SyntaxTriviaList GetBeginningWithSingleNewLineTrivia(this IEnumerable<SyntaxTrivia> syntaxTrivias)
        {
            var output = new SyntaxTriviaList(
                syntaxTrivias.GetBeginningWithSingleNewLineTrivias());

            return output;
        }

        public static int GetBeginningBlankTriviaCount(this IEnumerable<SyntaxTrivia> trivias)
        {
            // Count elements until something is non-blank.
            var counter = 0;

            foreach (var trivia in trivias)
            {
                if (trivia.IsNonBlank())
                {
                    break;
                }
                // Else

                counter++;
            }

            return counter;
        }

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

        public static SyntaxTriviaList SetBeginningBlankTrivia(this IEnumerable<SyntaxTrivia> syntaxTrivias,
            IEnumerable<SyntaxTrivia> beginningBlankTrivias)
        {
            var output = new SyntaxTriviaList(
                syntaxTrivias.SetBeginningBlankTrivias(
                    beginningBlankTrivias));

            return output;
        }

        public static IEnumerable<SyntaxTrivia> SetBeginningBlankTrivias(this IEnumerable<SyntaxTrivia> syntaxTrivias,
            IEnumerable<SyntaxTrivia> beginningBlankTrivias)
        {
            foreach (var syntaxTrivia in beginningBlankTrivias)
            {
                yield return syntaxTrivia;
            }

            var trimmedBeginningBlankTrivias = syntaxTrivias.TrimBeginningBlankTrivias();
            foreach (var syntaxTrivia in trimmedBeginningBlankTrivias)
            {
                yield return syntaxTrivia;
            }
        }

        public static SyntaxTriviaList TrimBeginningBlankTrivia(this IEnumerable<SyntaxTrivia> syntaxTrivias)
        {
            var output = new SyntaxTriviaList(
                syntaxTrivias.TrimBeginningBlankTrivias());
            
            return output;
        }

        public static IEnumerable<SyntaxTrivia> TrimBeginningBlankTrivias(this IEnumerable<SyntaxTrivia> syntaxTrivias)
        {
            var nonBlankTriviaFound = false;
            foreach (var syntaxTrivia in syntaxTrivias)
            {
                if(nonBlankTriviaFound)
                {
                    yield return syntaxTrivia;

                    continue;
                }

                if(!SyntaxTriviaHelper.IsBlank(syntaxTrivia))
                {
                    nonBlankTriviaFound = true;

                    yield return syntaxTrivia;

                    continue;
                }
            }
        }
    }
}

namespace System.Linq
{
    public static class SyntaxTriviaEnumerableExtensions
    {
        /// <summary>
        /// Note: already exists at <see cref="Microsoft.CodeAnalysis.CSharp.SyntaxExtensions.ToSyntaxTriviaList(IEnumerable{SyntaxTrivia})"/>, but that is an annoying namespace.
        /// </summary>
        public static SyntaxTriviaList ToSyntaxTriviaList(this IEnumerable<SyntaxTrivia> trivias)
        {
            var output = new SyntaxTriviaList(trivias);
            return output;
        }
    }
}