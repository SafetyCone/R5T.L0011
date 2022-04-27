using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class NamespaceDeclarationSyntaxExtensions
    {
        public static NamespaceDeclarationSyntax AddClass(this NamespaceDeclarationSyntax @namespace,
            ClassDeclarationSyntax @class,
            ModifierSynchronous<ClassDeclarationSyntax> modifier = default)
        {
            var modifiedClass = @class.ModifyWith(modifier);

            var output = @namespace.AddMembers(modifiedClass);
            return output;
        }

        public static NamespaceDeclarationSyntax AddInterface(this NamespaceDeclarationSyntax @namespace,
            InterfaceDeclarationSyntax @interface,
            ModifierSynchronous<InterfaceDeclarationSyntax> modifier = default)
        {
            var modifiedClass = @interface.ModifyWith(modifier);

            var output = @namespace.AddMembers(modifiedClass);
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
        /// Chooses <see cref="HasClass_SingleOrDefault(CompilationUnitSyntax, string)"/> as the default.
        /// </summary>
        public static WasFound<ClassDeclarationSyntax> HasClass(this NamespaceDeclarationSyntax @namespace,
            string className)
        {
            var output = @namespace.HasClass_SingleOrDefault(className);
            return output;
        }
    }
}
