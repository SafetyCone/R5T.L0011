using System;
using System.IO;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class SyntaxTriviaExtensions
    {
        public static bool IsEndOfLine(this SyntaxTrivia syntaxTrivia)
        {
            var output = syntaxTrivia.IsKind(SyntaxKind.EndOfLineTrivia);
            return output;
        }

        public static bool IsNewLine(this SyntaxTrivia syntaxTrivia)
        {
            var output = syntaxTrivia.IsEndOfLine();
            return output;
        }

        public static void WriteTo(this SyntaxTrivia syntaxTrivia, string filePath)
        {
            using var fileWriter = new StreamWriter(filePath);

            syntaxTrivia.WriteTo(fileWriter);
        }
    }
}
