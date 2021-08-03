using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;


namespace System
{
    public static class BaseMethodDeclarationSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static T AddInitialFormatting<T>(this T baseMethod,
            SyntaxTriviaList leadingWhitespace,
            bool prependBlankLine = true)
            where T : BaseMethodDeclarationSyntax
        {
            var output = baseMethod
                .AddInitialBody(leadingWhitespace)
                .AddLineStart2(leadingWhitespace)
                .PrependBlankLine(leadingWhitespace, prependBlankLine);

            return output;
        }

        public static T AddBody<T>(this T baseMethod,
            SyntaxTriviaList outerLeadingWhitespace,
            ModifierWithLineLeadingWhitespace<BlockSyntax> modifier = default)
            where T : BaseMethodDeclarationSyntax
        {
            var indentedWhitespace = outerLeadingWhitespace.IndentByTab();

            var body = SyntaxFactory.Body()
                    .WithOpenBraceToken(SyntaxFactory.OpenBrace2(outerLeadingWhitespace))
                    .WithCloseBraceToken(SyntaxFactory.CloseBrace2(outerLeadingWhitespace, true))
                    .ModifyWith(indentedWhitespace, modifier);

            var output = baseMethod.WithBody(body) as T;
            return output;
        }

        public static T AddBodyStatements<T>(this T method,
            SyntaxTriviaList leadingWhitespace,
            params StatementSyntax[] statements)
            where T : BaseMethodDeclarationSyntax
        {
            var adjustedStatements = statements
                .Select(x => x.AddLineStart2(leadingWhitespace))
                .ToArray();

            var output = method.AddBodyStatements(adjustedStatements) as T;
            return output;
        }

        public static T AddParameterWithModification<T>(this T method,
            string name, string typeName,
            SyntaxTriviaList outerLeadingWhitespace,
            ModifierWithLineLeadingWhitespace<ParameterSyntax> parameterModifier = default,
            ModifierWithLineLeadingWhitespace<ParameterSyntax> parameterWhitespaceModifier = default)
            where T : BaseMethodDeclarationSyntax
        {
            var parameter = SyntaxFactory.Parameter(name, typeName)
                .ModifyWith(outerLeadingWhitespace, parameterModifier)
                .NormalizeWhitespace()
                .ModifyWith(outerLeadingWhitespace, parameterWhitespaceModifier)
                ;

            var output = method.AddParameterListParameters(parameter) as T;
            return output;
        }
    }
}
