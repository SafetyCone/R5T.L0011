using System;

using Microsoft.CodeAnalysis;

using R5T.L0011.X003;


namespace System
{
    public static class ITypeSymbolExtensions
    {
        public static string GetNamespacedTypeName(this ITypeSymbol typeSymbol)
        {
            var namespaceName = typeSymbol.NamespaceName();
            var typeName = typeSymbol.TypeName();

            var output = Instances.NamespacedTypeName.From(
                namespaceName,
                typeName);

            return output;
        }

        public static string GetNamespacedTypeNameIncludingGenericTypeArguments(this ITypeSymbol typeSymbol)
        {
            var output = typeSymbol.ToString();
            return output;
        }

        public static bool IsNamespacedTypeName(this ITypeSymbol typeSymbol,
            string namespacedTypeName)
        {
            var namespacedTypeNameOfSymbol = typeSymbol.GetNamespacedTypeName();

            var output = namespacedTypeNameOfSymbol == namespacedTypeName;
            return output;
        }

        public static bool IsAssemblySpecifiedNamespacedTypeName(this INamedTypeSymbol namedTypeSymbol,
            string assemblyName,
            string namespacedTypeName)
        {
            var output =
                namedTypeSymbol.IsNamespacedTypeName(namespacedTypeName)
                && namedTypeSymbol.IsInAssembly(assemblyName)
                ;

            return output;
        }

        public static bool IsProjectSpecifiedNamespacedTypeName(this INamedTypeSymbol namedTypeSymbol,
            string projectName,
            string namespacedTypeName,
            Solution solution)
        {
            var projectAssemblyName = solution.GetProject(projectName).AssemblyName;

            var output = namedTypeSymbol.IsAssemblySpecifiedNamespacedTypeName(projectAssemblyName, namespacedTypeName);
            return output;
        }
    }
}
