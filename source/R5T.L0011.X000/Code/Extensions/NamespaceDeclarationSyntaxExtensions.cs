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

        public static IEnumerable<string> GetMissingUsingNamespaceNames(this NamespaceDeclarationSyntax @namespace,
            IEnumerable<string> namespaceNames)
        {
            var output = @namespace.GetUsingNamespaceDirectiveSyntaxes().GetMissingNamespaceNames(namespaceNames);
            return output;
        }

        /// <summary>
        /// Get the simple name for a namespace (regardless of namespace nesting).
        /// For example, if namespace "Z" was nested inside "X.Y", this method would just return "Z".
        /// </summary>
        public static string GetName(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.Name.ToString();
            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetChildUsingDirectives(NamespaceDeclarationSyntax)"/> as the default.
        /// Only child using directives are returned (as opposed to descendant using directives, which may exist in descendant namespaces).
        /// </summary>
        public static UsingDirectiveSyntax[] GetUsingDirectives(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetChildUsingDirectives();
            return output;
        }

        public static UsingDirectiveSyntax GetUsingDirective(this NamespaceDeclarationSyntax @namespace,
            string namespaceName)
        {
            var output = @namespace.GetUsingDirectives().GetUsing(namespaceName);
            return output;
        }

        public static UsingDirectiveSyntax GetUsingDirective(this NamespaceDeclarationSyntax @namespace,
            string destinationName,
            string sourceNameExpression)
        {
            var output = @namespace.GetUsingDirectives().GetUsing(destinationName, sourceNameExpression);
            return output;
        }

        /// <summary>
        /// Blocks are separated by blank lines (which are two new lines separated only by whitespace).
        /// </summary>
        public static UsingDirectiveSyntax[][] GetUsingBlocks(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsingDirectives().GetUsingBlocks();
            return output;
        }

        public static UsingNameAliasDirectiveSyntax[] GetUsingNameAliasDirectives(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsingDirectives().GetUsingNameAliasDirectives().Now_OLD();
            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNameAliasDirectiveSyntaxes(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsingDirectives().GetUsingNameAliasDirectiveSyntaxes();
            return output;
        }

        public static UsingNamespaceDirectiveSyntax[] GetUsingNamespaceDirectives(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsingDirectives().GetUsingNamespaceDirectives().Now_OLD();
            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNamespaceDirectiveSyntaxes(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsingDirectives().GetUsingNamespaceDirectiveSyntaxes();
            return output;
        }

        public static IEnumerable<string> GetUsingNamespaceNames(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.GetUsingDirectives().GetUsingNamespaceNames();
            return output;
        }

        public static WasFound<UsingDirectiveSyntax> HasUsing(this NamespaceDeclarationSyntax @namespace,
            string namespaceName)
        {
            var output = @namespace.GetUsingDirectives().HasUsing(namespaceName);
            return output;
        }

        public static WasFound<UsingDirectiveSyntax> HasUsing(this NamespaceDeclarationSyntax @namespace,
            string destinationName,
            string sourceNameExpression)
        {
            var output = @namespace.GetUsingDirectives().HasUsing(destinationName, sourceNameExpression);
            return output;
        }

        public static IEnumerable<ClassDeclarationSyntax> GetClasses(this NamespaceDeclarationSyntax @namespace)
        {
            var classes = @namespace.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                ;

            return classes;
        }

        public static WasFound<ClassDeclarationSyntax> HasClass_SingleOrDefault(this NamespaceDeclarationSyntax @namespace,
            string className)
        {
            var @class = @namespace.GetClasses()
                .Where(x => x.Identifier.Text == className)
                .SingleOrDefault();

            var output = WasFound.From(@class);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasClass_SingleOrDefault(NamespaceDeclarationSyntax, string)"/> as the default.
        /// </summary>
        public static WasFound<ClassDeclarationSyntax> HasClass(this NamespaceDeclarationSyntax @namespace,
            string className)
        {
            var output = @namespace.HasClass_SingleOrDefault(className);
            return output;
        }

        public static IEnumerable<InterfaceDeclarationSyntax> GetInterfaces(this NamespaceDeclarationSyntax @namespace)
        {
            var output = @namespace.DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>()
                ;

            return output;
        }

        public static WasFound<InterfaceDeclarationSyntax> HasInterface_SingleOrDefault(this NamespaceDeclarationSyntax @namespace,
            string interfaceName)
        {
            var @interface = @namespace.GetInterfaces()
                .Where(x => x.Identifier.Text == interfaceName)
                .SingleOrDefault();

            var output = WasFound.From(@interface);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasInterface_SingleOrDefault(NamespaceDeclarationSyntax, string)"/> as the default.
        /// </summary>
        public static WasFound<InterfaceDeclarationSyntax> HasInterface(this NamespaceDeclarationSyntax @namespace,
            string className)
        {
            var output = @namespace.HasInterface_SingleOrDefault(className);
            return output;
        }
    }
}
