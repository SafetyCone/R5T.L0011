using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class CompilationUnitSyntaxExtensions
    {
        public static IEnumerable<ClassDeclarationSyntax> GetClasses(this CompilationUnitSyntax compilationUnit)
        {
            var classes = compilationUnit.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                ;

            return classes;
        }

        public static ClassDeclarationSyntax GetClass(this CompilationUnitSyntax compilationUnit,
            string className)
        {
            var @class = compilationUnit.GetClasses()
                .Where(x => x.Identifier.Text == className)
                .Single();

            return @class;
        }

        /// <summary>
        /// Gets the single descendent class. (Assumes one class per compilation unit.)
        /// </summary>
        public static ClassDeclarationSyntax GetClass(this CompilationUnitSyntax compilationUnit)
        {
            var @class = compilationUnit.GetClasses()
                .Single();

            return @class;
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
            var intefaces = compilationUnit.DescendantNodes()
                .OfType<TypeDeclarationSyntax>()
                ;

            return intefaces;
        }

        public static TSyntaxNode ModifyWith<TSyntaxNode>(this TSyntaxNode syntaxNode, ModifierSynchronous<TSyntaxNode> modifier)
            where TSyntaxNode : SyntaxNode
        {
            var output = modifier is object
                ? modifier(syntaxNode)
                : syntaxNode;

            return output;
        }

        public static TSyntaxNode ModifyWith<TSyntaxNode, TData>(this TSyntaxNode syntaxNode, ModifierSynchronousWith<TSyntaxNode, TData> modifier, TData data)
            where TSyntaxNode : SyntaxNode
        {
            var output = modifier is object
                ? modifier(syntaxNode, data)
                : syntaxNode;

            return output;
        }
    }
}
