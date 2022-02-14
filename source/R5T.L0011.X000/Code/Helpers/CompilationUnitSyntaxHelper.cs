using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace R5T.L0011.X000
{
    public static class CompilationUnitSyntaxHelper
    {
        public static async Task<CompilationUnitSyntax> LoadFile(string filePath)
        {
            var fileText = await File.ReadAllTextAsync(filePath);

            var compilationUnit = CompilationUnitSyntaxHelper.ParseText(fileText);
            return compilationUnit;
        }

        public static CompilationUnitSyntax LoadFileSynchronous(string filePath)
        {
            var fileText = File.ReadAllText(filePath);

            var compilationUnit = CompilationUnitSyntaxHelper.ParseText(fileText);
            return compilationUnit;
        }

        public static CompilationUnitSyntax New()
        {
            var output = SyntaxFactory.CompilationUnit();
            return output;
        }

        public static CompilationUnitSyntax ParseText(string text)
        {
            var compilationUnit = SyntaxFactory.ParseCompilationUnit(text);
            return compilationUnit;
        }
    }
}
