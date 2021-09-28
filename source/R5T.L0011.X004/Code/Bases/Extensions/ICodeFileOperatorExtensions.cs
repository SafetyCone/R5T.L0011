using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;


namespace System
{
    public static class ICodeFileOperatorExtensions
    {
        public static async Task<SyntaxTree> LoadSyntaxTree(this ICodeFileOperator _,
            string filePath)
        {
            using var fileReader = new StreamReader(filePath);

            var fileText = await fileReader.ReadToEndAsync();

            var syntaxTree = CSharpSyntaxTree.ParseText(fileText);
            return syntaxTree;
        }

        public static async Task<CompilationUnitSyntax> LoadCompilationUnit(this ICodeFileOperator _,
            string filePath)
        {
            var syntaxTree = await _.LoadSyntaxTree(filePath);

            var compilationUnit = syntaxTree.GetCompilationUnitRoot();
            return compilationUnit;
        }

        /// <summary>
        /// Quality-of-life overload for <see cref="LoadCompilationUnit(ICodeFile, string)"/>.
        /// </summary>
        public static Task<CompilationUnitSyntax> Load(this ICodeFileOperator _,
            string filePath)
        {
            return _.LoadCompilationUnit(filePath);
        }
    }
}