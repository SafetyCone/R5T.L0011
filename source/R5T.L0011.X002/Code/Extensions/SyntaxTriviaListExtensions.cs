using System;

using Microsoft.CodeAnalysis;

using R5T.L0011.T001;


namespace System
{
    public static class SyntaxTriviaListExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static SyntaxTriviaList RemoveLeadingNewLine(this SyntaxTriviaList trivia)
        {
            var canRemove = trivia.Count > 0;

            var output = canRemove
                ? trivia.RemoveAt(0)
                : trivia;

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
