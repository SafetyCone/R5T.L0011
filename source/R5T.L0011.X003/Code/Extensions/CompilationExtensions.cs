using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class CompilationExtensions
    {
        public static bool HasAllNamespacedTypeNames(this Compilation compilation,
            IEnumerable<string> namespacedTypeNames)
        {
            var hashSet = new HashSet<string>(namespacedTypeNames);

            compilation.GetAllNamedTypeSymbols()
                .ForEach(typeSymbol =>
                {
                    var typeSymbolNamespacedTypeName = typeSymbol.GetNamespacedTypeName();

                    hashSet.Remove(typeSymbolNamespacedTypeName); // Ok, idempotent.
                });

            var hasAll = hashSet.Count == 0;
            return hasAll;
        }
    }
}
