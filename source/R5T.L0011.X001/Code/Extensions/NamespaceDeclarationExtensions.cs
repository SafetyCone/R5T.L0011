using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class NamespaceDeclarationExtensions
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
    }
}
