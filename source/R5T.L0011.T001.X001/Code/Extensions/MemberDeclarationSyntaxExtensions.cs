using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;


namespace System
{
    public static class MemberDeclarationSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static T MakePrivate<T>(this T member)
            where T : MemberDeclarationSyntax
        {
            return member
                .AddModifiers(SyntaxFactory.Private())
                as T;
        }

        public static T MakePublic<T>(this T member)
            where T : MemberDeclarationSyntax
        {
            return member
                .AddModifiers(SyntaxFactory.Public())
                as T;
        }

        public static T MakeStatic_Simple<T>(this T member)
            where T : MemberDeclarationSyntax
        {
            return member.AddModifiers(SyntaxFactory.Static()) as T;
        }
    }
}
