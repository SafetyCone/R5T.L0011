using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;
using R5T.L0011.T004;
using R5T.L0011.X002;


namespace System
{
    public static class CompilationUnitSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static NamespaceNameSet GetNamespaceNameSet(this CompilationUnitSyntax compilationUnit)
        {
            var usings = compilationUnit.GetUsings();

            var namespaceNames = usings
                .Select(@using => @using.Name.ToString())
                .ToArray();

            var set = NamespaceNameSet.New().AddRange(namespaceNames);
            return set;
        }

        public static UsingDirectiveSyntax[] GetUsings(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.ChildNodes()
                .OfType<UsingDirectiveSyntax>()
                .ToArray();

            return output;
        }

        public static CompilationUnitSyntax AddNamespace(this CompilationUnitSyntax compilationUnit, string namespaceName,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithIndentation<NamespaceDeclarationSyntax> modifier)
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

        public static CompilationUnitSyntax SetUsings(this CompilationUnitSyntax compilationUnit,
            IUsingDirectivesBlockList blockList)
        {
            var usingDirectives = SyntaxFactory.GetUsingDirectives(blockList);

            var output = compilationUnit.WithUsings(Instances.SyntaxFactory.SyntaxList(usingDirectives));
            return output;
        }
    }
}
