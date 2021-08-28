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
    }
}
