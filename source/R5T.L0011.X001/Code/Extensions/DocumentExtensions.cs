using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class DocumentExtensions
    {
        public static async Task<INamedTypeSymbol[]> GetClassSymbols(this Document document,
            IEnumerable<ClassDeclarationSyntax> classDeclarations)
        {
            var semanticModel = await document.GetSemanticModel();

            var classSymbols = classDeclarations
                .Select(x => semanticModel.GetDeclaredSymbol(x))
                .ToArray();

            return classSymbols;
        }

        public static async Task<INamedTypeSymbol[]> GetClassSymbols(this Document document)
        {
            var compilationUnit = await document.GetCompilationUnit();

            var classes = compilationUnit.GetClasses();

            var output = await document.GetClassSymbols(classes);
            return output;
        }

        public static async Task<CompilationUnitSyntax> GetCompilationUnit(this Document document)
        {
            var syntaxRoot = await document.GetSyntaxRootAsync();

            var output = syntaxRoot as CompilationUnitSyntax;
            return output;
        }

        public static async Task<SemanticModel> GetSemanticModel(this Document document)
        {
            var semanticModel = await document.GetSemanticModelAsync();

            semanticModel.VerifyNoCompilationErrors();

            return semanticModel;
        }

        /// <summary>
        /// Quality-of-life overload for <see cref="GetCompilationUnit(Document)"/>.
        /// </summary>
        public static Task<CompilationUnitSyntax> GetSyntaxModel(this Document document)
        {
            return document.GetCompilationUnit();
        }

        public static async Task<INamedTypeSymbol[]> GetNamedTypeSymbols<T>(this Document document,
            IEnumerable<T> typeDeclarations)
            where T : TypeDeclarationSyntax
        {
            var semanticModel = await document.GetSemanticModel();

            var typeSymbols = typeDeclarations
                .Select(x => semanticModel.GetDeclaredSymbol(x))
                .ToArray();

            return typeSymbols;
        }

        public static IEnumerable<Document> WhereFilePathIs(this IEnumerable<Document> documents,
            string codeFilePath)
        {
            var output = documents.Where(document => document.FilePath == codeFilePath);
            return output;
        }
    }
}
