using System;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace R5T.L0011.X000.Generation.Initial.Parse
{
    public static partial class SyntaxParser
    {
        public static PropertyDeclarationSyntax ParseProperty_Simple(string text)
        {
            var output = SyntaxFactory.ParseMemberDeclaration(text) as PropertyDeclarationSyntax;
            return output;
        }
    }
}
