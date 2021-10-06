using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class CompilationUnitSyntaxExtensions
    {
        public static UsingDirectiveSyntax[] GetUsings(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.ChildNodes()
                .OfType<UsingDirectiveSyntax>()
                .ToArray();

            return output;
        }

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

        public static WasFound<ClassDeclarationSyntax> HasClass_SingleOrDefault(this CompilationUnitSyntax compilationUnit,
            string className)
        {
            var @class = compilationUnit.GetClasses()
                .Where(x => x.Identifier.Text == className)
                .SingleOrDefault();

            var output = WasFound.From(@class);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasClass_SingleOrDefault(CompilationUnitSyntax, string)"/> as the default.
        /// </summary>
        public static WasFound<ClassDeclarationSyntax> HasClass(this CompilationUnitSyntax compilationUnit,
            string className)
        {
            var output = compilationUnit.HasClass_SingleOrDefault(className);
            return output;
        }

        /// <summary>
        /// Gets the single descendent class. (Assumes one class per compilation unit.)
        /// </summary>
        public static ClassDeclarationSyntax GetClassSingle(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetClasses()
                .Single();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetClassSingle(CompilationUnitSyntax)"/> as the default.
        /// </summary>
        public static ClassDeclarationSyntax GetClass(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetClassSingle();
            return output;
        }

        public static IEnumerable<InterfaceDeclarationSyntax> GetInterfaces(this CompilationUnitSyntax compilationUnit)
        {
            var intefaces = compilationUnit.DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>()
                ;

            return intefaces;
        }

        public static InterfaceDeclarationSyntax GetInterfaceSingle(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetInterfaces()
                .Single();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetInterfaceSingle(CompilationUnitSyntax)"/> as the default.
        /// </summary>
        public static InterfaceDeclarationSyntax GetInterface(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetInterfaceSingle();
            return output;
        }

        public static IEnumerable<NamespaceDeclarationSyntax> GetNamespaces(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.DescendantNodes()
                .OfType<NamespaceDeclarationSyntax>()
                ;

            return output;
        }

        public static NamespaceDeclarationSyntax GetNamespace_SingleOrDefault(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetNamespaces()
                .SingleOrDefault();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetNamespace_SingleOrDefault(CompilationUnitSyntax)"/>.
        /// </summary>
        public static NamespaceDeclarationSyntax GetNamespace(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetNamespace_SingleOrDefault();
            return output;
        }

        public static WasFound<ClassDeclarationSyntax> HasClassSingle(this CompilationUnitSyntax compilationUnit)
        {
            var classOrDefault = compilationUnit.GetClasses()
                .SingleOrDefault();

            var output = WasFound.From(classOrDefault);
            return output;
        }

        public static WasFound<ClassDeclarationSyntax> HasClassFirst(this CompilationUnitSyntax compilationUnit)
        {
            var classOrDefault = compilationUnit.GetClasses()
                .FirstOrDefault();

            var output = WasFound.From(classOrDefault);
            return output;
        }

        /// <summary>
        /// Select <see cref="HasClassSingle(CompilationUnitSyntax)"/> as the default.
        /// </summary>
        public static WasFound<ClassDeclarationSyntax> HasClass(this CompilationUnitSyntax compilationUnit)
        {
            var classOrDefault = compilationUnit.GetClasses()
                .SingleOrDefault();

            var output = WasFound.From(classOrDefault);
            return output;
        }

        public static WasFound<InterfaceDeclarationSyntax> HasInterfaceSingle(this CompilationUnitSyntax compilationUnit)
        {
            var interfaceOrDefault = compilationUnit.GetInterfaces()
                .SingleOrDefault();

            var output = WasFound.From(interfaceOrDefault);
            return output;
        }

        public static WasFound<InterfaceDeclarationSyntax> HasInterfaceFirst(this CompilationUnitSyntax compilationUnit)
        {
            var interfaceOrDefault = compilationUnit.GetInterfaces()
                .FirstOrDefault();

            var output = WasFound.From(interfaceOrDefault);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasInterfaceSingle(CompilationUnitSyntax)"/> as the default.
        /// </summary>
        public static WasFound<InterfaceDeclarationSyntax> HasInterface(this CompilationUnitSyntax compilationUnit)
        {
            return compilationUnit.HasInterfaceSingle();
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
