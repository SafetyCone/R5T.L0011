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
    }
}
