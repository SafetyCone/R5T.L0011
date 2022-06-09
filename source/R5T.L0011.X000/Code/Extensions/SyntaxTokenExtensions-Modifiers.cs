using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;



namespace System
{
    public static partial class SyntaxTokenExtensions
    {
        #region Simple Is() Methods

        public static bool IsAbstract(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.AbstractKeyword);
            return output;
        }

        public static bool IsAsync(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.AsyncKeyword);
            return output;
        }

        public static bool IsConst(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.ConstKeyword);
            return output;
        }

        public static bool IsExtern(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.ExternKeyword);
            return output;
        }

        public static bool IsInternal(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.InternalKeyword);
            return output;
        }

        public static bool IsOverride(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.OverrideKeyword);
            return output;
        }

        public static bool IsPrivate(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.PrivateKeyword);
            return output;
        }

        public static bool IsPartial(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.PartialKeyword);
            return output;
        }

        public static bool IsProtected(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.ProtectedKeyword);
            return output;
        }

        public static bool IsPublic(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.PublicKeyword);
            return output;
        }

        public static bool IsReadOnly(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.ReadOnlyKeyword);
            return output;
        }

        public static bool IsSealed(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.SealedKeyword);
            return output;
        }

        public static bool IsStatic(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.StaticKeyword);
            return output;
        }

        public static bool IsUnsafe(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.UnsafeKeyword);
            return output;
        }

        public static bool IsVirtual(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.VirtualKeyword);
            return output;
        }

        public static bool IsVolatile(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.VolatileKeyword);
            return output;
        }

        #endregion

        #region Composite Is() Methods

        public static bool IsAccessModifier(this SyntaxToken token)
        {
            var output = false
                || token.IsPublic()
                || token.IsProtected()
                || token.IsInternal()
                || token.IsPrivate();

            return output;
        }

        public static bool IsNotAccessModifier(this SyntaxToken token)
        {
            var isAccessModifier = token.IsAccessModifier();

            var output = !isAccessModifier;
            return output;
        }

        #endregion
    }
}


namespace System.Linq
{
    public static partial class SyntaxTokenExtensions
    {
        /// <summary>
        /// Returns the index of the last access modifer, or <see cref="IndexHelper.NotFound"/> if no access modifier is found.
        /// </summary>
        public static int GetIndexOfLastAccessModifier(this IEnumerable<SyntaxToken> modifiers)
        {
            var output = modifiers.LastIndexWhere(x => x.IsAccessModifier());
            return output;
        }

        /// <summary>
        /// Returns the first index at which the static modifier could be inserted.
        /// The static modifier must come after any accessiblity modifiers.
        /// </summary>
        public static int GetFirstIndexAvailableForStaticModifier(this IEnumerable<SyntaxToken> modifiers)
        {
            // First find the position at which to add the static modifier, which is after the last access modifier.
            var indexOfLastModifier = modifiers.GetIndexOfLastAccessModifier();
            var indexOfStaticToken = indexOfLastModifier + 1;

            return indexOfStaticToken;
        }

        /// <summary>
        /// Returns one past the last index of modifiers, since the partial keyword must come last.
        /// </summary>
        public static int GetIndexForPartialModifier(this IReadOnlyCollection<SyntaxToken> modifiers)
        {
            var output = modifiers.GetIndexForInsertionAtEnd();
            return output;
        }
    }
}
