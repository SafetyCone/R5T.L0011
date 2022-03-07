using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class PropertyDeclarationSyntaxExtensions
    {
        public static string GetInitializationExpressionText(this PropertyDeclarationSyntax property)
        {
            var output = property.Initializer.Value.ToString();
            return output;
        }

        /// <summary>
        /// Returns empty if the property does not have an initializer.
        /// </summary>
        public static string GetInitializationExpressionTextOrEmpty(this PropertyDeclarationSyntax property)
        {
            var hasInitializer = property.HasInitializer();
            if(!hasInitializer)
            {
                return Strings.Empty;
            }

            var output = property.GetInitializationExpressionText();
            return output;
        }

        public static string GetIdentifierText(this PropertyDeclarationSyntax property)
        {
            var output = property.Identifier.ValueText;
            return output;
        }

        public static string GetTypeExpressionText(this PropertyDeclarationSyntax property)
        {
            var output = property.Type.ToString();
            return output;
        }

        public static bool HasInitializer(this PropertyDeclarationSyntax property)
        {
            var output = property.Initializer is object;
            return output;
        }
    }
}
