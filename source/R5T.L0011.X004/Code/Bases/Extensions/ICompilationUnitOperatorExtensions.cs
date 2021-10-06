using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;
using R5T.T0049;

using Instances = R5T.L0011.X004.Instances;


namespace System
{
    public static class ICompilationUnitOperatorExtensions
    {
        public static
            IEnumerable<(NamespaceDeclarationSyntax Namespace, TTypeDeclaration Type)>
        GetNamespaceTypePairs<TTypeDeclaration>(this ICompilationUnitOperator _,
            CompilationUnitSyntax compilationUnit)
            where TTypeDeclaration : BaseTypeDeclarationSyntax
        {
            var output = compilationUnit.GetNamespaces()
                .SelectMany(xNamespace => xNamespace.GetDescendantsOfType<NamespaceDeclarationSyntax, TTypeDeclaration>()
                    .Select(xNode => (xNamespace, xNode)));

            return output;
        }

        public static
            IEnumerable<(NamespaceDeclarationSyntax Namespace, TTypeDeclaration Type)>
        GetNamespaceTypePairs<TTypeDeclaration>(this ICompilationUnitOperator _,
            CompilationUnitSyntax compilationUnit,
            Func<TTypeDeclaration, bool> predicateOnTypeDeclaration)
            where TTypeDeclaration : BaseTypeDeclarationSyntax
        {
            var output = _.GetNamespaceTypePairs<TTypeDeclaration>(compilationUnit)
                .Where(xPair => FunctionHelper.Run(predicateOnTypeDeclaration, xPair.Type))
                ;

            return output;
        }

        public static
            IEnumerable<(NamespaceDeclarationSyntax Namespace, ClassDeclarationSyntax StaticClass)>
        GetStaticClassPairs(this ICompilationUnitOperator _,
            CompilationUnitSyntax compilationUnit,
            Func<ClassDeclarationSyntax, bool> predicateOnStaticClassDeclaration = default)
        {
            var output = _.GetNamespaceTypePairs<ClassDeclarationSyntax>(compilationUnit,
                Instances.ClassOperator.GetIsStaticClassPredicate(predicateOnStaticClassDeclaration))
                ;

            return output;
        }

        public static
            IEnumerable<(NamespaceDeclarationSyntax Namespace, TTypeDeclaration Type, MethodDeclarationSyntax Method)>
        GetNamespaceTypeMethodTuples<TTypeDeclaration>(this ICompilationUnitOperator _,
            CompilationUnitSyntax compilationUnit,
            Func<TTypeDeclaration, bool> predicateOnTypeDeclaration = default)
            where TTypeDeclaration : BaseTypeDeclarationSyntax
        {
            var output = _.GetNamespaceTypePairs(compilationUnit,
                predicateOnTypeDeclaration)
                .SelectMany(xPair => xPair.Type.GetDescendantsOfType<TTypeDeclaration, MethodDeclarationSyntax>()
                    .Select(xMethod => (xPair.Namespace, xPair.Type, xMethod)))
                ;

            return output;
        }

        public static
            IEnumerable<(NamespaceDeclarationSyntax Namespace, TTypeDeclaration Type, MethodDeclarationSyntax Method)>
        GetNamespaceTypeMethodTuples<TTypeDeclaration>(this ICompilationUnitOperator _,
            CompilationUnitSyntax compilationUnit,
            Func<TTypeDeclaration, bool> predicateOnTypeDeclaration = default,
            Func<MethodDeclarationSyntax, bool> predicateOnMethodDeclaration = default)
            where TTypeDeclaration : BaseTypeDeclarationSyntax
        {
            var output = _.GetNamespaceTypeMethodTuples(compilationUnit,
                predicateOnTypeDeclaration)
                .Where(xTuple => FunctionHelper.Run(predicateOnMethodDeclaration, xTuple.Method))
                ;

            return output;
        }

        public static
            IEnumerable<(NamespaceDeclarationSyntax Namespace, ClassDeclarationSyntax StaticClass, MethodDeclarationSyntax ExtensionMethod)>
        GetExtensionMethodTuples(this ICompilationUnitOperator _,
            CompilationUnitSyntax compilationUnit,
            Func<MethodDeclarationSyntax, bool> predicateOnMethodDeclaration = default)
        {
            var output = _.GetNamespaceTypeMethodTuples(compilationUnit,
                Instances.ClassOperator.GetIsStaticClassPredicate(),
                Instances.MethodOperator.GetIsExtensionMethodPredicate(predicateOnMethodDeclaration))
                ;

            return output;
        }

