using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


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

        public static IEnumerable<NamespaceDeclarationSyntax> GetNamespaces(this CompilationUnitSyntax compilationUnit,
            Func<NamespaceDeclarationSyntax, bool> namespaceSelector)
        {
            var output = compilationUnit.GetNamespaces()
                .Where(namespaceSelector)
                ;

            return output;
        }

        public static NamespaceDeclarationSyntax GetNamespace_SingleOrDefault(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit
                .GetNamespaces()
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

        public static NamespaceDeclarationSyntax GetNamespace_SingleOrDefault(this CompilationUnitSyntax compilationUnit,
            string namespaceName)
        {
            var output = compilationUnit
                .GetNamespaces(xNamespace => xNamespace.GetName() == namespaceName)
                .SingleOrDefault();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetNamespace_SingleOrDefault(CompilationUnitSyntax, string)"/>.
        /// </summary>
        public static NamespaceDeclarationSyntax GetNamespace(this CompilationUnitSyntax compilationUnit,
            string namespaceName)
        {
            var output = compilationUnit.GetNamespace_SingleOrDefault(namespaceName);
            return output;
        }

        public static WasFound<NamespaceDeclarationSyntax> HasNamespace(this CompilationUnitSyntax compilationUnit,
            string namespaceName)
        {
            var namespaceOrDefault = compilationUnit
                .GetNamespaces(xNamespace => xNamespace.GetName() == namespaceName)
                .SingleOrDefault();

            var output = WasFound.From(namespaceOrDefault);
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

        public static async Task<CompilationUnitSyntax> ModifyClass(this CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, ClassDeclarationSyntax> classSelector,
            Func<ClassDeclarationSyntax, Task<ClassDeclarationSyntax>> classAction = default)
        {
            var @class = classSelector(compilationUnit);

            var outputClass = await classAction(@class);

            var outputCompilationUnit = compilationUnit.ReplaceNode(@class, outputClass);
            return outputCompilationUnit;
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

        public static async Task<CompilationUnitSyntax> ModifyClassMethod(this CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, ClassDeclarationSyntax> classSelector,
            Func<ClassDeclarationSyntax, MethodDeclarationSyntax> methodSelector,
            Func<MethodDeclarationSyntax, Task<MethodDeclarationSyntax>> methodAction = default)
        {
            var outputCompilationUnit = await compilationUnit.ModifyClass(
                classSelector,
                async @class =>
                {
                    var outputClass = await @class.ModifyMethod(
                        methodSelector,
                        methodAction);

                    return outputClass;
                });

            return outputCompilationUnit;
        }
    }
}
