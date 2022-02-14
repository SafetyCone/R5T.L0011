using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;


namespace R5T.L0011.X000
{
    public class SyntaxTriviaValueEqualityComparer : IEqualityComparer<SyntaxTrivia>
    {
        #region Static

        public static SyntaxTriviaValueEqualityComparer Instance { get; } = new();

        #endregion


        public bool Equals(SyntaxTrivia x, SyntaxTrivia y)
        {
            var output = x.ToFullString() == y.ToFullString();
            return output;
        }

        public int GetHashCode(SyntaxTrivia obj)
        {
            return obj.ToFullString().GetHashCode();
        }
    }
}
