using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class ITypeSymbolExtensions
    {
        public static IEnumerable<IMethodSymbol> GetAllMethods(this IEnumerable<ITypeSymbol> typeSymbols,
            Func<IMethodSymbol, bool> predicate)
        {
            var output = typeSymbols
                .SelectMany(typeSymbol =>
                    typeSymbol.GetAllMethods(predicate));

            return output;
        }

        public static IEnumerable<IMethodSymbol> GetAllExtensionMethodsOfType(this IEnumerable<ITypeSymbol> typeSymbols,
            ITypeSymbol receiverTypeSymbol)
        {
            var output = typeSymbols.GetAllMethods(
                methodSymbol => methodSymbol.IsExtensionOf(receiverTypeSymbol));

            return output;
        }

        public static IEnumerable<IMethodSymbol> GetAllMethods(this ITypeSymbol typeSymbol,
            Func<IMethodSymbol, bool> methodPredicate)
        {
            var output = typeSymbol.GetAllMethods()
                .Where(methodPredicate)
                ;

            return output;
        }

        public static IEnumerable<IMethodSymbol> GetAllMethods(this ITypeSymbol typeSymbol)
        {
            var output = typeSymbol.GetMembers()
                .OfType<IMethodSymbol>()
                ;

            return output;
        }

        public static string GetCodeFilePathFirst(this ITypeSymbol typeSymbol)
        {
            var output = typeSymbol.Locations.First().GetLineSpan().Path;
            return output;
        }

        public static string GetCodeFilePathSingle(this ITypeSymbol typeSymbol)
        {
            var output = typeSymbol.Locations.Single().GetLineSpan().Path;
            return output;
        }

        /// <summary>
        /// Selects <see cref="GetCodeFilePathFirst(ITypeSymbol)"/> as the default.
        /// </summary>
        public static string GetCodeFilePath(this ITypeSymbol typeSymbol)
        {
            var output = typeSymbol.GetCodeFilePathFirst();
            return output;
        }

        public static Document GetDocument(this ITypeSymbol typeSymbol,
            Project project)
        {
            var codeFilePath = typeSymbol.GetCodeFilePath();

            var document = project.GetDocumentWithinProjectByFilePath(codeFilePath);
            return document;
        }

        public static bool IsInAssembly(this ITypeSymbol typeSymbol,
            IAssemblySymbol assemblySymbol)
        {
            var output = SymbolEqualityComparer.Default.Equals(typeSymbol.ContainingAssembly, assemblySymbol);
            return output;
        }

        public static bool IsInAssembly(this ITypeSymbol typeSymbol,
            string assemblyName)
        {
            var output = typeSymbol.ContainingAssembly.Name == assemblyName;
            return output;
        }

        public static string TypeName(this ITypeSymbol typeSymbol)
        {
            var output = typeSymbol.Name;
            return output;
        }

        public static string NamespaceName(this ITypeSymbol typeSymbol)
        {
            var output = typeSymbol.ContainingNamespace.ToString();
            return output;
        }
    }
}
