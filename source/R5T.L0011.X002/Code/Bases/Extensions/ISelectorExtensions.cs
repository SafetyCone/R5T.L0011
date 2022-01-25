using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0060;


namespace System
{
    public static class ISelectorExtensions
    {
        public static Func<CompilationUnitSyntax, NamespaceDeclarationSyntax> FirstNamespace(this ISelector _)
        {
            return compilationUnit => compilationUnit.GetNamespaces().First();
        }

        public static Func<CompilationUnitSyntax, ClassDeclarationSyntax> ClassNamed(this ISelector _,
            string className)
        {
            return compilationUnit => compilationUnit.GetNamespaces()
                .SelectMany(xNamespace => xNamespace.GetClasses())
                .Where(xClass => xClass.IsClassName(className))
                .Single();
        }

        public static Func<ClassDeclarationSyntax, MethodDeclarationSyntax> MethodNamed(this ISelector _,
            string methodName)
        {
            return @class => @class.GetMethods()
                .Where(xMethod => xMethod.IsName(methodName))
                .Single();
        }
    }
}
