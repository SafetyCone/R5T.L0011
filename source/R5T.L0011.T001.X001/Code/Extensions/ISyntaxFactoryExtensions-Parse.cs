using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Note, this is a wrapper around <see cref="SyntaxFactory.ParseMemberDeclaration(string, int, Microsoft.CodeAnalysis.ParseOptions?, bool)"/>.
        /// As a result, it produces method body blocks with open braces that include a trailing new line.
        /// This is non-standard.
        /// </summary>
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

        /// <summary>
        /// Parses text containing a statement.
        /// <example-text-input>Example text input:
        /// <code>
        /// var executionSynchronicityProviderAction = Instances.ServiceAction.AddConstructorBasedExecutionSynchronicityProviderAction(Synchronicity.Synchronous);
        /// </code>
        /// </example-text-input>
        /// </summary>
        public static StatementSyntax ParseStatement(this ISyntaxFactory _,
            string text)
        {
            var output = SyntaxFactory.ParseStatement(text);
            return output;
        }

        /// <summary>
        /// Parses multiple statements using the single statement parsing functionality.
        /// <inheritdoc cref="ParseStatement(ISyntaxFactory, string)" path="/summary/example-text-input"/>
        /// </summary>
        public static IEnumerable<StatementSyntax> ParseStatements(this ISyntaxFactory _,
            IEnumerable<string> texts)
        {
            var output = texts.Select(_.ParseStatement);
            return output;
        }

        /// <summary>
        /// Parses text containing multiple statements. Example text input:
        /// <code>
        /// var executionSynchronicityProviderAction = Instances.ServiceAction.AddConstructorBasedExecutionSynchronicityProviderAction(Synchronicity.Synchronous);
        /// var organizationProviderAction = Instances.ServiceAction.AddOrganizationProviderAction(); // Rivet organization.
        /// var rootOutputDirectoryPathProviderAction = Instances.ServiceAction.AddConstructorBasedRootOutputDirectoryPathProviderAction(@"C:\Temp\Output");
        /// </code>
        /// </summary>
        public static IEnumerable<StatementSyntax> ParseStatements(this ISyntaxFactory _,
            string text)
        {
            // Parse as a compilation unit. Statements will each be wrapped in global statements.
            var statements = SyntaxFactory.ParseCompilationUnit(text)
                .GetChildrenOfType<GlobalStatementSyntax>()
                .Select(xGlobalStatement => xGlobalStatement
                    .GetChildAsType<StatementSyntax>());

            return statements;
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
