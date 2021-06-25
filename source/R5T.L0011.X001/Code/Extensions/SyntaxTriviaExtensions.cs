using System;
using System.IO;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class SyntaxTriviaExtensions
    {
        public static void WriteTo(this SyntaxTrivia syntaxTrivia, string filePath)
        {
            using var fileWriter = new StreamWriter(filePath);

            syntaxTrivia.WriteTo(fileWriter);
        }
    }
}
