using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class SemanticModelExtensions
    {
        public static IEnumerable<INamedTypeSymbol> GetTypeSymbols(this SemanticModel semanticModel,
            IEnumerable<TypeDeclarationSyntax> typeDeclarations)
        {
            var typeSymbols = typeDeclarations
                .Select(typeDeclaration => semanticModel.GetDeclaredSymbol(typeDeclaration))
                ;

            return typeSymbols;
        }

        public static SemanticModel VerifyNoCompilationErrors(this SemanticModel semanticModel)
        {
            semanticModel.Compilation.VerifyNoCompilationErrors();

            return semanticModel;
        }
    }
}
