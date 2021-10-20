using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0036;

using Instances = R5T.L0011.X004.Instances;


namespace System
{
    public static class IMethodNameOperatorExtensions
    {
        /// <summary>
        /// Does not include the extension parameter.
        /// </summary>
        public static string GetNamespacedTypedParameterizedMethodNameWithoutExtensionParameter(this IMethodNameOperator _,
            (NamespaceDeclarationSyntax Namespace, ClassDeclarationSyntax StaticClass, MethodDeclarationSyntax ExtensionMethod) tuple)
        {
            var namespaceName = tuple.Namespace.Name.ToString();
            var typeName = tuple.StaticClass.Identifier.ToString();
            var namespacedTypeName = Instances.NamespacedTypeName.GetNamespacedName(
                namespaceName,
                typeName);

            var methodName = tuple.ExtensionMethod.Identifier.ToString();

            var typeParameterNames = tuple.ExtensionMethod.ParameterList.Parameters
                .Where(xParameter => !xParameter.IsExtensionParameter())
                .Select(xParameter => $"{xParameter.Identifier}: {xParameter.Type}")
                ;

            var typeParametersArray = $"{Strings.OpenParenthesis}{String.Join(Strings.CommaSeparatedListSpacedSeparator, typeParameterNames)}{Strings.CloseParenthesis}";

            var namespacedTypedParameterizedMethodName = $"{namespacedTypeName}.{methodName}{typeParametersArray}";
            return namespacedTypedParameterizedMethodName;
        }

        /// <summary>
        /// Can handle extension parameters.
        /// </summary>
        public static string GetNamespacedTypedParameterizedMethodName(this IMethodNameOperator _,
            (NamespaceDeclarationSyntax Namespace, ClassDeclarationSyntax StaticClass, MethodDeclarationSyntax ExtensionMethod) tuple)
        {
            var namespaceName = tuple.Namespace.Name.ToString();
            var typeName = tuple.StaticClass.Identifier.ToString();
            var namespacedTypeName = Instances.NamespacedTypeName.GetNamespacedName(
                namespaceName,
                typeName);

            var methodName = tuple.ExtensionMethod.Identifier.ToString();

            var typeParameterNames = tuple.ExtensionMethod.ParameterList.Parameters
                .Select(xParameter => xParameter.ToStringStandard())
                ;

            var typeParametersArray = $"{Strings.OpenParenthesis}{String.Join(Strings.CommaSeparatedListSpacedSeparator, typeParameterNames)}{Strings.CloseParenthesis}";

            var namespacedTypedParameterizedMethodName = $"{namespacedTypeName}.{methodName}{typeParametersArray}";
            return namespacedTypedParameterizedMethodName;
        }
    }
}
