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
        public static AccessorDeclarationSyntax Get(this ISyntaxFactory _)
        {
            return SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration);
        }

        public static SyntaxToken Private(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.Token(SyntaxKind.PrivateKeyword);
            return output;
        }

        public static SyntaxToken Public(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.Token(SyntaxKind.PublicKeyword);
            return output;
        }

        public static SyntaxToken Static(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.Token(SyntaxKind.StaticKeyword);
            return output;
        }
    }
}
