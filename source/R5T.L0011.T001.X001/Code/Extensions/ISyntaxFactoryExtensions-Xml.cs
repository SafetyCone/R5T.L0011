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
        public static XmlCrefAttributeSyntax XmlCrefAttribute(this ISyntaxFactory syntaxFactory, string name)
        {
            var output = SyntaxFactory.XmlCrefAttribute(syntaxFactory.NameMemberCref(name));
            return output;
        }

        public static XmlElementEndTagSyntax XmlElementEndTag(this ISyntaxFactory syntaxFactory, string tag)
        {
            var output = SyntaxFactory.XmlElementEndTag(
                syntaxFactory.XmlName(tag));

            return output;
        }

        public static XmlElementStartTagSyntax XmlElementStartTag(this ISyntaxFactory syntaxFactory, string tag)
        {
            var output = SyntaxFactory.XmlElementStartTag(
                syntaxFactory.XmlName(tag));

            return output;
        }

        public static XmlElementSyntax XmlExampleElement(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.XmlExampleElement();
            return output;
        }

        public static XmlNameSyntax XmlName(this ISyntaxFactory _, string name)
        {
            var output = SyntaxFactory.XmlName(name);
            return output;
        }

        public static XmlEmptyElementSyntax XmlSee(this ISyntaxFactory syntaxFactory, string name)
        {
            var output = SyntaxFactory.XmlNullKeywordElement()
                .AddAttributes(
                    syntaxFactory.XmlCrefAttribute(name));

            return output;
        }

        public static XmlElementSyntax XmlSummaryElementOnly(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.XmlSummaryElement();
            return output;
        }

        public static XmlTextSyntax XmlText(this ISyntaxFactory syntaxFactory, string text)
        {
            var output = SyntaxFactory.XmlText()
                .AddTextTokens(syntaxFactory.XmlTextLiteral(text))
                ;

            return output;
        }

        public static SyntaxToken XmlTextLiteral(this ISyntaxFactory _, string text)
        {
            var output = SyntaxFactory.XmlTextLiteral(text);
            return output;
        }

        public static XmlTextSyntax XmlTextNewLine(this ISyntaxFactory syntaxFactory)
        {
            var output = SyntaxFactory.XmlText(
                syntaxFactory.XmlTextNewLineOnly());

            return output;
        }

        public static SyntaxToken XmlTextNewLineOnly(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.XmlTextNewLine(Environment.NewLine);
            return output;
        }

        public static XmlTextSyntax XmlTextOnly(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.XmlText();
            return output;
        }

        public static XmlTextSyntax XmlTextOnly(this ISyntaxFactory _, string name)
        {
            var output = SyntaxFactory.XmlText(name);
            return output;
        }
    }
}
