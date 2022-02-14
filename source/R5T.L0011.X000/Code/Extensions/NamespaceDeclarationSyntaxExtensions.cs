using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class NamespaceDeclarationSyntaxExtensions
    {
        public static UsingDirectiveSyntax[] GetChildUsingDirectives(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.ChildNodes()
                .OfType<UsingDirectiveSyntax>()
                .ToArray();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetChildUsingDirectives(CompilationUnitSyntax)"/> as the default.
        /// Only child using directives are returned (as opposed to descendant using directives, which may exist in descendant namespaces).
        /// </summary>
        public static UsingDirectiveSyntax[] GetUsings(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetChildUsingDirectives();
            return output;
        }

        public static UsingDirectiveSyntax GetUsing(this NamespaceDeclarationSyntax @namespace,
            string namespaceName)
        {
            var output = @namespace.GetUsings().GetUsing(namespaceName);
            return output;
        }

        public static IEnumerable<string> GetMissingUsingNamespaceNames(this NamespaceDeclarationSyntax @namespace,
            IEnumerable<string> namespaceNames)
        {
            var output = @namespace.GetUsingNamespaceDirectiveSyntaxes().GetMissingNamespaceNames(namespaceNames);
            return output;
        }

        public static UsingDirectiveSyntax GetUsing(this NamespaceDeclarationSyntax @namespace,
            string destinationName,
            string sourceNameExpression)
        {
            var output = @namespace.GetUsings().GetUsing(destinationName, sourceNameExpression);
            return output;
        }

        /// <summary>
        /// Blocks are separated by blank lines (which are two new lines separated only by whitespace).
        /// </summary>
        public static UsingDirectiveSyntax[][] GetUsingBlocks(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsings().GetUsingBlocks();
            return output;
        }

        public static UsingNameAliasDirectiveSyntax[] GetUsingNameAliasDirectives(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsings().GetUsingNameAliasDirectives().Now();
            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNameAliasDirectiveSyntaxes(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsings().GetUsingNameAliasDirectiveSyntaxes();
            return output;
        }

        public static UsingNamespaceDirectiveSyntax[] GetUsingNamespaceDirectives(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsings().GetUsingNamespaceDirectives().Now();
            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNamespaceDirectiveSyntaxes(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsings().GetUsingNamespaceDirectiveSyntaxes();
            return output;
        }

        public static IEnumerable<string> GetUsingNamespaceNames(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsings().GetUsingNamespaceNames();
            return output;
        }

        public static WasFound<UsingDirectiveSyntax> HasUsing(this NamespaceDeclarationSyntax @namespace,
            string namespaceName)
        {
            var output = @namespace.GetUsings().HasUsing(namespaceName);
            return output;
        }

        public static WasFound<UsingDirectiveSyntax> HasUsing(this NamespaceDeclarationSyntax @namespace,
            string destinationName,
            string sourceNameExpression)
        {
            var output = @namespace.GetUsings().HasUsing(destinationName, sourceNameExpression);
            return output;
        }
    }
}
