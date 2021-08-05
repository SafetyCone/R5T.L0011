using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.L0011.X001;

using Instances = R5T.L0011.X001.Instances;


namespace System
{
    public static class BaseMethodDeclarationSyntaxExtensions
    {
        public static T AddDocumentation<T>(this T baseMethod,
            DocumentationCommentTriviaSyntax documentationComment)
            where T : BaseMethodDeclarationSyntax
        {
            var output = baseMethod
                .AddLeadingLeadingTrivia(
                    SyntaxFactory.Trivia(documentationComment));

            return output;
        }

        public static IEnumerable<ParameterSyntax> GetParameters(this BaseMethodDeclarationSyntax method)
        {
            var output = method.ParameterList.Parameters
                .Select(parameter => parameter)
                ;

            return output;
        }

        public static IEnumerable<string> GetParameterNames(this BaseMethodDeclarationSyntax method)
        {
            var output = method.GetParameters()
                .Select(parameter => parameter.Identifier.ToString())
                ;

            return output;
        }

        public static WasFound<ParameterSyntax> HasParameter(this BaseMethodDeclarationSyntax method,
            string parameterName)
        {
            var parameterOrDefault = method.GetParameters()
                .Where(parameter => parameter.Identifier.Text == parameterName)
                .SingleOrDefault()
                ;

            var wasFound = WasFound.From(parameterOrDefault);
            return wasFound;
        }

        public static ParameterSyntax GetParameter(this BaseMethodDeclarationSyntax method,
            string parameterName)
        {
            var parameterWasFound = method.HasParameter(parameterName);
            if(!parameterWasFound)
            {
                var message = Instances.ExceptionMessage.ParameterNotFound(parameterName);

                throw new Exception(message);
            }

            return parameterWasFound.Result;
        }

        public static T RemoveParameter<T>(this T method,
            ParameterSyntax parameter)
            where T : BaseMethodDeclarationSyntax
        {
            var oldParameterList = method.ParameterList;
            var newParameterList = oldParameterList.WithParameters(oldParameterList.Parameters.Remove(parameter));

            var output = method.ReplaceNode(oldParameterList, newParameterList);
            return output;
        }

        public static IEnumerable<StatementSyntax> GetStatements(this BaseMethodDeclarationSyntax method)
        {
            var output = method.Body.Statements;
            return output;
        }

        /// <summary>
        /// If you can recreate the text of the statement, find the statement with that text.
        /// </summary>
        public static WasFound<StatementSyntax> HasStatement(this BaseMethodDeclarationSyntax method,
            string statementText)
        {
            var statementOrDefault = method.GetStatements()
                .Where(statement =>
                {
                    var currentStatementText = statement.GetTextAsString();

                    var output = currentStatementText == statementText;
                    return output;
                })
                .SingleOrDefault();

            var wasFound = WasFound.From(statementOrDefault);
            return wasFound;
        }

        /// <summary>
        /// If you can recreate the text of the statement, find the statement with that text.
        /// </summary>
        public static StatementSyntax GetStatement(this BaseMethodDeclarationSyntax method,
            string statementText)
        {
            var statementWasFound = method.HasStatement(statementText);
            if(!statementWasFound)
            {
                var message = Instances.ExceptionMessage.StatementNotFound(statementText);

                throw new Exception(message);
            }

            return statementWasFound.Result;
        }

        public static T RemoveStatement<T>(this T method,
            StatementSyntax statement)
            where T : BaseMethodDeclarationSyntax
        {
            var newBody = method.Body.WithStatements(method.Body.Statements.Remove(statement));

            var output = method.WithBody(newBody);
            return output as T;
        }
    }
}
