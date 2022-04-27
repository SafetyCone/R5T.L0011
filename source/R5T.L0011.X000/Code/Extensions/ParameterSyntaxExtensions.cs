using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class ParameterSyntaxExtensions
    {
        public static IEnumerable<AttributeSyntax> GetAttributes(this ParameterSyntax parameter)
        {
            var output = parameter.AttributeLists
                .SelectMany(xAttributeList => xAttributeList.Attributes)
                ;

            return output;
        }

        /// <summary>
        /// Simply check whether an attribute with the specified type name exists.
        /// (Does not check for X vs. XAttribute varieties of attribute type name.)
        /// </summary>
        public static bool HasAttributeOfTypeSimple(this ParameterSyntax parameter,
            string attributeTypeName)
        {
            var output = parameter.GetAttributes()
                .Where(xAttribute => xAttribute.NameIs(attributeTypeName))
                .Any();

            return output;
        }

        public static string GetName(this ParameterSyntax parameter)
        {
            var output = parameter.Identifier.GetText();
            return output;
        }

        public static string GetTypeName(this ParameterSyntax parameter)
        {
            var output = parameter.Type.ToString();
            return output;
        }

        public static bool IsExtensionParameter(this ParameterSyntax parameter)
        {
            var output = parameter.Modifiers
                .Where(xModifier => xModifier.IsKind(SyntaxKind.ThisKeyword))
                .Any();

            return output;
        }

        public static WasFound<AttributeSyntax[]> HasAttributes(this ParameterSyntax parameter)
        {
            var output = parameter.GetAttributes().Now();

            return WasFound.FromArray(output);
        }
    }
}
