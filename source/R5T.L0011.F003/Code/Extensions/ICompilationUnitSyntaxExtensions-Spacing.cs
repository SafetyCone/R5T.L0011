using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace System
{
    /// <summary>
    /// Provides <see cref="CompilationUnitSyntax"/>-specific overloads to the general <see cref="SyntaxNode"/> extensions.
    /// </summary>
    public static class ICompilationUnitSyntaxExtensions
    {
        /// <summary>
        /// Sets the line indentation for the first line of the annotated syntax node.
        /// </summary>
        public static CompilationUnitSyntax SetLineIndentation(this CompilationUnitSyntax compilationUnit,
            ISyntaxNodeAnnotation nodeAnnotation,
            SyntaxTriviaList lineIndentation)
        {
            var output = compilationUnit.SetLineIndentation<CompilationUnitSyntax>(
                nodeAnnotation,
                lineIndentation);

            return output;
        }

        public static CompilationUnitSyntax SetLeadingSeparatingSpacing(this CompilationUnitSyntax compilationUnit,
            ISyntaxNodeAnnotation nodeAnnotation,
            SyntaxTriviaList leadingSeparatingSpacing)
        {
            var output = compilationUnit.SetLeadingSeparatingSpacing<CompilationUnitSyntax>(
                nodeAnnotation,
                leadingSeparatingSpacing);

            return output;
        }

        public static CompilationUnitSyntax SetLeadingSeparatingSpacing(this CompilationUnitSyntax compilationUnit,
            SyntaxNode node,
            SyntaxTriviaList leadingSeparatingSpacing)
        {
            var output = compilationUnit.SetLeadingSeparatingSpacing<CompilationUnitSyntax>(
                node,
                leadingSeparatingSpacing);

            return output;
        }

        /// <summary>
        /// Sets the line indentation for the first line of the annotated syntax node.
        /// </summary>
        public static CompilationUnitSyntax SetLineIndentation(this CompilationUnitSyntax compilationUnit,
            SyntaxToken token,
            SyntaxTriviaList lineIndentation)
        {
            var output = compilationUnit.SetLineIndentation<CompilationUnitSyntax>(
                token,
                lineIndentation);

            return output;
        }

        public static CompilationUnitSyntax SetLeadingSeparatingSpacing(this CompilationUnitSyntax compilationUnit,
            SyntaxToken token,
            SyntaxTriviaList leadingSeparatingSpacing)
        {
            var output = compilationUnit.SetLeadingSeparatingSpacing<CompilationUnitSyntax>(
                token,
                leadingSeparatingSpacing);

            return output;
        }

        public static CompilationUnitSyntax SetLeadingSeparatingSpacing(this CompilationUnitSyntax compilationUnit,
            SyntaxToken token,
            SyntaxToken previousToken,
            SyntaxTriviaList leadingSeparatingSpacing)
        {
            var output = compilationUnit.SetLeadingSeparatingSpacing<CompilationUnitSyntax>(
                token,
                previousToken,
                leadingSeparatingSpacing);

            return output;
        }
    }
}