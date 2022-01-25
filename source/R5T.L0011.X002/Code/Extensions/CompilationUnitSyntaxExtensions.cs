using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T004;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class CompilationUnitSyntaxExtensions
    {
        public static CompilationUnitSyntax AddNameAliases(this CompilationUnitSyntax compilationUnit,
            UsingDirectiveSyntax[] nameAliases,
            bool addBlankLineBeforeFirstAlias = true)
        {
            if (!nameAliases.Any())
            {
                return compilationUnit;
            }

            var firstAlias = addBlankLineBeforeFirstAlias
                ? nameAliases.First().PrependBlankLine()
                : nameAliases.First();
            ;

            var modifiedNameAliases = EnumerableHelper.From(firstAlias).Concat(
                nameAliases.SkipFirst())
                .Now();

            var output = compilationUnit
                .AddUsings(modifiedNameAliases)
                ;

            return output;
        }

        public static CompilationUnitSyntax AddNamespace(this CompilationUnitSyntax compilationUnit, string namespaceName,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithIndentationSynchronous<NamespaceDeclarationSyntax> modifier)
        {
            var indentedLeadingWhitespace = leadingWhitespace.IndentByTab();

            var @namespace = Instances.SyntaxFactory.Namespace(namespaceName)
                .NormalizeWhitespace() // Regular spacing.
                .WithLeadingTrivia(leadingWhitespace.PrependNewLine().PrependNewLine()) // Two blank lines.
                .WithOpenBraceToken(Instances.SyntaxFactory.OpenBrace2(leadingWhitespace, false))
                .WithCloseBraceToken(Instances.SyntaxFactory.CloseBrace2(leadingWhitespace))
                .ModifyWith(indentedLeadingWhitespace, modifier)
                ;

            var output = compilationUnit.AddMembers(@namespace);
            return output;
        }

        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            IUsingDirectivesBlockList blockList)
        {
            var usingDirectives = Instances.SyntaxFactory.GetUsingDirectives(blockList);

            var output = compilationUnit.AddUsings(usingDirectives);
            return output;
        }

        public static NamespaceNameSet GetNamespaceNameSet(this CompilationUnitSyntax compilationUnit)
        {
            var usings = compilationUnit.GetUsings();

            var namespaceNames = usings
                .Select(@using => @using.Name.ToString())
                .ToArray();

            var set = NamespaceNameSet.New().AddRange(namespaceNames);
            return set;
        }

        public static async Task<CompilationUnitSyntax> ModifyClass(this CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, NamespaceDeclarationSyntax> namespaceSelector,
            Func<NamespaceDeclarationSyntax, ClassDeclarationSyntax> classSelector,
            Func<ClassDeclarationSyntax, Task<ClassDeclarationSyntax>> classAction = default)
        {
            var @namespace = namespaceSelector(compilationUnit);
            var @class = classSelector(@namespace);

            var outputClass = await classAction(@class);

            var outputCompilationUnit = compilationUnit.ReplaceNode(@class, outputClass);
            return outputCompilationUnit;
        }

        public static CompilationUnitSyntax SetUsings(this CompilationUnitSyntax compilationUnit,
            IUsingDirectivesBlockList blockList)
        {
            var usingDirectives = Instances.SyntaxFactory.GetUsingDirectives(blockList);

            var output = compilationUnit.WithUsings(Instances.SyntaxFactory.SyntaxList(usingDirectives));
            return output;
        }
    }
}
