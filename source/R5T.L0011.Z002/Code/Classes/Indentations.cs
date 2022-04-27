using System;

using Microsoft.CodeAnalysis;

using R5T.L0011.F002;


namespace R5T.L0011.Z002
{
    public static class Indentations
    {
        public static SyntaxTriviaList ByTabCount(int tabCount)
        {
            var output = IndentationHelper.GetTabs_SyntaxTriviaList(tabCount);
            return output;
        }
    }
}
