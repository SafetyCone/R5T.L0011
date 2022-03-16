using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class BaseTypeDeclarationSyntaxExtensions
    {
        public static string GetTypeName(this BaseTypeDeclarationSyntax baseType)
        {
            var output = baseType.Identifier.Text;
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
