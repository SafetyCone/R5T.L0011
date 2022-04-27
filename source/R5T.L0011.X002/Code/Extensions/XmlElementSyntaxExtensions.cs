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


        public static XmlElementSyntax AddTagLineStarts(this XmlElementSyntax xmlElement)
        {
            var output = xmlElement
                .WithStartTag(
                    xmlElement.StartTag
                        .AddTrailingLeadingTrivia(
                            SyntaxFactory.DocumentationCommentExteriorOnly(),
                            SyntaxFactory.Space()))
                .WithEndTag(
                    xmlElement.EndTag
                        .AddTrailingLeadingTrivia(
                            SyntaxFactory.DocumentationCommentExteriorOnly(),
                            SyntaxFactory.Space()))
                ;

            return output;
        }

        public static XmlElementSyntax AddTagLineStarts(this XmlElementSyntax xmlElement,
            SyntaxTriviaList indentation)
        {
            var output = xmlElement
                .WithStartTag(
                    xmlElement.StartTag
                        .AddLineStart(indentation)
                        .AddTrailingLeadingTrivia(
                            SyntaxFactory.DocumentationCommentExteriorOnly(),
                            SyntaxFactory.Space()))
                .WithEndTag(
                    xmlElement.EndTag
                        .AddLineStart(indentation)
                        .AddTrailingLeadingTrivia(
                            SyntaxFactory.DocumentationCommentExteriorOnly(),
                            SyntaxFactory.Space()))
                ;

            return output;
        }

        public static XmlElementSyntax AddContentLine(this XmlElementSyntax xmlElement,
            string documenationLine)
        {
            var documentationLineElements = SyntaxFactory.ParseDocumentationLine(documenationLine);

            var output = xmlElement.AddContent(documentationLineElements);
            return output;
        }

        public static XmlElementSyntax AddContentLine(this XmlElementSyntax xmlElement,
            SyntaxTriviaList indentation,
            string documenationLine)
        {
            var documentationLineElements = SyntaxFactory.ParseDocumentationLine(documenationLine);

            // If any elements, prepend the line leading whitespace.
            if(documentationLineElements.Any())
            {
                documentationLineElements[0] = documentationLineElements[0]
                .AddLineStart(indentation);
            }

            var output = xmlElement.AddContent(documentationLineElements);
            return output;
        }
    }
}
