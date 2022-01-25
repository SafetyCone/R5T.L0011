using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class BlockSyntaxExtensions
    {
        public static BlockSyntax AddStatementsEnsuringAfterOpenAndBeforeCloseBraceIndentation(this BlockSyntax block,
            StatementSyntax[] statements,
            SyntaxTriviaList statementIndentation,
            SyntaxTriviaList closeBraceIndentation)
        {
            // Return if no work to do.
            if(!block.Statements.Any() && !statements.Any())
            {
                return block;
            }

            var outputBlock = block.AddStatements(statements);

            // As of now, there is guaranteed to be at least one statement.
            var firstStatement = outputBlock.Statements.First();

            // Set the open brace-to-first statement separation.
            outputBlock = outputBlock.SetSeparatingWhitespaceBetweenDescendents(
                outputBlock.OpenBraceToken,
                firstStatement,
                statementIndentation);

            var lastStatement = outputBlock.Statements.Last();

            // Set the last statement-to-close brace separation.
            outputBlock = outputBlock.SetSeparatingWhitespaceBetweenDescendents(
                lastStatement,
                outputBlock.CloseBraceToken,
                closeBraceIndentation);

            return outputBlock;
        }

        public static int GetIndexOfStatement(this BlockSyntax block,
            StatementSyntax statementSyntax)
        {
            var indexOfStatement = block.Statements.IndexOf(statementSyntax);
            if (IndexHelper.IsNotFound(indexOfStatement))
            {
                throw new Exception("Statement not found in block statements.");
            }

            return indexOfStatement;
        }

        public static BlockSyntax InsertStatementBefore(this BlockSyntax block,
            StatementSyntax insertBeforeStatement,
            StatementSyntax statement)
        {
            var indexOfInsertBeforeStatement = block.GetIndexOfStatement(insertBeforeStatement);

            var existingStatements = block.Statements;

            var newStatements = existingStatements
                .Take(indexOfInsertBeforeStatement)
                .Append(statement)
                .Concat(existingStatements.Skip(indexOfInsertBeforeStatement))
                .Now();

            var outputBlock = block.WithStatements(newStatements.ToSyntaxList());
            return outputBlock;
        }

        public static BlockSyntax InsertStatementsBefore(this BlockSyntax block,
            StatementSyntax insertBeforeStatement,
            StatementSyntax[] statements,
            bool addLineSeparationToInsertBeforeStatement = false)
        {
            // Ensure the statement was found.
            // Do this before returning even if not found.
            var indexOfInsertBeforeStatement = block.GetIndexOfStatement(insertBeforeStatement);

            // Ensure there is at least one statment to add.
            if (!statements.Any())
            {
                return block;
            }

            var outputBlock = block;

            // Note: will not change index of statement.
            if (addLineSeparationToInsertBeforeStatement)
            {
                var newInsertBeforeStatement = insertBeforeStatement.PrependBlankLine();

                outputBlock = outputBlock.ReplaceNode(insertBeforeStatement, newInsertBeforeStatement);
            }

            var existingStatements = outputBlock.Statements;

            var newStatements = existingStatements
                .Take(indexOfInsertBeforeStatement)
                .Concat(statements)
                .Concat(existingStatements.Skip(indexOfInsertBeforeStatement))
                .Now();

            // Now set new statements.
            outputBlock = outputBlock.WithStatements(newStatements.ToSyntaxList());
            
            return outputBlock;
        }

        public static BlockSyntax InsertStatementsBeforeEnsuringAfterOpenAndBeforeCloseBraceIndentation(this BlockSyntax block,
            StatementSyntax insertBeforeStatement,
            StatementSyntax[] statements,
            SyntaxTriviaList statementIndentation,
            SyntaxTriviaList closeBraceIndentation)
        {
            var indexOfInsertBeforeStatement = block.GetIndexOfStatement(insertBeforeStatement);

            var existingStatements = block.Statements;

            var newStatements = existingStatements
                .Take(indexOfInsertBeforeStatement)
                .Concat(statements)
                .Concat(existingStatements.Skip(indexOfInsertBeforeStatement))
                .Now();

            var output = block.WithStatementsEnsuringAfterOpenAndBeforeCloseBraceIndentation(
                newStatements,
                statementIndentation,
                closeBraceIndentation);

            return output;
        }

        public static BlockSyntax WithStatementsEnsuringAfterOpenAndBeforeCloseBraceIndentation(this BlockSyntax block,
            StatementSyntax[] statements,
            SyntaxTriviaList statementIndentation,
            SyntaxTriviaList closeBraceIndentation)
        {
            // Return if no work to do.
            if (!block.Statements.Any() && !statements.Any())
            {
                return block;
            }

            // As of now, there is at least one statement.
            var outputBlock = block.WithStatements(statements.ToSyntaxList());

            // As of now, there is guaranteed to be at least one statement.
            var firstStatement = statements.First();

            // Set the open brace-to-first statement separation.
            outputBlock = outputBlock.SetSeparatingWhitespaceBetweenDescendents(
                outputBlock.OpenBraceToken,
                firstStatement,
                statementIndentation);

            var lastStatement = outputBlock.Statements.Last();

            // Set the last statement-to-close brace separation.
            outputBlock = outputBlock.SetSeparatingWhitespaceBetweenDescendents(
                lastStatement,
                outputBlock.CloseBraceToken,
                closeBraceIndentation);

            return outputBlock;
        }
    }
}
