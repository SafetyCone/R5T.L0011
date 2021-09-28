using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.L0011.T003;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class MemberDeclarationSyntaxExtensions
    {
        public static T ApplyMemberSignatureModel<T>(this T member, MemberSignatureModel signatureModel)
            where T : MemberDeclarationSyntax
        {
            var modifiedMember = member;

            modifiedMember = signatureModel.AccessibilityLevel switch
            {
                AccessibilityLevel.Public => modifiedMember.MakePublic(),
                _ => throw EnumerationHelper.UnexpectedEnumerationValueException(signatureModel.AccessibilityLevel),
            };

            return modifiedMember;
        }

        /// <summary>
        /// The <paramref name="attributeTypeName"/> value might included the "-Attribute" suffix, or it may not.
        /// This method tests for the existence of both forms.
        /// </summary>
        public static bool HasAttributeOfTypeSuffixedOrUnsuffixed<TMemberDeclarationSyntax>(this TMemberDeclarationSyntax member,
            string attributeTypeName)
            where TMemberDeclarationSyntax : MemberDeclarationSyntax
        {
            var attributeSuffixedAttributeTypeName = Instances.AttributeTypeName.GetEnsuredAttributeSuffixedTypeName(attributeTypeName);
            var hasAttributeSuffixedAttributeTypeName = member.HasAttributeOfTypeSimple(attributeSuffixedAttributeTypeName);
            if (hasAttributeSuffixedAttributeTypeName)
            {
                // If the interface has the attribute-suffixed attribute type name, we are done.
                return hasAttributeSuffixedAttributeTypeName;
            }

            var nonAttributeSuffixedAttributeTypeName = Instances.AttributeTypeName.GetEnsuredNonAttributeSuffixedTypeName(attributeTypeName);
            var hasNonAttributeSuffixedAttributeTypeName = member.HasAttributeOfTypeSimple(nonAttributeSuffixedAttributeTypeName);

            // At this point, we have already tested the attribute-suffixed attribute type name, so the interface either has the non-attribute-suffixed type name or it doesn't have the attribute.
            return hasNonAttributeSuffixedAttributeTypeName;
        }

        /// <summary>
        /// Selects <see cref="HasAttributeOfTypeSuffixedOrUnsuffixed{TMemberDeclarationSyntax}(TMemberDeclarationSyntax, string)"/> as the default.
        /// </summary>
        public static bool HasAttributeOfType<TMemberDeclarationSyntax>(this TMemberDeclarationSyntax @interface,
            string attributeTypeName)
            where TMemberDeclarationSyntax : MemberDeclarationSyntax
        {
            var output = @interface.HasAttributeOfTypeSuffixedOrUnsuffixed(attributeTypeName);
            return output;
        }
    }
}
