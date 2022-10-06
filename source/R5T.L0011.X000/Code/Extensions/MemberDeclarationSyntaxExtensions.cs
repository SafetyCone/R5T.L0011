using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class MemberDeclarationSyntaxExtensions
    {
        #region Modifiers

        public static WasFound<SyntaxToken> HasPartialModifier(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsPartial());
            return output;
        }

        public static WasFound<SyntaxToken> HasStaticModifier(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsStatic());
            return output;
        }

        public static bool IsAbstract(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsAbstract());
            return output;
        }

        public static bool IsAsync(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsAsync());
            return output;
        }

        public static bool IsConst(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsConst());
            return output;
        }

        public static bool IsExtern(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsExtern());
            return output;
        }

        public static bool IsInternal(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsInternal());
            return output;
        }

        public static bool IsOverride(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsOverride());
            return output;
        }

        public static bool IsPrivate(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsPrivate());
            return output;
        }

        public static bool IsPartial(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsPartial());
            return output;
        }

        public static bool IsProtected(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsProtected());
            return output;
        }

        public static bool IsPublic(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsPublic());
            return output;
        }

        public static bool IsReadOnly(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsReadOnly());
            return output;
        }

        public static bool IsSealed(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsSealed());
            return output;
        }

        public static bool IsStatic(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_Boolean(x => x.IsStatic());
            return output;
        }

        public static bool IsUnsafe(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsUnsafe());
            return output;
        }

        public static bool IsVirtual(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsVirtual());
            return output;
        }

        public static bool IsVolatile(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsVolatile());
            return output;
        }

        #endregion

        public static SyntaxTokenList GetModifiers(this MemberDeclarationSyntax member)
        {
            return member.Modifiers;
        }

        public static IEnumerable<SyntaxToken> GetModifiers_Enumerable(this MemberDeclarationSyntax member)
        {
            return member.Modifiers;
        }

        public static WasFound<AttributeSyntax[]> HasAttributes(this MemberDeclarationSyntax member)
        {
            var attributes = member.AttributeLists
                .SelectMany(x => x.Attributes)
                .ToArray();

            var output = WasFound.FromArray(attributes);
            return output;
        }

        public static WasFound<SyntaxToken> HasModifier(this MemberDeclarationSyntax member,
            Func<SyntaxToken, bool> predicate)
        {
            // Use first (not single) to allow the predicate to select multiple modifiers.
            // In general, the predicate will only be used to select a single modifier (since the return type is only a single syntax token). But we stil

            // Use single (not first) even though the predicate could be used to select multiple modifier syntax tokens.
            // Since the return type is only a single syntax token, choose correctness over robustness to keep with the assumption of only a single syntax token.
            var singleOrDefault = member.GetModifiers_Enumerable()
                .Where(predicate)
                .SingleOrDefault();

            var output = WasFound.From(singleOrDefault);
            return output;
        }

        public static bool HasModifier_Boolean(this MemberDeclarationSyntax member,
            Func<SyntaxToken, bool> predicate)
        {
            var output = member.GetModifiers_Enumerable()
                .Where(predicate)
                .Any();

            return output;
        }

        public static bool HasModifiers(this MemberDeclarationSyntax member,
            IEnumerable<SyntaxToken> modifiers)
        {
            // If none of the input modifiers remain after removing all modifiers on the member, then the member has all the input modifiers.
            var output = modifiers.Except(
                    member.GetModifiers_Enumerable())
                .None_OLD();    

            return output;
        }

        public static IEnumerable<SyntaxToken> HasModifiers(this MemberDeclarationSyntax member,
            Func<SyntaxToken, bool> predicate)
        {
            var output = member.GetModifiers_Enumerable()
                .Where(predicate)
                ;

            return output;
        }

        /// <summary>
        /// Modifies the modifiers of the member.
        /// </summary>
        /// <remarks>
        /// Cannot use a typed annotation since syntax token lists do not have annotations.
        /// </remarks>
        public static T ModifyModifiers<T>(this T member,
            Func<SyntaxTokenList, SyntaxTokenList> modifersModifier)
            where T : MemberDeclarationSyntax
        {
            var modifiers = member.GetModifiers();

            var modifiedModifiers = modifersModifier(modifiers);

            member = member.WithModifiers(modifiedModifiers) as T;

            return member;
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
