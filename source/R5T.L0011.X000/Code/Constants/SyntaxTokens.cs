using System;


namespace Microsoft.CodeAnalysis.CSharp
{
    public static partial class SyntaxTokens
    {
        public static SyntaxToken Semicolon()
        {
            var output = SyntaxFactory.Token(SyntaxKind.SemicolonToken);
            return output;
        }

        public static SyntaxToken Void()
        {
            var output = SyntaxFactory.Token(SyntaxKind.VoidKeyword);
            return output;
        }
    }
}
