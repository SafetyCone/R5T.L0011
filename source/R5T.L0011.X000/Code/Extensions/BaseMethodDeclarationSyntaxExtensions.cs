using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class BaseMethodDeclarationSyntaxExtensions
    {
        public static T AddBodyStatements_WithIndentation<T>(this T method,
            StatementSyntax[] statements,
            SyntaxTriviaList indentation)
            where T : BaseMethodDeclarationSyntax
        {
            var indentedStatements = statements
                .IndentStartLine(indentation)
                .Now();

            var output = method.AddBodyStatements_Latest(indentedStatements);
            return output;
        }

        public static T AddBodyStatements_Latest<T>(this T method,
            StatementSyntax[] statements)
            where T : BaseMethodDeclarationSyntax
        {
            var output = method
                .AddBodyStatements(statements)
                .PerformAddBodyStatementsPostAdditionFormatting();

            return output as T;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T PerformAddBodyStatementsPostAdditionFormatting<T>(this T method)
            where T : BaseMethodDeclarationSyntax
        {
            var hasBody = method.HasBody();
            if(hasBody)
            {
                var hasStatements = hasBody.Result.Statements.Any();
                if(hasStatements)
                {
                    var firstStatement = hasBody.Result.Statements.First();

                    // If the first statement has no new line, prepend one.
                    var hasLeadingTrivia = firstStatement.HasLeadingTrivia;
                    if(hasLeadingTrivia)
                    {
                        var leadingTrivia = firstStatement.GetLeadingTrivia();

                        var hasAnyNewLine = leadingTrivia.HasAnyNewLines();
                        if(!hasAnyNewLine)
                        {
                            var newFirstStatement = firstStatement.AddLeadingLeadingTrivia(
                                SyntaxTriviaHelper.NewLine());

                            method = method.ReplaceNode_Better(
                                firstStatement,
                                newFirstStatement);
                        }
                    }
                }
            }

            return method;
        }

        public static ParameterSyntax FirstParameterOrDefault(this BaseMethodDeclarationSyntax method)
        {
            var output = method.GetParameters_Enumerable().FirstOrDefault();
            return output;
        }

        public static BlockSyntax GetBody(this BaseMethodDeclarationSyntax method)
        {
            var hasBody = method.HasBody();
            if(!hasBody)
            {
                throw new Exception("No body (block body) found on method.");
            }

            return hasBody.Result;
        }

        public static ArrowExpressionClauseSyntax GetExpressionBody(this BaseMethodDeclarationSyntax method)
        {
            var hasBody = method.HasExpressionBody();
            if (!hasBody)
            {
                throw new Exception("No expresskon body found on method.");
            }

            return hasBody.Result;
        }

        /// <summary>
        /// Returns the first containing type for the method.
        /// </summary>
        public static TypeDeclarationSyntax GetContainingType(this BaseMethodDeclarationSyntax method)
        {
            var hasContainingType = method.HasContainingType();
            if(!hasContainingType)
            {
                throw new Exception("Containing type not found.");
            }

            return hasContainingType.Result;
        }

        public static IEnumerable<ParameterSyntax> GetParameters_Enumerable(this BaseMethodDeclarationSyntax method)
        {
            var output = method.ParameterList.Parameters;
            return output;
        }

        public static ParameterSyntax[] GetParameters(this BaseMethodDeclarationSyntax method)
        {
            var output = method.GetParameters_Enumerable().ToArray();
            return output;
        }

        /// <summary>
        /// Gets the paramter type method name, i.e. Method(string, string, int).
        /// </summary>
        public static string GetParameterTypedMethodName(this BaseMethodDeclarationSyntax method,
            string simpleMethodName)
        {
            var parameterTypeFragments = method.GetParameterTypeFragments();

            var output = $"{simpleMethodName}{Strings.OpenParenthesis}{String.Join(Strings.CommaSeparatedListSpacedSeparator, parameterTypeFragments)}{Strings.CloseParenthesis}";
            return output;
        }

        public static string[] GetParameterTypeFragments(this BaseMethodDeclarationSyntax method)
        {
            var parameters = method.GetParameters();

            var output = parameters
                .Select(xParameter => xParameter.Type.GetTypeNameFragment())
                .Now();

            return output;
        }

        public static WasFound<BlockSyntax> HasBody(this BaseMethodDeclarationSyntax method)
        {
            var output = WasFound.From(method.Body);
            return output;
        }

        public static WasFound<ArrowExpressionClauseSyntax> HasExpressionBody(this BaseMethodDeclarationSyntax method)
        {
            var expressionBody = method.ExpressionBody;

            var output = WasFound.From(expressionBody);
            return output;
        }

        /// <summary>
        /// Returns the first containing type for the method, if the method has one.
        /// </summary>
        public static WasFound<TypeDeclarationSyntax> HasContainingType(this BaseMethodDeclarationSyntax method)
        {
            // Get parents, 
            var parents = method.GetParentsInsideToOutside_Enumerable();

            foreach (var parent in parents)
            {
                if(parent is TypeDeclarationSyntax typeDeclaration)
                {
                    return WasFound.From(typeDeclaration);
                }
            }

            return WasFound.NotFound<TypeDeclarationSyntax>();
        }

        public static WasFound<ParameterSyntax[]> HasParameters(this BaseMethodDeclarationSyntax method)
        {
            var parameters = method.GetParameters();

            return WasFound.FromArray(parameters);
        }

        public static T SetBracesLineIndentation<T>(this T method,
            SyntaxTriviaList indentation)
            where T : BaseMethodDeclarationSyntax
        {
            var body = method.GetBody();

            method = method.WithBody(
                body.SetBracesLineIndentation(
                    indentation))
                as T;

            return method;
        }

        public static T SetBracesLineIndentation<T>(this T method,
            SyntaxToken openBraceToken,
            SyntaxToken closeBraceToken,
            SyntaxTriviaList indentation)
            where T : BaseMethodDeclarationSyntax
        {
            var body = method.GetBody();

            method = method.WithBody(
                body.SetBracesLineIndentation(
                    openBraceToken,
                    closeBraceToken,
                    indentation))
                as T;

            return method;
        }
    }
}
