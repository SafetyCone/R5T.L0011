using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using R5T.L0011.T001;

using CSharpSyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        //public static SyntaxToken EmptyToken(this ISyntaxFactory syntaxFactory)
        //{
        //    //var output = CSharpSyntaxFactory.Token()
        //    return output;
        //}

        public static SyntaxToken EndOfDocumentationCommentEmptyToken(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.Token(SyntaxKind.EndOfDocumentationCommentToken);
            return output;
        }

        public static SyntaxToken Semicolon(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.Token(SyntaxKind.SemicolonToken);
            return output;
        }

        public static SyntaxToken ThisToken(this ISyntaxFactory _)
        {
            return CSharpSyntaxFactory.Token(SyntaxKind.ThisKeyword);
        }
    }
}
