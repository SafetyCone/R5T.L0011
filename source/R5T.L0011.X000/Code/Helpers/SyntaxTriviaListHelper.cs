using System;

using Microsoft.CodeAnalysis;

using R5T.L0011.X000.N8;

using Glossary = R5T.L0011.X000.Glossary;


namespace Microsoft.CodeAnalysis.CSharp
{
    public static class SyntaxTriviaListHelper
    {
        public static SyntaxTriviaList NewLine()
        {
            var output = SyntaxFactoryHelper.NewLine()
                .ToSyntaxTriviaList();

            return output;
        }
    }
}
