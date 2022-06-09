using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class BaseTypeDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Chooses new line indentation as the default.
        /// </summary>
        public static TNode EnsureHasBraces<TNode>(this TNode node)
            where TNode : BaseTypeDeclarationSyntax
        {
            var output = node.EnsureHasBraces(
                SyntaxTriviaListHelper.NewLine());

            return output;
        }

        /// <inheritdoc cref="EnsureHasBraces{TNode}(TNode)"/>
        public static TNode EnsureHasCloseBrace<TNode>(this TNode node)
            where TNode : BaseTypeDeclarationSyntax
        {
            var output = node.EnsureHasCloseBrace(
                SyntaxTriviaListHelper.NewLine());

            return output;
        }

        /// <inheritdoc cref="EnsureHasBraces{TNode}(TNode)"/>
        public static TNode EnsureHasOpenBrace<TNode>(this TNode node)
            where TNode : BaseTypeDeclarationSyntax
        {
            var output = node.EnsureHasOpenBrace(
                SyntaxTriviaListHelper.NewLine());

            return output;
        }

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
                var closeBrace = SyntaxFactoryHelper.CloseBrace()
                    .WithLeadingTrivia(indentation)
                    ;

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
                var openBrace = SyntaxFactoryHelper.OpenBrace()
                    .WithLeadingTrivia(indentation)
                    ;

                outputNode = outputNode.WithOpenBraceToken(openBrace) as TNode;
            }

            return outputNode;
        }

        public static string GetTypeName(this BaseTypeDeclarationSyntax baseType)
        {
            var output = baseType.Identifier.Text;
            return output;
        }

        public static WasFound<BaseTypeSyntax[]> HasBaseTypes(this BaseTypeDeclarationSyntax baseTypeDeclarationSyntax)
        {
            var hasBaseTypesList = baseTypeDeclarationSyntax.HasBaseTypesList();
            if (!hasBaseTypesList)
            {
                return WasFound.NotFound<BaseTypeSyntax[]>();
            }

            var output = WasFound.Found(
                baseTypeDeclarationSyntax.BaseList.Types.ToArray());

            return output;
        }

        public static WasFound<BaseListSyntax> HasBaseTypesList(this BaseTypeDeclarationSyntax baseTypeDeclarationSyntax)
        {
            var output = WasFound.From(baseTypeDeclarationSyntax.BaseList);
            return output;
        }

        public static bool HasBraces(this BaseTypeDeclarationSyntax baseType)
        {
            var hasOpenBrace = baseType.HasOpenBrace();
            var hasCloseBrace = baseType.HasCloseBrace();

            var output = hasOpenBrace && hasCloseBrace;
            return output;
        }

        public static bool HasCloseBrace(this BaseTypeDeclarationSyntax baseType)
        {
            var output = !baseType.CloseBraceToken.IsMissing;
            return output;
        }

        public static bool HasOpenBrace(this BaseTypeDeclarationSyntax baseType)
        {
            var output = !baseType.OpenBraceToken.IsMissing;
            return output;
        }

        public static bool IsStatic(this BaseTypeDeclarationSyntax type)
        {
            var isStatic = type.Modifiers
                .Where(xToken => xToken.IsKind(SyntaxKind.StaticKeyword))
                .Any();

            return isStatic;
        }

        public static void VerifyHasBraces(this BaseTypeDeclarationSyntax typeDeclaration)
        {
            var hasBraces = typeDeclaration.HasBraces();
            if (!hasBraces)
            {
                throw new Exception($"No open or close brace found for type '{typeDeclaration.GetTypeName()}'.");
            }
        }

        public static T WithoutBaseList<T>(this T baseType)
            where T : BaseTypeDeclarationSyntax
        {
            BaseListSyntax emptyBaseList = null;

            var output = baseType.WithBaseList(emptyBaseList) as T;
            return output;
        }

        public static T WithoutCloseBraceToken<T>(this T baseType)
            where T : BaseTypeDeclarationSyntax
        {
            var noCloseBraceToken = SyntaxFactoryHelper.None();

            var output = baseType.WithCloseBraceToken(noCloseBraceToken) as T;
            return output;
        }

        public static T WithoutOpenBraceToken<T>(this T baseType)
            where T : BaseTypeDeclarationSyntax
        {
            var noOpenBraceToken = SyntaxFactoryHelper.None();

            var output = baseType.WithOpenBraceToken(noOpenBraceToken) as T;
            return output;
        }

        public static T WithoutSemicolonToken<T>(this T baseType)
            where T : BaseTypeDeclarationSyntax
        {
            var noSemicolonToken = SyntaxFactoryHelper.None();

            var output = baseType.WithSemicolonToken(noSemicolonToken) as T;
            return output;
        }
    }
}
