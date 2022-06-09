using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Microsoft.CodeAnalysis.CSharp
{
    public static class SyntaxFactoryHelper
    {
        public static SyntaxAnnotation Annotation()
        {
            var output = new SyntaxAnnotation();
            return output;
        }

        public static BaseListSyntax BaseList()
        {
            var output = SyntaxFactory.BaseList();
            return output;
        }

        public static SyntaxToken CloseBrace()
        {
            var output = SyntaxFactory.Token(SyntaxKind.CloseBraceToken);
            return output;
        }

        public static SyntaxToken None()
        {
            var output = SyntaxFactory.Token(SyntaxKind.None);
            return output;
        }

        public static SyntaxTrivia EndOfLine(string endOfLineText)
        {
            var output = SyntaxFactory.EndOfLine(endOfLineText);
            return output;
        }

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

        public static SyntaxTrivia NewLine(string newLineText)
        {
            var output = SyntaxFactoryHelper.EndOfLine(newLineText);
            return output;
        }

        public static SyntaxTrivia NewLine()
        {
            var output = SyntaxFactoryHelper.EndOfLine();
            return output;
        }

        public static SyntaxToken OpenBrace()
        {
            var output = SyntaxFactory.Token(SyntaxKind.OpenBraceToken);
            return output;
        }

        public static SyntaxTrivia Space()
        {
            var output = SyntaxFactory.Space;
            return output;
        }

        public static SyntaxTrivia Tab()
        {
            var output = SyntaxFactory.Tab;
            return output;
        }
    }
}
