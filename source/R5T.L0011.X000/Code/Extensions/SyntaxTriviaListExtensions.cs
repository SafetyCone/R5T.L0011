using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Glossary = R5T.L0011.X000.Glossary;


namespace System
{
    public static class SyntaxTriviaListExtensions
    {
        public static SyntaxTriviaList AddLeadingTrivia(this SyntaxTriviaList trivia,
            IEnumerable<SyntaxTrivia> trivias)
        {
            var output = trivia.InsertRange(0, trivias);
            return output;
        }

        public static SyntaxTriviaList AddLeadingTrivia(this SyntaxTriviaList trivia,
            params SyntaxTrivia[] trivias)
        {
            var output = trivia.AddLeadingTrivia(trivias.AsEnumerable());
            return output;
        }

        public static SyntaxTriviaList AddTrailingTrivia(this SyntaxTriviaList trivia,
            IEnumerable<SyntaxTrivia> trivias)
        {
            var output = trivia.AddRange(trivias);
            return output;
        }

        public static SyntaxTriviaList AddTrailingTrivia(this SyntaxTriviaList trivia,
            params SyntaxTrivia[] trivias)
        {
            var output = trivia.AddTrailingTrivia(trivias.AsEnumerable());
            return output;
        }

        public static SyntaxTriviaList Append(this SyntaxTriviaList trivia,
            params SyntaxTrivia[] trivias)
        {
            var output = trivia.Append(trivias.AsEnumerable()); ;
            return output;
        }

        public static SyntaxTriviaList Append(this SyntaxTriviaList trivia,
            IEnumerable<SyntaxTrivia> trivias)
        {
            var output = trivia.AddRange(trivias);
            return output;
        }

        public static SyntaxTriviaList AppendNewLine(this SyntaxTriviaList trivia)
        {
            var output = trivia.Append(SyntaxFactoryHelper.NewLine());
            return output;
        }

        public static bool BeginsWithNewLine(this SyntaxTriviaList trivia)
        {
            var output = trivia.Any() && trivia.First().IsNewLine();
            return output;
        }

        public static bool ContainsBlankLine(this SyntaxTriviaList trivias)
        {
            var initialNewLineFound = false;

            foreach (var trivia in trivias)
            {
                if(trivia.IsNewLine())
                {
                    if(initialNewLineFound)
                    {
                        // We have found two new lines in a row. Return true.
                        return true;
                    }

                    initialNewLineFound = true;
                }

                if(trivia.IsNonWhitespace())
                {
                    // Reset.
                    initialNewLineFound = false;
                }
            }

            return false;
        }

        public static bool EndsWithNewLine(this SyntaxTriviaList trivia)
        {
            var output = trivia.Any() && trivia.Last().IsNewLine();
            return output;
        }


        public static bool Exceeds(this SyntaxTriviaList trivia,
            SyntaxTriviaList otherTrivia)
        {
            var output = trivia.Count > otherTrivia.Count;
            return output;
        }

        /// <summary>
        /// <para>Determines whether the syntax trivia list is indentation.</para>
        /// <inheritdoc cref="Glossary.Indentation" path="/definition"/>
        /// </summary>
        public static bool IsIndentation(this SyntaxTriviaList trivias)
        {
            var anyNonBlankTrivia = trivias
                .Where(xTrivia => xTrivia.IsNonBlank())
                .Any();

            var output = !anyNonBlankTrivia;
            return output;
        }

        public static SyntaxTriviaList Prepend(this SyntaxTriviaList trivias,
            SyntaxTrivia trivia)
        {
            var output = trivias.Insert(0, trivia);
            return output;
        }

        public static SyntaxTriviaList PrependNewLine(this SyntaxTriviaList trivia)
        {
            var output = trivia.AddLeadingTrivia(SyntaxFactoryHelper.NewLine());
            return output;
        }

        public static bool StartsWithNewLine(this SyntaxTriviaList trivia)
        {
            var output = trivia.Any() && trivia.First().IsNewLine();
            return output;
        }
    }
}
