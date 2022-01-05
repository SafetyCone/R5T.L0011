using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.L0011.T001;


namespace System
{
    public static class TypeDeclarationSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static T AddMembersWithLineSpacing<T>(this T typeDeclaration,
            MemberDeclarationSyntax[] members,
            bool addSpaceBeforeFirstMember)
            where T : TypeDeclarationSyntax
        {
            if(members.Length < 1)
            {
                return typeDeclaration;
            }

            var actualFirstMember = addSpaceBeforeFirstMember
                ? members.First().PrependBlankLine()
                : members.First()
                ;

            var actualMembers = EnumerableHelper.From(actualFirstMember)
                .Concat(members.SkipFirst()
                    .Select(xMember => xMember.PrependBlankLine()))
                .ToArray();

            var output = typeDeclaration.AddMembers(actualMembers) as T;
            return output;
        }

        public static T AddMembersWithLineSpacing<T>(this T typeDeclaration,
            MemberDeclarationSyntax[] members)
            where T : TypeDeclarationSyntax
        {
            // If we have members already, prepend a blank line to the first member, else do not.
            var addSpaceBeforeFirstMember = typeDeclaration.HasMembers();

            var output = typeDeclaration.AddMembersWithLineSpacing(members,
                addSpaceBeforeFirstMember);

            return output;
        }

        public static T AddMethodWithoutBody<T>(this T typeDeclaration,
            string name, string returnType,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithIndentationSynchronous<MethodDeclarationSyntax> modifier)
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
            ModifierSynchronous<MethodDeclarationSyntax> methodModifier = default,
            ModifierWithIndentationSynchronous<MethodDeclarationSyntax> methodWhitespaceModifier = default,
            ModifierWithIndentationSynchronous<BlockSyntax> bodyModifier = default)
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
            ModifierWithIndentationSynchronous<PropertyDeclarationSyntax> premodifier = default,
            ModifierWithIndentationSynchronous<PropertyDeclarationSyntax> modifier = default)
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
                .WithCloseBrace2(outerLeadingWhitespace)
                ;

            return output;
        }
    }
}
