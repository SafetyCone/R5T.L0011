using System;


namespace Microsoft.CodeAnalysis.CSharp
{
    public static class SyntaxFactoryHelper
    {
        public static SyntaxTrivia EndOfLine_Environment()
        {
            var output = SyntaxFactory.EndOfLine(Environment.NewLine);
            return output;
        }

        public static SyntaxTrivia EndOfLine()
        {
            var output = SyntaxFactoryHelper.EndOfLine_Environment();
            return output;
        }

        public static SyntaxTrivia NewLine()
        {
            var output = SyntaxFactoryHelper.EndOfLine_Environment();
            return output;
        }
    }
}
