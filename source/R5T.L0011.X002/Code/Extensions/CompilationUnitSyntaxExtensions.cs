using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;
using R5T.L0011.T004;


namespace System
{
    public static class CompilationUnitSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static CompilationUnitSyntax AddNamespace(this CompilationUnitSyntax compilationUnit, string namespaceName,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithLineLeadingWhitespace<NamespaceDeclarationSyntax> modifier)
        {
            var indentedLeadingWhitespace = leadingWhitespace.IndentByTab();

            var @namespace = SyntaxFactory.Namespace(namespaceName)
                .NormalizeWhitespace() // Regular spacing.
                .WithLeadingTrivia(leadingWhitespace.PrependNewLine().PrependNewLine()) // Two blank lines.
                .WithOpenBraceToken(SyntaxFactory.OpenBrace2(leadingWhitespace, false))
                .WithCloseBraceToken(SyntaxFactory.CloseBrace2(leadingWhitespace))
                .ModifyWith(indentedLeadingWhitespace, modifier)
                ;

            var output = compilationUnit.AddMembers(@namespace);
            return output;
        }

        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            IUsingDirectivesBlockList blockList)
        {
            var usingDirectives = SyntaxFactory.GetUsingDirectives(blockList);

            var output = compilationUnit.AddUsings(usingDirectives);
            return output;
        }
    }
}
