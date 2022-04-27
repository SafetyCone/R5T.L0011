using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class TypeSyntaxExtensions
    {
        /// <summary>
        /// Chooses <see cref="GetTypeName_Simple(TypeSyntax)"/> as the default.
        /// </summary>
        public static string GetTypeName(this TypeSyntax typeSyntax)
        {
            var output = typeSyntax.GetTypeName_Simple();
            return output;
        }

        /// <summary>
        /// Same as <see cref="GetTypeName(TypeSyntax)"/>.
        /// </summary>
        public static string GetTypeNameFragment(this TypeSyntax typeSyntax)
        {
            var output = typeSyntax.GetTypeName();
            return output;
        }

        /// <summary>
        /// Simply gets the string results of the type name.
        /// </summary>
        public static string GetTypeName_Simple(this TypeSyntax typeSyntax)
        {
            var output = typeSyntax.ToString();
            return output;
        }

        public static string GetTypeName_HandlingTypeParameters(this TypeSyntax typeSyntax)
        {
            var isGeneric = typeSyntax.IsGeneric();
            if(isGeneric)
            {
                var output = isGeneric.Result.GetTypeName_HandlingTypeParameters();
                return output;
            }
            else
            {
                var output = typeSyntax.GetTypeName_Simple();
                return output;
            }
        }

        public static WasFound<GenericNameSyntax> IsGeneric(this TypeSyntax typeSyntax)
        {
            var genericNameOrDefault = typeSyntax as GenericNameSyntax;

            var output = WasFound.From(genericNameOrDefault);
            return output;
        }

        public static bool IsNamed(this TypeSyntax typeSyntax,
            string typeName)
        {
            var typeNameFromSyntax = typeSyntax.GetTypeName();

            var output = typeNameFromSyntax == typeName;
            return output;
        }
    }
}
