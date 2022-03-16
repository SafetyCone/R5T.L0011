using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class MemberDeclarationSyntaxExtensions
    {
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
