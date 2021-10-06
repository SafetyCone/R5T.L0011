using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    /// <summary>
    /// NOTE: you probably want <see cref="BaseMethodDeclarationSyntaxExtensions"/>.
    /// </summary>
    public static class MethodDeclarationSyntaxExtensions
    {
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

        public static bool IsExtensionMethod(this MethodDeclarationSyntax method)
        {
            // Where the first parameter has a "this" keyword modifier.
            var output = method.ParameterList.Parameters
                .FirstOrDefault()?.IsExtensionParameter()
                ?? false;

            return output;
        }

        public static string Name(this MethodDeclarationSyntax method)
        {
            var output = method.Identifier.Text;
            return output;
        }
    }
}
