using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using Instances = R5T.L0011.X001_1.Instances;


namespace System
{
    public static class CompilationUnitSyntaxExtensions
    {
        /// <summary>
        /// Selects <see cref="AddUsing_Idempotent(CompilationUnitSyntax, string)"/> as the default.
        /// </summary>
        public static CompilationUnitSyntax AddUsing(this CompilationUnitSyntax compilationUnit,
            string namespaceName)
        {
            var output = compilationUnit.AddUsing_Idempotent(namespaceName);
            return output;
        }

        /// <summary>
        /// Selects <see cref="AddUsing_Idempotent(CompilationUnitSyntax, string, string)"/> as the default.
        /// </summary>
        public static CompilationUnitSyntax AddUsing(this CompilationUnitSyntax compilationUnit,
            string destinationName,
            string sourceNameExpression)
        {
            var output = compilationUnit.AddUsing_Idempotent(destinationName, sourceNameExpression);
            return output;
        }

        public static CompilationUnitSyntax AddUsing_Idempotent(this CompilationUnitSyntax compilationUnit,
            string namespaceName)
        {
            var hasUsing = compilationUnit.HasUsing(namespaceName);
            if(!hasUsing)
            {
                var outputCompilationUnit = compilationUnit.AddUsing_NonIdempotent(namespaceName);
                return outputCompilationUnit;
            }

            return compilationUnit;
        }

        public static CompilationUnitSyntax AddUsing_Idempotent(this CompilationUnitSyntax compilationUnit,
            string destinationName,
            string sourceNameExpression)
        {
            var hasUsing = compilationUnit.HasUsing(destinationName, sourceNameExpression);
            if (!hasUsing)
            {
                var outputCompilationUnit = compilationUnit.AddUsing_NonIdempotent(destinationName, sourceNameExpression);
                return outputCompilationUnit;
            }

            return compilationUnit;
        }

        public static CompilationUnitSyntax AddUsing_NonIdempotent(this CompilationUnitSyntax compilationUnit,
            string namespaceName)
        {
            var usingDirective = Instances.SyntaxFactory.Using_WithoutLeadingNewLine(namespaceName);

            var output = compilationUnit.AddUsings(usingDirective);
            return output;
        }

        public static CompilationUnitSyntax AddUsing_NonIdempotent(this CompilationUnitSyntax compilationUnit,
            string destinationName,
            string sourceNameExpression)
        {
            var usingDirective = Instances.SyntaxFactory.Using(destinationName, sourceNameExpression);

            var output = compilationUnit.AddUsings(usingDirective);
            return output;
        }

        public static CompilationUnitSyntax AddUsings_NonIdempotent(this CompilationUnitSyntax compilationUnit,
            IEnumerable<string> namespaceNames)
        {
            var usingDirectives = namespaceNames
                .Select(xNamespaceName => Instances.SyntaxFactory.Using(xNamespaceName))
                .ToArray();

            var output = compilationUnit.AddUsings(usingDirectives);
            return output;
        }

        public static CompilationUnitSyntax AddUsings_Idempotent(this CompilationUnitSyntax compilationUnit,
            IEnumerable<string> namespaceNames)
        {
            var missingUsingDirectives = compilationUnit.GetMissingUsingNamespaceNames(namespaceNames);

            var output = compilationUnit.AddUsings_NonIdempotent(missingUsingDirectives);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="AddUsings_Idempotent(CompilationUnitSyntax, IEnumerable{string})"/> as the default.
        /// </summary>
        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            IEnumerable<string> namespaceNames)
        {
            var output = compilationUnit.AddUsings_Idempotent(namespaceNames);
            return output;
        }

        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            params string[] namespaceNames)
        {
            var output = compilationUnit.AddUsings(namespaceNames.AsEnumerable());
            return output;
        }

        public static CompilationUnitSyntax AddUsings_NonIdempotent(this CompilationUnitSyntax compilationUnit,
            IEnumerable<NameAlias> nameAliases)
        {
            var usingDirectives = nameAliases
                .Select(xNameAlias => Instances.SyntaxFactory.Using(xNameAlias))
                .ToArray();

            var output = compilationUnit.AddUsings(usingDirectives);
            return output;
        }

        public static CompilationUnitSyntax AddUsings_Idempotent(this CompilationUnitSyntax compilationUnit,
            IEnumerable<NameAlias> nameAliases)
        {
            var missingNameAliases = compilationUnit.GetMissingUsingNameAliases(nameAliases);

            var output = compilationUnit.AddUsings_NonIdempotent(missingNameAliases);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="AddUsings_Idempotent(CompilationUnitSyntax, IEnumerable{NameAlias})"/> as the default.
        /// </summary>
        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            IEnumerable<NameAlias> nameAliases)
        {
            var output = compilationUnit.AddUsings_Idempotent(nameAliases);
            return output;
        }

        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            params NameAlias[] nameAliases)
        {
            var output = compilationUnit.AddUsings(nameAliases.AsEnumerable());
            return output;
        }

        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            IEnumerable<(string DestinationName, string SourceNameExpression)> nameAliasValues)
        {
            var nameAliases = nameAliasValues
                .Select(xTuple => NameAlias.From(
                    xTuple.DestinationName,
                    xTuple.SourceNameExpression))
                ;

            var output = compilationUnit.AddUsings(nameAliases);
            return output;
        }

        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            params (string DestinationName, string SourceNameExpression)[] nameAliasValues)
        {
            var output = compilationUnit.AddUsings(nameAliasValues.AsEnumerable());
            return output;
        }

        public static CompilationUnitSyntax EnsureHasUsings(this CompilationUnitSyntax compilationUnit,
            IEnumerable<string> namespaceNames)
        {
            var output = compilationUnit.AddUsings_Idempotent(namespaceNames);
            return output;
        }

        public static CompilationUnitSyntax EnsureHasUsings(this CompilationUnitSyntax compilationUnit,
            params string[] namespaceNames)
        {
            var output = compilationUnit.EnsureHasUsings(namespaceNames.AsEnumerable());
            return output;
        }
    }
}
