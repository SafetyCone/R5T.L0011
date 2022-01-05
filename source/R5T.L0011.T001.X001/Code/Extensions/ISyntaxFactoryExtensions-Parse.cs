using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;

using SyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        public static ConstructorDeclarationSyntax ParseConstructorDeclaration(this ISyntaxFactory _,
            string text)
        {
            var output = SyntaxFactory.ParseMemberDeclaration(text) as ConstructorDeclarationSyntax;
            return output;
        }

        public static ClassDeclarationSyntax ParseClassDeclaration(this ISyntaxFactory _,
            string text)
        {
            var output = SyntaxFactory.ParseMemberDeclaration(text) as ClassDeclarationSyntax;
            return output;
        }

        public static XmlNodeSyntax[] ParseDocumentationLine(this ISyntaxFactory _,
            string text)
        {
            var syntaxTree = SyntaxFactory.ParseSyntaxTree(text);

            var firstTrivia = syntaxTree
                .GetRoot()
                .DescendantTrivia().First();

            var structure = firstTrivia.GetStructure();

            var xmlElements = structure.ChildNodes()
                .Cast<XmlNodeSyntax>()
                .ToArray();

            return xmlElements;
        }

        public static InterfaceDeclarationSyntax ParseInterfaceDeclaration(this ISyntaxFactory _,
            string text)
        {
            var output = SyntaxFactory.ParseMemberDeclaration(text) as InterfaceDeclarationSyntax;
            return output;
        }

        public static MethodDeclarationSyntax ParseMethodDeclaration(this ISyntaxFactory _,
            string text)
        {
            var output = SyntaxFactory.ParseMemberDeclaration(text) as MethodDeclarationSyntax;

            output.VerifyNonNull("Failed to parse method declaration.");

            return output;
        }

        public static ParameterSyntax ParseParameter(this ISyntaxFactory _,
            string text)
        {
            var output = SyntaxFactory.ParseParameterList(text)
                .Parameters.Single(); // Throw if more or less than one.

            return output;
        }

        public static PropertyDeclarationSyntax ParsePropertyDeclaration(this ISyntaxFactory _,
            string text)
        {
            var output = SyntaxFactory.ParseMemberDeclaration(text) as PropertyDeclarationSyntax;
            return output;
        }

        //public static TypeParameterConstraintClauseSyntax ParseConstraint(this ISyntaxFactory _,
        //    string text)
        //{
        //    var temp = SyntaxFactory.ParseSyntaxTree(text);

        //    var output = SyntaxFactory.ParseSyntaxTree(text).GetRoot() as TypeParameterConstraintClauseSyntax;
        //    return output;
        //}

        public static ReturnStatementSyntax ParseReturnStatement(this ISyntaxFactory _,
            string text)
        {
            var output = SyntaxFactory.ParseStatement(text) as ReturnStatementSyntax;
            return output;
        }

        public static StatementSyntax ParseStatement(this ISyntaxFactory _,
            string text)
        {
            var output = SyntaxFactory.ParseStatement(text);
            return output;
        }

        public static UsingDirectiveSyntax ParseUsingDirective(this ISyntaxFactory _,
            string text)
        {
            var output = SyntaxFactory.ParseCompilationUnit(text)
                .DescendantNodes()
                .OfType<UsingDirectiveSyntax>()
                .First();

            return output;
        }
    }
}
