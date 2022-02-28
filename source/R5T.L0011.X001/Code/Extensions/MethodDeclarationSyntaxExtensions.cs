using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    /// <summary>
    /// NOTE: you probably want <see cref="BaseMethodDeclarationSyntaxExtensions"/>.
    /// </summary>
    public static class MethodDeclarationSyntaxExtensions
    {
        public static string GetConstraintsText(this MethodDeclarationSyntax method)
        {
            var hasConstraints = method.HasConstraints();
            if(!hasConstraints)
            {
                return Strings.Empty;
            }

            var constraintStandardTexts = method.ConstraintClauses
                .Select(xConstraint => xConstraint.ToTextStandard())
                ;

            var output = String.Join(Strings.Space, constraintStandardTexts);
            return output;
        }

        public static ParameterSyntax GetExtensionParameter(this MethodDeclarationSyntax method)
        {
            var hasExtensionParameter = method.HasExtensionParameter();
            if(!hasExtensionParameter)
            {
                throw new Exception("Method did not have an extension parameter.");
            }

            return hasExtensionParameter.Result;
        }

        public static WasFound<ParameterSyntax> HasExtensionParameter(this MethodDeclarationSyntax method)
        {
            var firstOrDefaultParameter = method.ParameterList.Parameters.FirstOrDefault();

            var isExtensionParameter = firstOrDefaultParameter?.IsExtensionParameter()
                ?? false;

            var result = isExtensionParameter
                ? firstOrDefaultParameter
                : default
                ;

            var output = WasFound.From(result);
            return output;
        }

        public static bool HasConstraints(this MethodDeclarationSyntax method)
        {
            var output = method.ConstraintClauses.Any();
            return output;
        }

        public static bool HasParameters(this MethodDeclarationSyntax method)
        {
            var output = method.ParameterList.Parameters.Any();
            return output;
        }

        public static bool HasTypeParameterList(this MethodDeclarationSyntax method)
        {
            var output = method.TypeParameterList is object;
            return output;
        }

        public static bool IsExtensionMethod(this MethodDeclarationSyntax method)
        {
            // Where the first parameter has a "this" keyword modifier.
            var output = method.ParameterList.Parameters
                .FirstOrDefault()?.IsExtensionParameter()
                ?? false;

            return output;
        }

        public static bool IsName(this MethodDeclarationSyntax method,
            string methodName)
        {
            var output = method.Name() == methodName;
            return output;
        }

        public static async Task<MethodDeclarationSyntax> ModifyMethodBody(this MethodDeclarationSyntax method,
            Func<BlockSyntax, Task<BlockSyntax>> methodBodyAction = default)
        {
            // Only do work if work is required.
            if (methodBodyAction == default)
            {
                return method;
            }

            var methodBody = method.Body is object
                ? method.Body
                : SyntaxFactory.Block()
                ;

            var outputMethodBody = await methodBodyAction(methodBody);

            var outputMethod = method.WithBody(outputMethodBody);
            return outputMethod;
        }

        public static string Name(this MethodDeclarationSyntax method)
        {
            var output = method.Identifier.Text;
            return output;
        }
    }
}
