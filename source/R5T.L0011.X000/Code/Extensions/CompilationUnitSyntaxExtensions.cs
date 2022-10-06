using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class CompilationUnitSyntaxExtensions
    {
        public static UsingDirectiveSyntax[] GetChildUsingDirectives(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.ChildNodes()
                .OfType<UsingDirectiveSyntax>()
                .ToArray();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetChildUsingDirectives(CompilationUnitSyntax)"/> as the default.
        /// Only child using directives are returned (as opposed to descendant using directives, which may exist in descendant namespaces).
        /// </summary>
        public static UsingDirectiveSyntax[] GetUsings(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetChildUsingDirectives();
            return output;
        }

        public static UsingDirectiveSyntax GetUsing(this CompilationUnitSyntax compilationUnit,
            string namespaceName)
        {
            var output = compilationUnit.GetUsings().GetUsing(namespaceName);
            return output;
        }

        public static IEnumerable<string> GetMissingUsingNamespaceNames(this CompilationUnitSyntax compilationUnit,
            IEnumerable<string> namespaceNames)
        {
            var output = compilationUnit.GetUsingNamespaceDirectiveSyntaxes().GetMissingNamespaceNames(namespaceNames);
            return output;
        }

        public static IEnumerable<NameAlias> GetMissingUsingNameAliases(this CompilationUnitSyntax compilationUnit,
            IEnumerable<NameAlias> nameAliases)
        {
            var output = compilationUnit.GetUsingNameAliasDirectiveSyntaxes().GetMissingNameAliases(nameAliases);
            return output;
        }

        public static IEnumerable<ClassDeclarationSyntax> GetClasses(this CompilationUnitSyntax compilationUnit)
        {
            var classes = compilationUnit.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                ;

            return classes;
        }

        public static IEnumerable<InterfaceDeclarationSyntax> GetInterfaces(this CompilationUnitSyntax compilationUnit)
        {
            var intefaces = compilationUnit.DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>()
                ;

            return intefaces;
        }

        public static IEnumerable<TypeDeclarationSyntax> GetTypes(this CompilationUnitSyntax compilationUnit)
        {
            var classes = compilationUnit.DescendantNodes()
                .OfType<TypeDeclarationSyntax>()
                ;

            return classes;
        }

        public static UsingDirectiveSyntax GetUsing(this CompilationUnitSyntax compilationUnit,
            string destinationName,
            string sourceNameExpression)
        {
            var output = compilationUnit.GetUsings().GetUsing(destinationName, sourceNameExpression);
            return output;
        }

        /// <summary>
        /// Blocks are separated by blank lines (which are two new lines separated only by whitespace).
        /// </summary>
        public static UsingDirectiveSyntax[][] GetUsingBlocks(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetUsings().GetUsingBlocks();
            return output;
        }

        public static UsingNameAliasDirectiveSyntax[] GetUsingNameAliasDirectives(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetUsings().GetUsingNameAliasDirectives().Now_OLD();
            return output;
        }

        public static NameAlias[] GetUsingNameAliases(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetUsingNameAliasDirectives()
                .Select(x => x.GetNameAlias())
                .ToArray();

            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNameAliasDirectiveSyntaxes(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetUsings().GetUsingNameAliasDirectiveSyntaxes();
            return output;
        }

        public static UsingNamespaceDirectiveSyntax[] GetUsingNamespaceDirectives(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetUsings().GetUsingNamespaceDirectives().Now_OLD();
            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNamespaceDirectiveSyntaxes(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetUsings().GetUsingNamespaceDirectiveSyntaxes();
            return output;
        }

        public static IEnumerable<string> GetUsingNamespaceNames(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetUsings().GetUsingNamespaceNames();
            return output;
        }

        public static WasFound<UsingDirectiveSyntax> HasUsing(this CompilationUnitSyntax compilationUnit,
            string namespaceName)
        {
            var output = compilationUnit.GetUsings().HasUsing(namespaceName);
            return output;
        }

        public static WasFound<UsingDirectiveSyntax> HasUsing(this CompilationUnitSyntax compilationUnit,
            string destinationName,
            string sourceNameExpression)
        {
            var output = compilationUnit.GetUsings().HasUsing(destinationName, sourceNameExpression);
            return output;
        }
    }
}
