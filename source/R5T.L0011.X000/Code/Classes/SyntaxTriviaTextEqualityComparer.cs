using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;


namespace R5T.L0011.X000
{
    /// <summary>
    /// Equality comparer for <see cref="SyntaxTrivia"/> that uses the text of the trivia.
    /// </summary>
    public class SyntaxTriviaTextEqualityComparer : IEqualityComparer<SyntaxTrivia>
    {
        #region Static

        public static SyntaxTriviaTextEqualityComparer Instance { get; } = new();

        #endregion


        public bool Equals(SyntaxTrivia x, SyntaxTrivia y)
        {
            var output = x.GetText() == y.GetText();
            return output;
        }

        public int GetHashCode(SyntaxTrivia obj)
        {
            return obj.GetText().GetHashCode();
        }
    }
}
