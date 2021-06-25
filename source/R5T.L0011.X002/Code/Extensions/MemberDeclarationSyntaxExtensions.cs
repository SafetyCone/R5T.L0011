using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.L0011.T003;


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
    }
}
