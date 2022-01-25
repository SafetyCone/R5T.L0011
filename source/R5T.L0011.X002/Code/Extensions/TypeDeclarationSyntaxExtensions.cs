using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.L0011.T001;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class TypeDeclarationSyntaxExtensions
    {
        //public static T AddMembers<T>(this T typeDeclaration,
        //    MemberDeclarationSyntax[] members,
        //    bool addNewLineBeforeFirstMember)
        //    where T : TypeDeclarationSyntax
        //{
        //    // Only do work if there's work to do.
        //    if(members.Length < 1)
        //    {
        //        return typeDeclaration;
        //    }

        //    var actualFirstMember = addNewLineBeforeFirstMember
        //        ? members.First().PrependBlankLine()
        //        : members.First()
        //        ;

        //    var actualMembers = EnumerableHelper.From(actualFirstMember)
        //        .Concat(members.SkipFirst())
        //        .ToArray();

        //    var output = typeDeclaration.AddMembers(actualMembers) as T;
        //    return output;
        //}

        public static T AddMembersWithBlankLineSeparation<T>(this T typeDeclaration,
            MemberDeclarationSyntax[] members,
            bool addLineBeforeFirstMember)
            where T : TypeDeclarationSyntax
        {
            // Only do work if there's work to do.
            if (members.Length < 1)
            {
                return typeDeclaration;
            }

            var actualFirstMember = addLineBeforeFirstMember
                ? members.First().PrependBlankLine()
                : members.First()
                ;

            // Add lines between each member.
            var actualMembers = EnumerableHelper.From(actualFirstMember)
                .Concat(members
                    .SkipFirst()
                    .Select(xMember => xMember.PrependBlankLine()))
                .ToArray();

            var output = typeDeclaration.AddMembers(actualMembers) as T;
            return output;
        }

        public static T AddMembersWithBlankLineSeparation<T>(this T typeDeclaration,
            MemberDeclarationSyntax[] members)
            where T : TypeDeclarationSyntax
        {
            // Ensure there is at least one member.
            if (members.Length < 1)
            {
                return typeDeclaration;
            }

            // If members already exist, we will want to separate the first new member with a blank line.
            // If members don't already exists, we will not want to separate the first member with a blank line.
            // This is because the standard open brace has no trailing trivia and so cannot provide the new line.
            var addSpaceBeforeFirstMember = typeDeclaration.HasMembers();

            var output = typeDeclaration.AddMembersWithBlankLineSeparation(
                members,
                addSpaceBeforeFirstMember);

            return output;
        }

        public static T AddMethods<T>(this T typeDeclaration,
            params BaseMethodDeclarationSyntax[] methods)
            where T : TypeDeclarationSyntax
        {
            var output = typeDeclaration.AddMembersWithBlankLineSeparation(methods);
            return output;
        }

        public static T AddMethods<T>(this T typeDeclaration,
            IEnumerable<BaseMethodDeclarationSyntax> methods)
            where T : TypeDeclarationSyntax
        {
            var output = typeDeclaration.AddMethods(methods.ToArray());
            return output;
        }

        public static T AddMethodWithoutBody<T>(this T typeDeclaration,
            string name, string returnType,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithIndentationSynchronous<MethodDeclarationSyntax> modifier)
            where T : TypeDeclarationSyntax
        {
            var indentedLeadingWhitespace = leadingWhitespace.IndentByTab();

            var method = Instances.SyntaxFactory.Method(name, returnType)
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

            var method = Instances.SyntaxFactory.Method(name, returnType)
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
                        var body = Instances.SyntaxFactory.Body();

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
            var property = Instances.SyntaxFactory.Property(name, typeName)
                .ModifyWith(outerLeadingWhitespace, premodifier)
                .NormalizeWhitespace()
                .WithLeadingTrivia(outerLeadingWhitespace.GetNewLineLeadingWhitespace())
                .ModifyWith(outerLeadingWhitespace, modifier)
                ;

            var output = typeDeclaration.AddMembers(property) as T;
            return output;
        }

        /// <summary>
        /// Gets the name of the namespace directly containing the type.
        /// <there-might-be-nested-namespaces>There might be nested namespaces, meaning that the full namespace for a type might not be just the name of the containing namespace.</there-might-be-nested-namespaces>
        /// This returns just the name of the containing namespace. See also: <seealso cref="GetNamespaceName(TypeDeclarationSyntax)"/>
        /// </summary>
        public static string GetContainingNamespaceName(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var output = typeDeclarationSyntax.GetContainingNamespace().GetName();
            return output;
        }

        /// <summary>
        /// Gets the name of the namespace containing the type.
        /// <there-might-be-nested-namespaces>There might be nested namespaces, meaning that the full namespace for a type might not be just the name of the containing namespace.</there-might-be-nested-namespaces>
        /// This returns the full name of the namespace.  See also: <seealso cref="GetContainingNamespaceName(TypeDeclarationSyntax)"/>
        /// </summary>
        public static string GetNamespaceName(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var namespaces = typeDeclarationSyntax.GetContainingNamespacesOutsideToInside();
            var namespaceNames = namespaces
                .Select(xNamespace => xNamespace.GetName())
                ;

            var output = Instances.NamespaceName.CombineTokens(namespaceNames);
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
