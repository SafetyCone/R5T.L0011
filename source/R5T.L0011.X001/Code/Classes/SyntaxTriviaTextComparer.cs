using System;
using System.Collections.Generic;


namespace Microsoft.CodeAnalysis
{
    public class SyntaxTriviaTextComparer : IEqualityComparer<SyntaxTrivia>
    {
        #region Static

        public static SyntaxTriviaTextComparer Instance { get; } = new();

        #endregion


        public bool Equals(SyntaxTrivia x, SyntaxTrivia y)
        {
            var xFullString = x.ToFullString();
            var yFullString = y.ToFullString();

            var output = xFullString == yFullString;
            return output;
        }

        public int GetHashCode(SyntaxTrivia obj)
        {
            return obj.GetHashCode();
        }
    }
}
