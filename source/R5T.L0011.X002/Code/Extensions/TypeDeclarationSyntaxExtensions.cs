using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;


namespace System
{
    public static class TypeDeclarationSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static T AddMethodWithoutBody<T>(this T typeDeclaration,
            string name, string returnType,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithLineLeadingWhitespace<MethodDeclarationSyntax> modifier)
            where T : TypeDeclarationSyntax
        {
            var indentedLeadingWhitespace = leadingWhitespace.IndentByTab();

            var method = SyntaxFactory.Method(name, returnType)
                .NormalizeWhitespace()
                //.WithOpenBraceToken(SyntaxFactory.OpenBrace(leadingWhitespace, false))
                //.WithCloseBraceToken(SyntaxFactory.CloseBrace(leadingWhitespace))
                .ModifyWith(indentedLeadingWhitespace, modifier)
                ;

            var output = typeDeclaration.AddMembers(method) as T;
            return output;
        }

        public static T AddMethod<T>(this T typeDeclaration,
            string name, string returnType,
            SyntaxTriviaList leadingWhitespace,
            Modifier<MethodDeclarationSyntax> methodModifier = default,
            ModifierWithLineLeadingWhitespace<MethodDeclarationSyntax> methodWhitespaceModifier = default,
            ModifierWithLineLeadingWhitespace<BlockSyntax> bodyModifier = default)
            where T : TypeDeclarationSyntax
        {
            var indentedLeadingWhitespace = leadingWhitespace.IndentByTab();

            var method = SyntaxFactory.Method(name, returnType)
                .ModifyWith(methodModifier)
                .NormalizeWhitespace()
                .WithLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace())
                .ModifyWith(indentedLeadingWhitespace, methodWhitespaceModifier)
                //.WithOpenBraceToken(SyntaxFactory.OpenBrace(leadingWhitespace, false))
                //.WithCloseBraceToken(SyntaxFactory.CloseBrace(leadingWhitespace))
                ;

            if (bodyModifier is object)
            {
                method = method.ModifyWith(theMethod =>
                    {
                        var body = SyntaxFactory.Body();

                        var modifiedBody = body.ModifyWith(indentedLeadingWhitespace, bodyModifier);

                        return theMethod.WithBody(modifiedBody);
                    });
            }

            var output = typeDeclaration.AddMembers(method) as T;
            return output;
        }

        public static T AddProperty<T>(this T typeDeclaration, string name, string typeName,
            SyntaxTriviaList outerLeadingWhitespace,
            ModifierWithLineLeadingWhitespace<PropertyDeclarationSyntax> premodifier = default,
            ModifierWithLineLeadingWhitespace<PropertyDeclarationSyntax> modifier = default)
            where T : TypeDeclarationSyntax
        {
            var property = SyntaxFactory.Property(name, typeName)
                .ModifyWith(outerLeadingWhitespace, premodifier)
                .NormalizeWhitespace()
                .WithLeadingTrivia(outerLeadingWhitespace.GetNewLineLeadingWhitespace())
                .ModifyWith(outerLeadingWhitespace, modifier)
                ;

            var output = typeDeclaration.AddMembers(property) as T;
            return output;
        }

        public static T WithStandardWhitespace<T>(this T typeDeclaration,
            SyntaxTriviaList outerLeadingWhitespace)
            where T : TypeDeclarationSyntax
        {
            var output = typeDeclaration
                .WithLineLeadingWhitespace(outerLeadingWhitespace)
                .WithOpenBrace(outerLeadingWhitespace)
                .WithCloseBrace(outerLeadingWhitespace)
                ;

            return output;
        }
    }
}
