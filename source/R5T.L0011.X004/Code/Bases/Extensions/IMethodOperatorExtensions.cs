using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;


namespace System
{
    public static class IMethodOperatorExtensions
    {
        public static Func<MethodDeclarationSyntax, bool> GetIsExtensionMethodPredicate(this IMethodOperator _,
            Func<MethodDeclarationSyntax, bool> predicateOnExtensionMethod = default)
        {
            bool Output(MethodDeclarationSyntax method)
            {
                // Only if the method is an extension method, do we care about the caller-specified input predicate.
                var isExtensionMethod = method.IsExtensionMethod();

                var output = isExtensionMethod
                    && FunctionHelper.Run(predicateOnExtensionMethod, method)
                    ;

                return output;
            }
            return Output;
        }
    }
}
