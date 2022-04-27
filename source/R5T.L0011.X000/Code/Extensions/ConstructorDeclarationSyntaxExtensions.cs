using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class ConstructorDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Returns the simple method name. (Just the identifier text.)
        /// </summary>
        public static string GetName_Simple(this ConstructorDeclarationSyntax constructor)
        {
            var output = constructor.Identifier.Text;
            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetName_Simple(MethodDeclarationSyntax)"/> as the default.
        /// </summary>
        public static string GetName(this ConstructorDeclarationSyntax constructor)
        {
            var output = constructor.Identifier.Text;
            return output;
        }

        /// <inheritdoc cref="BaseMethodDeclarationSyntaxExtensions.GetParameterTypedMethodName(BaseMethodDeclarationSyntax, string)"/>
        public static string GetParameterTypedMethodName(this ConstructorDeclarationSyntax constructor)
        {
            var simpleMethodName = constructor.GetName_Simple();

            var output = constructor.GetParameterTypedMethodName(
                simpleMethodName);

            return output;
        }
    }
}
