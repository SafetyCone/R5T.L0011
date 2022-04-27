using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Microsoft.CodeAnalysis.CSharp
{
    public static class CompilationUnitSyntaxHelper
    {
        public static async Task<CompilationUnitSyntax> LoadCompilationUnitFromFile(string filePath)
        {
            var fileText = await File.ReadAllTextAsync(filePath);

            var compilationUnit = CompilationUnitSyntaxHelper.ParseCompilationUnitFromText(fileText);
            return compilationUnit;
        }

        public static CompilationUnitSyntax LoadCompilationUnitFromFile_Synchronous(string filePath)
        {
            var fileText = File.ReadAllText(filePath);

            var compilationUnit = CompilationUnitSyntaxHelper.ParseCompilationUnitFromText(fileText);
            return compilationUnit;
        }

        public static Task<CompilationUnitSyntax> LoadFile(string filePath)
        {
            return CompilationUnitSyntaxHelper.LoadCompilationUnitFromFile(filePath);
        }

        public static CompilationUnitSyntax LoadFile_Synchronous(string filePath)
        {
            return CompilationUnitSyntaxHelper.LoadCompilationUnitFromFile_Synchronous(filePath);
        }

        public static CompilationUnitSyntax CreateNew()
        {
            var output = SyntaxFactory.CompilationUnit();
            return output;
        }

        public static CompilationUnitSyntax ParseCompilationUnitFromText(string text)
        {
            var compilationUnit = SyntaxFactory.ParseCompilationUnit(text);
            return compilationUnit;
        }

        public static CompilationUnitSyntax ParseText(string text)
        {
            return CompilationUnitSyntaxHelper.ParseCompilationUnitFromText(text);
        }
    }
}
