using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;

using CSharpSyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static class XmlElementSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static XmlElementSyntax AddTagLineStarts(this XmlElementSyntax xmlElement,
            SyntaxTriviaList leadingWhitespace)
        {
            var output = xmlElement
                .WithStartTag(
                    xmlElement.StartTag
                        .AddLineStart(leadingWhitespace)
                        .AddTrailingLeadingTrivia(
                            SyntaxFactory.DocumentationCommentExteriorOnly(),
                            SyntaxFactory.Space()))
                .WithEndTag(
                    xmlElement.EndTag
                        .AddLineStart(leadingWhitespace)
                        .AddTrailingLeadingTrivia(
                            SyntaxFactory.DocumentationCommentExteriorOnly(),
                            SyntaxFactory.Space()))
                ;

            return output;
        }

        public static XmlElementSyntax AddContentLine(this XmlElementSyntax xmlElement,
            SyntaxTriviaList leadingWhitespace,
            string text)
        {
            var documentationLineElements = SyntaxFactory.ParseDocumentationLine(text);

            // If any elements, prepend the line leading whitespace.
            if(documentationLineElements.Any())
            {
                documentationLineElements[0] = documentationLineElements[0]
                .AddLineStart(leadingWhitespace);
            }

            var output = xmlElement.AddContent(documentationLineElements);
            return output;
        }
    }
}
