using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;
using R5T.L0011.X002;

using CSharpSyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static class SyntaxTriviaListExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static SyntaxTriviaList RemoveFirstToken(this SyntaxTriviaList trivia)
        {
            var output = trivia.RemoveAt(0);
            return output;
        }

        public static bool FirstTokenIsNewLineNotIncludingStructuredTrivia(this SyntaxTriviaList trivia)
        {
            var output = trivia.First().IsNewLine();
            return output;
        }

        /// <summary>
        /// Handles the case where leading trivia is structured, and has its own leadin
        /// </summary>
        public static bool FirstTokenIsNewLine(this SyntaxTriviaList trivia)
        {
            var firstNodeHasStructure = trivia.First().HasStructure;
            if (firstNodeHasStructure)
            {
                var firstNodeTrivia = trivia.First().GetStructure().GetLeadingTrivia();

                var output = firstNodeTrivia.FirstTokenIsNewLine();
                return output;
            }
            else
            {
                var output = trivia.FirstTokenIsNewLineNotIncludingStructuredTrivia();
                return output;
            }
        }

        public static SyntaxTriviaList RemoveLeadingNewLineNotIncludingStructuredTrivia(this SyntaxTriviaList trivia)
        {
            var output = trivia;

            var canRemove = trivia.Count > 0;
            if(canRemove)
            {
                var firstTokenIsNewLine = trivia.FirstTokenIsNewLineNotIncludingStructuredTrivia();
                if(firstTokenIsNewLine)
                {
                    output = trivia.RemoveFirstToken();
                }
            }

            return output;
        }

        public static SyntaxTriviaList RemoveLeadingNewLine(this SyntaxTriviaList trivia)
        {
            var output = trivia;

            var canRemove = trivia.Count > 0;
            if (canRemove)
            {
                var firstTrivia = trivia.First();
                if(firstTrivia.HasStructure)
                {
                    var structuredFirstTriviaNode = firstTrivia.GetStructure() as StructuredTriviaSyntax;

                    var structuredFirstTriviaNodeLeadingTrivia = structuredFirstTriviaNode.GetLeadingTrivia();

                    var structuredNewFirstTriviaNodeLeadingTrivia = structuredFirstTriviaNodeLeadingTrivia.RemoveLeadingNewLine();

                    var newStructureFirstTriviaNode = structuredFirstTriviaNode.WithLeadingTrivia(structuredNewFirstTriviaNodeLeadingTrivia);

                    var newTriviaToken = CSharpSyntaxFactory.Trivia(newStructureFirstTriviaNode);

                    output = output.Replace(firstTrivia, newTriviaToken);
                }
                else
                {
                    output = trivia.RemoveLeadingNewLineNotIncludingStructuredTrivia();
                }
            }
            // Else, just return the input trivia.

            return output;
        }

        public static SyntaxTriviaList IndentByTab(this SyntaxTriviaList trivia)
        {
            var tab = SyntaxFactory.Tab();

            var output = trivia.Add(tab);
            return output;
        }

        public static SyntaxTriviaList IndentByTabs(this SyntaxTriviaList trivia,
            int tabCount)
        {
            var modifiedTrivia = trivia;

            for (int iTab = 0; iTab < tabCount; iTab++)
            {
                modifiedTrivia = modifiedTrivia.IndentByTab();
            }

            return modifiedTrivia;
        }
    }
}
