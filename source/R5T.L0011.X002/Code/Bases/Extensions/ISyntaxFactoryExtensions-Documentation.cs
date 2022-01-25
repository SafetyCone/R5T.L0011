using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;
using R5T.L0011.T002;

using CSharpSyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        public static SyntaxTrivia DocumentationCommentExteriorOnly(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.DocumentationCommentExterior(Syntax.DocumentationCommentExterior());
            return output;
        }

        public static XmlTextSyntax DocumentationCommentExterior(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.XmlTextOnly()
                .AddTextTokens(
                    syntaxFactory.XmlTextLiteral(Strings.Space)
                        .WithLeadingTrivia(
                            syntaxFactory.DocumentationCommentExteriorOnly()));

            return output;
        }

        //public static XmlTextSyntax DocumentationLineEnd(this ISyntaxFactory syntaxFactory)
        //{
        //    var output = syntaxFactory.DocumentationLineStart(); // Seems to be the same?
        //    return output;
        //}

        public static XmlTextSyntax DocumentationLineStart(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList leadingWhitespace)
        {
            var output = syntaxFactory.DocumentationCommentExterior()
                .AddLineStart2(leadingWhitespace);

            return output;
        }

        public static XmlTextSyntax DocumentationLine(this ISyntaxFactory syntaxFactory, SyntaxTriviaList leadingWhitespace,
            string text)
        {
            var output = syntaxFactory.XmlText(text)
                .AddLineStart(leadingWhitespace)
                .AddTrailingLeadingTrivia(
                    syntaxFactory.DocumentationCommentExteriorOnly(),
                    syntaxFactory.Space())
                ;

            return output;
        }
    }
}
