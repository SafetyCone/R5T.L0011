using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;

using SyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        public static SyntaxAnnotation Annotation(this ISyntaxFactory _)
        {
            var output = new SyntaxAnnotation();
            return output;
        }

        public static AttributeSyntax Attribute(this ISyntaxFactory _,
            string name)
        {
            var nameSyntax = _.Name(name);

            var output = SyntaxFactory.Attribute(nameSyntax);
            return output;
        }

        public static AttributeListSyntax AttributeList(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.AttributeList();
            return output;
        }

        public static ArgumentSyntax Argument(this ISyntaxFactory _, ExpressionSyntax expression)
        {
            var output = SyntaxFactory.Argument(expression);
            return output;
        }

        public static ArgumentSyntax Argument(this ISyntaxFactory syntaxFactory, string variableName)
        {
            var output = SyntaxFactory.Argument(syntaxFactory.IdentifierName(variableName));
            return output;
        }

        public static BaseTypeSyntax BaseType(this ISyntaxFactory syntaxFactory, string typeName)
        {
            var type = syntaxFactory.Type(typeName);

            var output = SyntaxFactory.SimpleBaseType(type);
            return output;
        }

        public static BlockSyntax Block(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.Block();
            return output;
        }

        public static BlockSyntax Body(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.Block();
            return output;
        }

        public static BlockSyntax Body(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList indentation)
        {
            var output = syntaxFactory.Body()
                .Indent(indentation)
                ;

            return output;
        }

        public static ClassDeclarationSyntax Class(this ISyntaxFactory _, string name)
        {
            return SyntaxFactory.ClassDeclaration(name);
        }

        public static SyntaxToken CloseBrace(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.Token(SyntaxKind.CloseBraceToken);
            return output;
        }

        public static SyntaxToken CloseBrace(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList indentation)
        {
            var output = syntaxFactory.CloseBrace()
                .Indent(indentation)
                ;

            return output;
        }

        public static SyntaxToken CloseBrace2(this ISyntaxFactory syntaxFactory, SyntaxTriviaList leadingWhitespace, bool appendNewLine = false)
        {
            var lineLeadingWhitespace = leadingWhitespace.GetNewLineLeadingWhitespace();

            var endingWhitespace = appendNewLine
                ? syntaxFactory.NewLine()
                : syntaxFactory.EmptyTrivia();

            var output = SyntaxFactory.Token(SyntaxKind.CloseBraceToken)
                .WithLeadingTrivia(lineLeadingWhitespace)
                .AddTrailingTrailingTrivia(endingWhitespace)
                ;

            return output;
        }

        /// <summary>
        /// Create a comment given the entire text of the comment (including the leading comment marks like "//", or the wrapping marks like "/*" and "*/" for a multi-line comment).
        /// </summary>
        public static SyntaxTrivia Comment_EntireText(this ISyntaxFactory _, string entireText)
        {
            var output = SyntaxFactory.Comment(entireText);
            return output;
        }

        // No Comment() since that name default will be used for a method that can handle non-entire text.

        public static CompilationUnitSyntax CompilationUnit(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.CompilationUnit();
            return output;
        }

        public static ConstructorDeclarationSyntax Constructor(this ISyntaxFactory _, SyntaxToken typeNameToken)
        {
            var output = SyntaxFactory.ConstructorDeclaration(typeNameToken);
            return output;
        }

        public static ConstructorDeclarationSyntax Constructor(this ISyntaxFactory _, string typeName)
        {
            var output = SyntaxFactory.ConstructorDeclaration(typeName);
            return output;
        }

        public static SyntaxTrivia EndOfLine(this ISyntaxFactory _, string endOfLineText)
        {
            var output = SyntaxFactory.EndOfLine(endOfLineText);
            return output;
        }

        public static SyntaxTrivia EndOfLine_Environment(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.EndOfLine(Environment.NewLine);
            return output;
        }

        /// <summary>
        /// Selects <see cref="EndOfLine_Environment(ISyntaxFactory)"/> as the default end of line.
        /// </summary>
        public static SyntaxTrivia EndOfLine(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.EndOfLine_Environment();
            return output;
        }

        public static EqualsValueClauseSyntax EqualsValueClause(this ISyntaxFactory _,
            ExpressionSyntax expression)
        {
            var output = SyntaxFactory.EqualsValueClause(expression);
            return output;
        }

        public static ExpressionStatementSyntax ExpressionStatement(this ISyntaxFactory _,
            ExpressionSyntax expression)
        {
            var output = SyntaxFactory.ExpressionStatement(expression);
            return output;
        }

        public static GenericNameSyntax GenericName(this ISyntaxFactory _,
            string name)
        {
            var output = SyntaxFactory.GenericName(name);
            return output;
        }

        public static SyntaxTriviaList GetInitialLineLeadingWhitespace(this ISyntaxFactory syntaxFactory)
        {
            var output = new SyntaxTriviaList(
                syntaxFactory.NewLine());

            return output;
        }

        public static SyntaxTriviaList GetInitialLeadingWhitespace(this ISyntaxFactory _)
        {
            var output = new SyntaxTriviaList();
            return output;
        }

        public static SyntaxToken Identifier(this ISyntaxFactory _, SyntaxKind syntaxKind, string text, string valueText)
        {
            var output = SyntaxFactory.Identifier(
                SyntaxFactory.TriviaList(),
                syntaxKind,
                text,
                valueText,
                SyntaxFactory.TriviaList());

            return output;
        }

        public static SyntaxToken Identifier(this ISyntaxFactory syntaxFactory, SyntaxKind syntaxKind, string text)
        {
            var output = syntaxFactory.Identifier(syntaxKind, text, text);
            return output;
        }

        public static SyntaxToken Identifier(this ISyntaxFactory _, string text)
        {
            return SyntaxFactory.Identifier(text);
        }

        public static IdentifierNameSyntax IdentifierName(this ISyntaxFactory _, string text)
        {
            return SyntaxFactory.IdentifierName(text);
        }

        public static IdentifierNameSyntax IdentifierName(this ISyntaxFactory _, SyntaxToken identifier)
        {
            return SyntaxFactory.IdentifierName(identifier);
        }

        public static InterfaceDeclarationSyntax Interface(this ISyntaxFactory _, string name)
        {
            return SyntaxFactory.InterfaceDeclaration(name);
        }

        public static InvocationExpressionSyntax Invocation(this ISyntaxFactory _, ExpressionSyntax expression)
        {
            var output = SyntaxFactory.InvocationExpression(expression);
            return output;
        }

        public static InvocationExpressionSyntax Invocation(this ISyntaxFactory _, ExpressionSyntax expression, params ArgumentSyntax[] arguments)
        {
            var output = SyntaxFactory.InvocationExpression(expression)
                .AddArgumentListArguments(arguments);

            return output;
        }

        public static LocalDeclarationStatementSyntax LocalDeclaration(this ISyntaxFactory _,
            VariableDeclarationSyntax variableDeclaration)
        {
            var output = SyntaxFactory.LocalDeclarationStatement(variableDeclaration);
            return output;
        }

        public static MemberAccessExpressionSyntax MemberAccess(this ISyntaxFactory _,
            ExpressionSyntax memberedExpression,
            SimpleNameSyntax memberName)
        {
            var output = SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                memberedExpression,
                memberName);

            return output;
        }

        public static MethodDeclarationSyntax Method(this ISyntaxFactory _, string name, TypeSyntax returnType)
        {
            var output = SyntaxFactory.MethodDeclaration(returnType, name);
            return output;
        }

        public static MethodDeclarationSyntax Method(this ISyntaxFactory syntaxFactory, string name, string returnType)
        {
            var returnTypeSyntax = syntaxFactory.Type(returnType);

            var output = syntaxFactory.Method(name, returnTypeSyntax);
            return output;
        }

        public static IdentifierNameSyntax Name(this ISyntaxFactory _, string name)
        {
            var output = SyntaxFactory.IdentifierName(name);
            return output;
        }

        public static string NameOfType(this ISyntaxFactory _, Type type)
        {
            return type.Name;
        }

        public static string NameOfType<T>(this ISyntaxFactory syntaxFactory)
        {
            var type = typeof(T);

            return syntaxFactory.NameOfType(type);
        }

        public static NamespaceDeclarationSyntax Namespace(this ISyntaxFactory syntaxFactory, string namespaceName)
        {
            var namespaceIdentifierName = syntaxFactory.Name(namespaceName);

            var output = SyntaxFactory.NamespaceDeclaration(namespaceIdentifierName);
            return output;
        }

        public static SyntaxTrivia NewLine(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.EndOfLine();
            return output;
        }

        public static SyntaxToken OpenBrace(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.Token(SyntaxKind.OpenBraceToken);
            return output;
        }

        public static SyntaxToken OpenBrace(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList indentation)
        {
            var output = syntaxFactory.OpenBrace()
                .Indent(indentation)
                ;

            return output;
        }

        public static SyntaxToken OpenBrace2(this ISyntaxFactory _, SyntaxTriviaList leadingWhitespace, bool prependNewLine = true)
        {
            var lineLeadingWhitespace = prependNewLine
                ? leadingWhitespace.GetNewLineLeadingWhitespace()
                : leadingWhitespace;

            var output = SyntaxFactory.Token(SyntaxKind.OpenBraceToken)
                .WithLeadingTrivia(lineLeadingWhitespace);

            return output;
        }

        public static ParameterSyntax Parameter(this ISyntaxFactory syntaxFactory, string name, TypeSyntax type)
        {
            var nameToken = syntaxFactory.Identifier(name);

            var output = SyntaxFactory.Parameter(nameToken)
                .WithType(type)
                ;

            return output;
        }

        public static ParameterSyntax Parameter(this ISyntaxFactory syntaxFactory, string name, string typeName)
        {
            var type = syntaxFactory.Type(typeName);

            return syntaxFactory.Parameter(name, type);
        }

        public static ParameterSyntax Parameter<T>(this ISyntaxFactory syntaxFactory, string name)
        {
            var typeName = syntaxFactory.Type<T>();

            return syntaxFactory.Parameter(name, typeName);
        }

        public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambda(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.ParenthesizedLambdaExpression();
            return output;
        }

        public static PropertyDeclarationSyntax Property(this ISyntaxFactory syntaxFactory, string name, string typeName)
        {
            var typeIdentifierName = syntaxFactory.Type(typeName);

            var output = SyntaxFactory.PropertyDeclaration(typeIdentifierName, name);
            return output;
        }

        public static PropertyDeclarationSyntax Property<T>(this ISyntaxFactory syntaxFactory, string name)
        {
            var typeName = syntaxFactory.NameOfType<T>();

            var output = syntaxFactory.Property(name, typeName);
            return output;
        }

        public static ReturnStatementSyntax Return(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.ReturnStatement();
            return output;
        }

        public static ReturnStatementSyntax Return(this ISyntaxFactory _,
            ExpressionSyntax expression)
        {
            var output = SyntaxFactory.ReturnStatement(expression);
            return output;
        }

        public static ReturnStatementSyntax Return(this ISyntaxFactory syntaxFactory,
            string variableName)
        {
            var output = SyntaxFactory.ReturnStatement(syntaxFactory.IdentifierName(variableName));
            return output;
        }

        public static SyntaxList<TNode> SyntaxList<TNode>(this ISyntaxFactory _,
            IEnumerable<TNode> syntaxNodes)
            where TNode : SyntaxNode
        {
            var output = new SyntaxList<TNode>(syntaxNodes);
            return output;
        }

        public static SyntaxToken Token(this ISyntaxFactory _, SyntaxKind kind)
        {
            var output = SyntaxFactory.Token(kind);
            return output;
        }

        public static IdentifierNameSyntax Type<T>(this ISyntaxFactory syntaxFactory)
        {
            var nameOfType = syntaxFactory.NameOfType<T>();

            return syntaxFactory.Name(nameOfType);
        }

        public static TypeSyntax Type(this ISyntaxFactory _, string name)
        {
            return SyntaxFactory.IdentifierName(name);
        }

        public static TypeParameterConstraintClauseSyntax TypeParameterConstraint(this ISyntaxFactory syntaxFactory, string typeParameterName, string constraintTypeName)
        {
            var typeConstraint = SyntaxFactory.TypeConstraint(syntaxFactory.Type(constraintTypeName));

            var output = SyntaxFactory.TypeParameterConstraintClause(
                syntaxFactory.IdentifierName(typeParameterName))
                .AddConstraints(typeConstraint)
                .NormalizeWhitespace()
                ;

            return output;
        }

        public static UsingDirectiveSyntax Using(this ISyntaxFactory syntaxFactory, string namespaceName)
        {
            var name = syntaxFactory.Name(namespaceName);

            var output = syntaxFactory.Using(name);
            return output;
        }

        public static UsingDirectiveSyntax Using(this ISyntaxFactory syntaxFactory, 
            string aliasForName,
            string name)
        {
            var nameSyntax = syntaxFactory.Name(name);

            var aliasSyntax = SyntaxFactory.NameEquals(aliasForName);

            var output = SyntaxFactory.UsingDirective(aliasSyntax, nameSyntax);
            return output;
        }

        public static UsingDirectiveSyntax Using(this ISyntaxFactory _, NameSyntax name)
        {
            var output = SyntaxFactory.UsingDirective(name)
                .NormalizeWhitespace()
                .WithEndOfLine();

            return output;
        }

        public static SyntaxTrivia Whitespace(this ISyntaxFactory _, string whitespaceText)
        {
            var output = SyntaxFactory.Whitespace(whitespaceText);
            return output;
        }

        public static VariableDeclaratorSyntax VariableDeclarator(this ISyntaxFactory _, SyntaxToken identifier)
        {
            var output = SyntaxFactory.VariableDeclarator(identifier);
            return output;
        }

        public static VariableDeclaratorSyntax VariableDeclarator(this ISyntaxFactory syntaxFactory, string name)
        {
            var output = SyntaxFactory.VariableDeclarator(syntaxFactory.Identifier(name));
            return output;
        }
    }
}
