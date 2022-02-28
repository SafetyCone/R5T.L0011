using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.T0036;

using Instances = R5T.L0011.X004.Instances;


namespace System
{
    public static class IMethodNameOperatorExtensions
    {
        public static string GetFullName(this IMethodNameOperator _,
            MethodDeclarationSyntax methodDeclaration)
        {
            // Remove the body of the method declaration, then ask for the text of the method declaration, and remove any multiple spacings.
            var methodWithoutBody = methodDeclaration.WithoutAnyBody();

            // Replace tab and new lines with space.
            var output = methodWithoutBody.ToStringWithSingleSpacing();
            return output;
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
        public static string GetNamespacedTypedParameterizedMethodNameWithTypeParameters(this IMethodNameOperator _,
            (NamespaceDeclarationSyntax Namespace, ClassDeclarationSyntax StaticClass, MethodDeclarationSyntax ExtensionMethod) tuple)
        {
            var namespaceName = tuple.Namespace.Name.ToString();
            var typeName = tuple.StaticClass.Identifier.ToString();
            var namespacedTypeName = Instances.NamespacedTypeName.GetNamespacedName(
                namespaceName,
                typeName);

            var methodName = _.GetTypeParameterListedMethodName(
                tuple.ExtensionMethod);

            var namespacedTypedParameterizedMethodName = $"{namespacedTypeName}.{methodName}";
            return namespacedTypedParameterizedMethodName;
        }

        public static string GetTypeParameterListedMethodName(this IMethodNameOperator _,
            MethodDeclarationSyntax method)
        {
            var methodName = method.Identifier.ToString();

            var hasParameters = method.HasParameters();
            var parametersSegment = hasParameters
                ? method.ParameterList.ToTextStandard()
                : Strings.Empty
                ;

            var hasTypeParameterList = method.HasTypeParameterList();
            var typeParametersSegment = hasTypeParameterList
                ? method.TypeParameterList.ToTextStandard()
                : Strings.Empty
                ;

            var hasConstraints = method.HasConstraints();
            var constraintsText = hasConstraints
                ? method.GetConstraintsText()
                : String.Empty;
                ;

            var constraintsSegment = hasConstraints
                ? $"{Strings.Space}{constraintsText}"
                : String.Empty
                ;

            var output = $"{methodName}{typeParametersSegment}{parametersSegment}{constraintsSegment}";
            return output;
        }
    }
}
