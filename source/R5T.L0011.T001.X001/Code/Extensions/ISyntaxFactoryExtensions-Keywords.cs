using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;

using SyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        #region Modifiers

        public static SyntaxToken Abstract(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Abstract();
            return output;
        }

        public static SyntaxToken Async(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Async();
            return output;
        }

        public static SyntaxToken Const(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Const();
            return output;
        }

        public static SyntaxToken Extern(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Extern();
            return output;
        }

        public static SyntaxToken Internal(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Internal();
            return output;
        }

        public static SyntaxToken Override(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Override();
            return output;
        }

        public static SyntaxToken Private(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Private();
            return output;
        }

        public static SyntaxToken Protected(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Protected();
            return output;
        }

        public static SyntaxToken Public(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Public();
            return output;
        }

        public static SyntaxToken ReadOnly(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.ReadOnly();
            return output;
        }

        public static SyntaxToken Sealed(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Sealed();
            return output;
        }

        public static SyntaxToken Static(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Static();
            return output;
        }

        public static SyntaxToken Unsafe(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Unsafe();
            return output;
        }

        public static SyntaxToken Virtual(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Virtual();
            return output;
        }

        public static SyntaxToken Volatile(this ISyntaxFactory _)
        {
            var output = SyntaxTokens.Volatile();
            return output;
        }

        #endregion


        public static AccessorDeclarationSyntax Get(this ISyntaxFactory _)
        {
            return SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration);
        }
    }
}
