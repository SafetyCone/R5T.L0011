using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class MethodDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Returns the simple method name. (Just the identifier text.)
        /// </summary>
        public static string GetName_Simple(this MethodDeclarationSyntax method)
        {
            var output = method.Identifier.Text;
            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetName_Simple(MethodDeclarationSyntax)"/> as the default.
        /// </summary>
        public static string GetName(this MethodDeclarationSyntax method)
        {
            var output = method.Identifier.Text;
            return output;
        }

        /// <inheritdoc cref="BaseMethodDeclarationSyntaxExtensions.GetParameterTypedMethodName(BaseMethodDeclarationSyntax, string)"/>
        public static string GetParameterTypedMethodName(this MethodDeclarationSyntax method)
        {
            var simpleMethodName = method.GetName_Simple();

            var output = method.GetParameterTypedMethodName(
                simpleMethodName);

            return output;
        }

        // Note, this is an extension on method, not method base, because the other method type derived from method base (like constructor, destructor, operator) cannot be extension methods.
        public static bool IsExtensionMethod(this MethodDeclarationSyntax method)
        {
            // Where the first parameter has a "this" keyword modifier.
            var output = method.FirstParameterOrDefault()?.IsExtensionParameter()
                ?? false;

            return output;
        }

        /// <summary>
        /// Returns the simple method name.
        /// </summary>
        public static string Name(this MethodDeclarationSyntax method)
        {
            var output = method.GetName_Simple();
            return output;
        }
    }
}