        public static string[] GetExtensionMethodNamespacedTypedMethodNames_StringlyTyped(this ICompilationUnitOperator _,
            CompilationUnitSyntax compilationUnit)
        {
            var output = _.GetExtensionMethodTuples(compilationUnit)
                .Select(xTuple =>
                {
                    var namespacedTypedMethodName = Instances.MethodNameOperator.GetNamespacedTypedParameterizedMethodName(xTuple);
                    return namespacedTypedMethodName;
                })
                .ToArray();

            return output;
        }

        public static string[] GetNamespacedTypeNames_StringlyTyped<TTypeDeclaration>(this ICompilationUnitOperator _,
            CompilationUnitSyntax compilationUnit,
            Func<TTypeDeclaration, bool> typeDeclarationPredicate = default)
            where TTypeDeclaration : BaseTypeDeclarationSyntax
        {
            var output = compilationUnit.GetNamespaces()
                .SelectMany(xNamespace => xNamespace.GetDescendantsOfType<NamespaceDeclarationSyntax, TTypeDeclaration>()
                    .Select(xNode => (xNamespace, xNode)))
                .Where(xNamespaceAndNodePair => FunctionHelper.Run(
                    typeDeclarationPredicate,
                    xNamespaceAndNodePair.xNode))
                .Select(xNamespaceAndInterfacePair =>
                {
                    var nodeTypeName = xNamespaceAndInterfacePair.xNode.Identifier.ToString();
                    var namespacename = xNamespaceAndInterfacePair.xNamespace.Name.ToString();

                    var interfaceNamespacedTypeNameValue = Instances.NamespacedTypeName.GetNamespacedName(
                        namespacename,
                        nodeTypeName);

                    return interfaceNamespacedTypeNameValue;
                })
                .ToArray();

            return output;
        }

        public static NamespacedTypeName[] GetNamespacedTypeNames_StronglyTyped<TTypeDeclaration>(this ICompilationUnitOperator _,
            CompilationUnitSyntax compilationUnit,
            Func<TTypeDeclaration, bool> typeDeclarationPredicate = default)
            where TTypeDeclaration : BaseTypeDeclarationSyntax
        {
            var stringlyTypedNamespacedTypeNames = _.GetNamespacedTypeNames_StringlyTyped(
                compilationUnit,
                typeDeclarationPredicate);

            var output = stringlyTypedNamespacedTypeNames
                .Select(xStringlyTypedNamespacedTypeName => Instances.NamespacedTypeName.ToStronglyTypedNamespacedTypeName(xStringlyTypedNamespacedTypeName))
                .ToArray();

            return output;
        }

        /// <summary>
        /// Selects <see cref="GetNamespacedTypeNames_StronglyTyped{TTypeDeclaration}(ICompilationUnitOperator, CompilationUnitSyntax, Func{TTypeDeclaration, bool})"/> as the default.
        /// </summary>
        public static NamespacedTypeName[] GetNamespacedTypeNames<TTypeDeclaration>(this ICompilationUnitOperator _,
            CompilationUnitSyntax compilationUnit,
            Func<TTypeDeclaration, bool> typeDeclarationPredicate = default)
            where TTypeDeclaration : BaseTypeDeclarationSyntax
        {
            var output = _.GetNamespacedTypeNames_StronglyTyped(
                compilationUnit,
                typeDeclarationPredicate);

            return output;
        }

        public static Task<CompilationUnitSyntax> LoadCompilationUnit(this ICompilationUnitOperator _,
            string filePath)
        {
            return Instances.CodeFileOperator.LoadCompilationUnit(filePath);
        }

        /// <summary>
        /// Quality-of-life overload for <see cref="LoadCompilationUnit(ICodeFile, string)"/>.
        /// </summary>
        public static Task<CompilationUnitSyntax> Load(this ICompilationUnitOperator _,
            string filePath)
        {
            return _.LoadCompilationUnit(filePath);
        }
    }
}
