using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

using R5T.Magyar;

using R5T.L0011.X001;

using Instances = R5T.L0011.X001.Instances;


namespace System
{
    public static class CompilationExtensions
    {
        // Based on: https://github.com/dotnet/roslyn/issues/6138#issuecomment-626266325
        public static IEnumerable<INamedTypeSymbol> GetAllNamedTypeSymbols(this Compilation compilation,
            Func<INamedTypeSymbol, bool> typeSymbolPredicate)
        {
            var namespacesStack = new Stack<INamespaceSymbol>();

            namespacesStack.Push(compilation.GlobalNamespace);

            while (namespacesStack.Any())
            {
                var @namespace = namespacesStack.Pop();

                foreach (var member in @namespace.GetMembers())
                {
                    if (member is INamespaceSymbol memberAsNamespace)
                    {
                        namespacesStack.Push(memberAsNamespace);

                        continue;
                    }

                    if (member is INamedTypeSymbol memberAsNamedTypeSymbol)
                    {
                        if(typeSymbolPredicate(memberAsNamedTypeSymbol))
                        {
                            yield return memberAsNamedTypeSymbol;
                        }
                    }
                }
            }
        }
        
        public static IEnumerable<INamedTypeSymbol> GetAllNamedTypeSymbols(this Compilation compilation)
        {
            var output = compilation.GetAllNamedTypeSymbols(typeSymbol => true);
            return output;
        }

        public static IEnumerable<INamedTypeSymbol> GetAllNamedTypeSymbolsInAssembly(this Compilation compilation,
            IAssemblySymbol assemblySymbol)
        {
            var output = compilation.GetAllNamedTypeSymbols(
                typeSymbol => typeSymbol.IsInAssembly(assemblySymbol));

            return output;
        }

        public static IEnumerable<INamedTypeSymbol> GetAllNamedTypeSymbolsInAssemblies(this Compilation compilation,
            IEnumerable<string> assemblyNames)
        {
            var assemblyNameHashSet = new HashSet<string>(assemblyNames);

            var output = compilation.GetAllNamedTypeSymbols(
                typeSymbol => assemblyNameHashSet.Contains(typeSymbol.ContainingAssembly.Name));

            return output;
        }

        /// <summary>
        /// NOTE! This is not *just* the type, but all types!
        /// </summary>
        public static IEnumerable<IMethodSymbol> GetAllExtensionMethodsForType(this Compilation compilation,
            INamedTypeSymbol typeSymbol)
        {
            var output = compilation.GetAllNamedTypeSymbols()
                .SelectMany(typeSymbol => typeSymbol.GetAllMethods())
                .Where(methodSymbol => methodSymbol.IsExtensionMethod && methodSymbol.ReduceExtensionMethod(typeSymbol) != null)
                ;

            return output;
        }        

        /// <summary>
        /// Quality-of-life overload for <see cref="HasTypeSymbolByNamespacedTypeName(Compilation, string)"/>.
        /// </summary>
        public static WasFound<INamedTypeSymbol> HasNamespacedTypeName(this Compilation compilation,
            string namespacedTypeName)
        {
            var output = compilation.HasTypeSymbolByNamespacedTypeName(namespacedTypeName);
            return output;
        }

        public static WasFound<INamedTypeSymbol> HasTypeSymbolByNamespacedTypeName(this Compilation compilation,
            string namespacedTypeName)
        {
            var typeSymbolOrNull = compilation.GetTypeByMetadataName(namespacedTypeName);

            var exists = NullHelper.IsNonNull(typeSymbolOrNull);

            var output = WasFound.From(exists, typeSymbolOrNull);
            return output;
        }

        public static INamedTypeSymbol GetTypeSymbolByNamespacedTypeName(this Compilation compilation,
            string namespacedTypeName)
        {
            var wasFound = compilation.HasTypeSymbolByNamespacedTypeName(namespacedTypeName);
            if (!wasFound)
            {
                throw Instances.ExceptionGenerator.TypeNotFoundInCompilation(
                    compilation,
                    namespacedTypeName,
                    nameof(namespacedTypeName));
            }

            return wasFound.Result;
        }

        public static Compilation VerifyNoCompilationErrors(this Compilation compilation)
        {
            var diagnostics = compilation.GetDiagnostics();

            var anyErrors = diagnostics
                .Where(x => x.Severity == DiagnosticSeverity.Error)
                .Any();

            if (anyErrors)
            {
                throw new Exception("There were errors in the compilation.");
            }

            return compilation;
        }

        public static async Task<Compilation> VerifyNoCompilationErrors(this Task<Compilation> gettingCompilation)
        {
            var compilation = await gettingCompilation;

            compilation.VerifyNoCompilationErrors();

            return compilation;
        }
    }
}
