using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class BaseTypeDeclarationSyntaxExtensions
    {
        public static TNode EnsureHasBraces<TNode>(this TNode node,
            SyntaxTriviaList indentation)
            where TNode : BaseTypeDeclarationSyntax
        {
            var outputNode = node;

            outputNode = outputNode.EnsureHasCloseBrace(indentation);
            outputNode = outputNode.EnsureHasOpenBrace(indentation);

            return outputNode;
        }

        public static TNode EnsureHasCloseBrace<TNode>(this TNode node,
            SyntaxTriviaList indentation)
            where TNode : BaseTypeDeclarationSyntax
        {
            var outputNode = node;

            var hasCloseBrace = node.HasCloseBrace();
            if (!hasCloseBrace)
            {
                var closeBrace = Instances.SyntaxFactory.CloseBrace(
                    indentation);

                outputNode = outputNode.WithCloseBraceToken(closeBrace) as TNode;
            }

            return outputNode;
        }

        public static TNode EnsureHasOpenBrace<TNode>(this TNode node,
            SyntaxTriviaList indentation)
            where TNode : BaseTypeDeclarationSyntax
        {
            var outputNode = node;

            var hasOpenBrace = node.HasOpenBrace();
            if (!hasOpenBrace)
            {
                var openBrace = Instances.SyntaxFactory.OpenBrace(
                    indentation);

                outputNode = outputNode.WithOpenBraceToken(openBrace) as TNode;
            }

            return outputNode;
        }

        public static T WithBraces<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList indentation)
            where T : BaseTypeDeclarationSyntax
        {
            var modified = baseTypeDeclarationSyntax
                .WithOpenBraceToken(Instances.SyntaxFactory.OpenBrace(indentation))
                .WithCloseBraceToken(Instances.SyntaxFactory.CloseBrace(indentation))
                ;

            var output = modified as T;
            return output;
        }

        public static T WithCloseBrace<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList indentation)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithCloseBraceToken(Instances.SyntaxFactory.CloseBrace(indentation))
                as T;

            return output;
        }

        public static T WithCloseBrace2<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList leadingWhitespace, bool appendNewLine = false)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithCloseBraceToken(Instances.SyntaxFactory.CloseBrace2(leadingWhitespace, appendNewLine))
                as T;

            return output;
        }

        public static T WithOpenBrace<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList indentation)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithOpenBraceToken(Instances.SyntaxFactory.OpenBrace(indentation))
                as T;

            return output;
        }

        public static T WithOpenBrace2<T>(this T baseTypeDeclarationSyntax,
            SyntaxTriviaList leadingWhitespace, bool prependNewLine = true)
            where T : BaseTypeDeclarationSyntax
        {
            var output = baseTypeDeclarationSyntax
                .WithOpenBraceToken(Instances.SyntaxFactory.OpenBrace2(leadingWhitespace, prependNewLine))
                as T;

            return output;
        }
    }
}
