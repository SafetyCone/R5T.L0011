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
            SyntaxTriviaList indentation)
            where T : BaseMethodDeclarationSyntax
        {
            var output = baseMethod
                .AddInitialBody(indentation)
                .Indent(indentation)
                ;

            return output;
        }

        public static T AddInitialFormatting2<T>(this T baseMethod,
            SyntaxTriviaList indentation,
            bool prependBlankLine = true)
            where T : BaseMethodDeclarationSyntax
        {
            var output = baseMethod
                .AddInitialBody(indentation)
                .Indent(indentation)
                .PrependBlankLine(indentation, prependBlankLine);

            return output;
        }

        public static T AddBody<T>(this T baseMethod,
            SyntaxTriviaList outerLeadingWhitespace,
            ModifierWithIndentation<BlockSyntax> modifier = default)
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
            ModifierWithIndentation<ParameterSyntax> parameterModifier = default,
            ModifierWithIndentation<ParameterSyntax> parameterWhitespaceModifier = default)
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
