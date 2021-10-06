using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;


namespace System
{
    public static class IClassOperatorExtensions
    {
        public static Func<ClassDeclarationSyntax, bool> GetIsStaticClassPredicate(this IClassOperator _,
            Func<ClassDeclarationSyntax, bool> predicateOnStaticClassDeclaration = default)
        {
            bool Output(ClassDeclarationSyntax @class)
            {
                // Only if the class is static, do we care about the caller-specified input predicate.
                var isStatic = @class.IsStatic();

                var output = isStatic
                    && FunctionHelper.Run(predicateOnStaticClassDeclaration, @class)
                    ;

                return output;
            }
            return Output;
        }
    }
}
