using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class ParameterSyntaxExtensions
    {
        /// <summary>
        /// The <paramref name="attributeTypeName"/> value might included the "-Attribute" suffix, or it may not.
        /// This method tests for the existence of both forms.
        /// </summary>
        public static bool HasAttributeOfTypeSuffixedOrUnsuffixed(this ParameterSyntax parameter,
            string attributeTypeName)
        {
            var attributeSuffixedAttributeTypeName = Instances.AttributeTypeName.GetEnsuredAttributeSuffixedTypeName(attributeTypeName);
            var hasAttributeSuffixedAttributeTypeName = parameter.HasAttributeOfTypeSimple(attributeSuffixedAttributeTypeName);
            if (hasAttributeSuffixedAttributeTypeName)
            {
                // If the interface has the attribute-suffixed attribute type name, we are done.
                return hasAttributeSuffixedAttributeTypeName;
            }

            var nonAttributeSuffixedAttributeTypeName = Instances.AttributeTypeName.GetEnsuredNonAttributeSuffixedTypeName(attributeTypeName);
            var hasNonAttributeSuffixedAttributeTypeName = parameter.HasAttributeOfTypeSimple(nonAttributeSuffixedAttributeTypeName);

            // At this point, we have already tested the attribute-suffixed attribute type name, so the interface either has the non-attribute-suffixed type name or it doesn't have the attribute.
            return hasNonAttributeSuffixedAttributeTypeName;
        }

        /// <summary>
        /// Selects <see cref="HasAttributeOfTypeSuffixedOrUnsuffixed(ClassDeclarationSyntax, string)"/> as the default.
        /// </summary>
        public static bool HasAttributeOfType(this ParameterSyntax parameter,
            string attributeTypeName)
        {
            var output = parameter.HasAttributeOfTypeSuffixedOrUnsuffixed(attributeTypeName);
            return output;
        }

        public static bool HasAttributeOfType<T>(this ParameterSyntax parameter)
            where T : Attribute
        {
            // The type name could be -Attribute or not without worry, since the -SuffixedOrUnsuffixed handles that.
            var attributeTypeName = typeof(T).Name;

            var output = parameter.HasAttributeOfTypeSuffixedOrUnsuffixed(attributeTypeName);
            return output;
        }

        public static string ToStringStandard(this ParameterSyntax parameter)
        {
            var isExtensionParameter = parameter.IsExtensionParameter();

            var extensionSignifier = isExtensionParameter
                ? $"{Instances.Syntax.This()} "
                : Strings.Empty
                ;

            var output = $"{extensionSignifier}{parameter.Type} {parameter.Identifier}";
            return output;
        }
    }
}
