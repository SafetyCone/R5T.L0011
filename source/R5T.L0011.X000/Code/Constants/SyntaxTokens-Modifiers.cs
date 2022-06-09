using System;


namespace Microsoft.CodeAnalysis.CSharp
{
    public static partial class SyntaxTokens
    {
        public static SyntaxToken Abstract()
        {
            var output = SyntaxFactory.Token(SyntaxKind.AbstractKeyword);
            return output;
        }

        public static SyntaxToken Async()
        {
            var output = SyntaxFactory.Token(SyntaxKind.AsyncKeyword);
            return output;
        }

        public static SyntaxToken Const()
        {
            var output = SyntaxFactory.Token(SyntaxKind.ConstKeyword);
            return output;
        }

        public static SyntaxToken Extern()
        {
            var output = SyntaxFactory.Token(SyntaxKind.ExternKeyword);
            return output;
        }

        public static SyntaxToken Internal()
        {
            var output = SyntaxFactory.Token(SyntaxKind.InternalKeyword);
            return output;
        }

        public static SyntaxToken Override()
        {
            var output = SyntaxFactory.Token(SyntaxKind.OverrideKeyword);
            return output;
        }

        public static SyntaxToken Partial()
        {
            var output = SyntaxFactory.Token(SyntaxKind.PartialKeyword);
            return output;
        }

        public static SyntaxToken Private()
        {
            var output = SyntaxFactory.Token(SyntaxKind.PrivateKeyword);
            return output;
        }

        public static SyntaxToken Protected()
        {
            var output = SyntaxFactory.Token(SyntaxKind.ProtectedKeyword);
            return output;
        }

        public static SyntaxToken Public()
        {
            var output = SyntaxFactory.Token(SyntaxKind.PublicKeyword);
            return output;
        }

        public static SyntaxToken ReadOnly()
        {
            var output = SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword);
            return output;
        }

        public static SyntaxToken Sealed()
        {
            var output = SyntaxFactory.Token(SyntaxKind.SealedKeyword);
            return output;
        }

        public static SyntaxToken Static()
        {
            var output = SyntaxFactory.Token(SyntaxKind.StaticKeyword);
            return output;
        }

        public static SyntaxToken Unsafe()
        {
            var output = SyntaxFactory.Token(SyntaxKind.UnsafeKeyword);
            return output;
        }

        public static SyntaxToken Virtual()
        {
            var output = SyntaxFactory.Token(SyntaxKind.VirtualKeyword);
            return output;
        }

        public static SyntaxToken Volatile()
        {
            var output = SyntaxFactory.Token(SyntaxKind.VolatileKeyword);
            return output;
        }
    }
}
