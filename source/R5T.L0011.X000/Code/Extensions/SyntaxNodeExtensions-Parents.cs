using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        public static UsingDirectiveSyntax[] GetAvailableUsingDirectives(this SyntaxNode node)
        {
            var containingNamespaces = node.GetContainingNamespaces();

            var compilationUnit = node.GetContainingCompilationUnit();

            var availableUsingDirectives = containingNamespaces
                .SelectMany(x => x.GetChildUsingDirectives())
                .Concat(compilationUnit
                    .GetChildUsingDirectives())
                .Now_OLD();

            return availableUsingDirectives;
        }

        public static CompilationUnitSyntax GetContainingCompilationUnit(this SyntaxNode syntaxNode)
        {
            var currentParent = syntaxNode.Parent;

            while(currentParent is object)
            {
                if(currentParent is CompilationUnitSyntax compilationUnit)
                {
                    return compilationUnit;
                }

                currentParent = currentParent.Parent;
            }

            throw new Exception("Unable to find a containing compilation unit.");
        }

        public static IEnumerable<NamespaceDeclarationSyntax> GetContainingNamespacesInsideToOutside(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetParentsInsideToOutside()
                .OfType<NamespaceDeclarationSyntax>();

            return output;
        }

        public static IEnumerable<NamespaceDeclarationSyntax> GetContainingNamespacesOutsideToInside(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetContainingNamespacesInsideToOutside()
                .Reverse()
                ;

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetContainingNamespacesInsideToOutside(SyntaxNode)"/> as the default.
        /// </summary>
        public static IEnumerable<NamespaceDeclarationSyntax> GetContainingNamespaces(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetContainingNamespacesInsideToOutside();
            return output;
        }

        public static NamespaceDeclarationSyntax GetContainingNamespace(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetContainingNamespacesInsideToOutside()
                .First();

            return output;
        }

        public static WasFound<SyntaxNode> GetParent(this SyntaxNode syntaxNode)
        {
            var output = WasFound.From(syntaxNode.Parent);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetParentsInsideToOutside(SyntaxNode)"/> as the default.
        /// </summary>
        public static SyntaxNode[] GetParents(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetParentsInsideToOutside().ToArray();
            return output;
        }

        public static IEnumerable<SyntaxNode> GetParentsInsideToOutside_Enumerable(this SyntaxNode node)
        {
            var currentNode = node;

            while(currentNode.HasParent())
            {
                yield return currentNode.Parent;

                currentNode = currentNode.Parent;
            }
            // Else, return.
        }

        /// <summary>
        /// The default is <see cref="GetParentsInsideToOutside_Enumerable(SyntaxNode)"/>
        /// </summary>
        public static IEnumerable<SyntaxNode> GetParents_Enumerable(this SyntaxNode node)
        {
            return node.GetParentsInsideToOutside_Enumerable();
        }

        private static void GetParentsInsideToOutside_Internal(this SyntaxNode syntaxNode, List<SyntaxNode> parentAccumulator)
        {
            if (syntaxNode.HasParent())
            {
                parentAccumulator.Add(syntaxNode.Parent);

                syntaxNode.Parent.GetParentsInsideToOutside_Internal(parentAccumulator);
            }
            // Else, return.
        }

        public static IEnumerable<SyntaxNode> GetParentsInsideToOutside(this SyntaxNode syntaxNode)
        {
            var parentAccumulator = new List<SyntaxNode>();

            syntaxNode.GetParentsInsideToOutside_Internal(parentAccumulator);

            return parentAccumulator;
        }

        public static IEnumerable<SyntaxNode> GetParentsOutsideToInside(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetParentsInsideToOutside()
                .Reverse()
                ;

            return output;
        }
    }
}