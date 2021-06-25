using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;

using SyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        public static DocumentationCommentTriviaSyntax DocumentationComment(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.DocumentationCommentOnly()
                .WithEndOfComment(syntaxFactory.EndOfDocumentationCommentEmptyToken());

            return output;
        }

        public static DocumentationCommentTriviaSyntax DocumentationCommentOnly(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.DocumentationComment();
            return output;
        }

        public static SyntaxTrivia DocumentationCommentExterior(this ISyntaxFactory _, string text)
        {
            var output = SyntaxFactory.DocumentationCommentExterior(text);
            return output;
        }

        public static NameMemberCrefSyntax NameMemberCref(this ISyntaxFactory syntaxFactory, string typeName)
        {
            var output = SyntaxFactory.NameMemberCref(syntaxFactory.Type(typeName));
            return output;
        }
    }
}
