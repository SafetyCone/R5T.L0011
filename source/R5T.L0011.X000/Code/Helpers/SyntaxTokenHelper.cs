using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace R5T.L0011.X000
{
    public static class SyntaxTokenHelper
    {
        public static SyntaxToken GetVoid()
        {
            var output = SyntaxFactory.Token(SyntaxKind.VoidKeyword);
            return output;
        }
    }
}
