using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class MemberDeclarationSyntaxExtensions
    {
        public static WasFound<AttributeSyntax[]> HasAttributes(this MemberDeclarationSyntax member)
        {
            var attributes = member.AttributeLists
                .SelectMany(x => x.Attributes)
                .Now();

            var output = WasFound.FromArray(attributes);
            return output;
        }

        public static T WithoutAttributeLists<T>(this T memberDeclaration)
            where T : MemberDeclarationSyntax
        {
            var emptyAttributeListList = new SyntaxList<AttributeListSyntax>();

            var output = memberDeclaration.WithAttributeLists(emptyAttributeListList) as T;
            return output;
        }

        public static T WithoutModifiers<T>(this T memberDeclaration)
            where T : MemberDeclarationSyntax
        {
            var emptySyntaxTokenList = new SyntaxTokenList();

            var output = memberDeclaration.WithModifiers(emptySyntaxTokenList) as T;
            return output;
        }
    }
}
