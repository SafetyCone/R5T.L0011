using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;

using SyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static class ISyntaxFactoryExtensions
    {
        public static IdentifierNameSyntax IdentifierName(this ISyntaxFactory _,
            string text)
        {
            return SyntaxFactory.IdentifierName(text);
        }

        public static IdentifierNameSyntax IdentifierName(this ISyntaxFactory _,
            SyntaxToken identifier)
        {
            return SyntaxFactory.IdentifierName(identifier);
        }

        public static IdentifierNameSyntax Name(this ISyntaxFactory _,
            string name)
        {
            var output = _.IdentifierName(name);
            return output;
        }

        public static NameEqualsSyntax NameEquals(this ISyntaxFactory _,
            string destinationName)
        {
            var output = SyntaxFactory.NameEquals(destinationName);
            return output;
        }

        public static UsingDirectiveSyntax Using_WithoutLeadingNewLine(this ISyntaxFactory _,
            NameSyntax name)
        {
            var output = SyntaxFactory.UsingDirective(name)
                .NormalizeWhitespace();

            return output;
        }

        public static UsingDirectiveSyntax Using_WithoutLeadingNewLine(this ISyntaxFactory _,
            string namespaceName)
        {
            var name = _.Name(namespaceName);

            var output = _.Using_WithoutLeadingNewLine(name);
            return output;
        }

        public static UsingDirectiveSyntax Using_WithoutLeadingNewLine(this ISyntaxFactory _,
            string destinationName,
            string sourceNameExpression)
        {
            var nameEqualsSyntax = _.NameEquals(destinationName);

            var nameSyntax = _.Name(sourceNameExpression);

            var output = SyntaxFactory.UsingDirective(nameEqualsSyntax, nameSyntax)
                .NormalizeWhitespace();

            return output;
        }
    }
}