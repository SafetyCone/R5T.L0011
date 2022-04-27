using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class MethodDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Example: X.Y.X.Method(string, string, int).
        /// </summary>
        public static string GetNamespacedTypeNamedParameterTypedMethodName(this MethodDeclarationSyntax method)
        {
            var containingType = method.GetContainingType();

            var containingTypeNamespacedTypeName = containingType.GetNamespacedTypeName();

            var parameterTypedMethodName = method.GetParameterTypedMethodName();

            var output = Instances.NamespaceName.CombineTokens(
                containingTypeNamespacedTypeName,
                parameterTypedMethodName);

            return output;
        }
    }
}
