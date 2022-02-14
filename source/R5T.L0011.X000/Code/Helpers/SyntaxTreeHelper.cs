using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


namespace R5T.L0011.X000
{
    public static class SyntaxTreeHelper
    {
        public static async Task<SyntaxTree> LoadFile(string filePath)
        {
            using var fileReader = new StreamReader(filePath);

            var fileText = await fileReader.ReadToEndAsync();

            var output = SyntaxTreeHelper.ParseText(fileText);
            return output;
        }

        public static SyntaxTree LoadFileSynchronous(string filePath)
        {
            using var fileReader = new StreamReader(filePath);

            var fileText = fileReader.ReadToEnd();

            var output = SyntaxTreeHelper.ParseText(fileText);
            return output;
        }

        public static SyntaxTree ParseText(string text)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(text);
            return syntaxTree;
        }
    }
}
