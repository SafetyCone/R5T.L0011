using System;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.L0011.T001;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class SyntaxTokenExtensions
    {
        public static SyntaxToken AppendComment(this SyntaxToken syntaxToken, string text)
        {
            var comment = Instances.SyntaxFactory.Comment(text);

            var output = syntaxToken.AddTrailingTrivia(comment);
            return output;
        }
    }
}
