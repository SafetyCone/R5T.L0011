using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class ConstructorDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Example: X.Y.X.Method(string, string, int).
        /// </summary>
        public static string GetNamespacedTypeNamedParameterTypedMethodName(this ConstructorDeclarationSyntax constructor)
        {
            var containingType = constructor.GetContainingType();

            var containingTypeNamespacedTypeName = containingType.GetNamespacedTypeName();

            var parameterTypedMethodName = constructor.GetParameterTypedMethodName();

            var output = Instances.NamespaceName.CombineTokens(
                containingTypeNamespacedTypeName,
                parameterTypedMethodName);

            return output;
        }
    }
}
