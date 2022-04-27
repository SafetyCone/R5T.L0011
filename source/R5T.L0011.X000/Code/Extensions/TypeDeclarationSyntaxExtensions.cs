using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class TypeDeclarationSyntaxExtensions
    {
        public static T AddProperties<T>(this T typedeclaration,
            params PropertyDeclarationSyntax[] properties)
            where T : TypeDeclarationSyntax
        {
            var output = typedeclaration.AddMembers(properties) as T;
            return output;
        }

        public static T AddProperties<T>(this T typedeclaration,
            IEnumerable<PropertyDeclarationSyntax> properties)
            where T : TypeDeclarationSyntax
        {
            var propertiesArray = properties.Now();

            var output = typedeclaration.AddProperties(propertiesArray);
            return output;
        }

        public static string GetConstraintClausesText(this TypeDeclarationSyntax typeDeclaration)
        {
            var output = String.Join(
                Strings.Space,
                typeDeclaration.ConstraintClauses
                    .Select(xConstraintClause => xConstraintClause.ToTextStandard()));

            return output;
        }

        public static IEnumerable<MethodDeclarationSyntax> GetMethods(this TypeDeclarationSyntax typeDeclaration)
        {
            var output = typeDeclaration.GetMembers()
                .Where(x => x is MethodDeclarationSyntax)
                .Cast<MethodDeclarationSyntax>()
                ;

            return output;
        }

        public static IEnumerable<MemberDeclarationSyntax> GetMembers(this TypeDeclarationSyntax typeDeclaration)
        {
            return typeDeclaration.Members.AsEnumerable();
        }

        public static IEnumerable<MemberDeclarationSyntax> GetMembers(this TypeDeclarationSyntax typeDeclaration,
            Func<MemberDeclarationSyntax, bool> memberSelector)
        {
            var output = typeDeclaration.Members
                .Where(xMember => memberSelector(xMember))
                ;

            return output;
        }

        public static IEnumerable<PropertyDeclarationSyntax> GetProperties(this TypeDeclarationSyntax typeDeclaration)
        {
            var output = typeDeclaration
                .GetMembers(xMember => xMember.IsPropertyDeclaration())
                .Cast<PropertyDeclarationSyntax>()
                ;

            return output;
        }

        public static string GetTypeParametersCommaSeparatedListWithoutAngleBraces(this TypeDeclarationSyntax typeDeclaration)
        {
            var output = String.Join(
                Strings.CommaSeparatedListSpacedSeparator,
                typeDeclaration.TypeParameterList.Parameters
                    .Select(xTypeParameter => xTypeParameter.ToTextStandard()));

            return output;
        }

        public static string GetTypeParametersCommaSeparatedListText(this TypeDeclarationSyntax typeDeclaration)
        {
            var commaSeparatedListWithoutAngleBraces = typeDeclaration.GetTypeParametersCommaSeparatedListWithoutAngleBraces();

            var output = $"{Characters.OpenAngleBrace}{commaSeparatedListWithoutAngleBraces}{Characters.CloseAngleBrace}";
            return output;
        }

        public static bool HasConstraintClauses(this TypeDeclarationSyntax typeDeclaration)
        {
            var output = typeDeclaration.ConstraintClauses.Any();
            return output;
        }

        public static bool HasTypeParameters(this TypeDeclarationSyntax typeDeclaration)
        {
            var output = typeDeclaration.TypeParameterList is object;
            return output;
        }

        public static bool IsClass(this TypeDeclarationSyntax typeDeclaration)
        {
            var output = typeDeclaration is ClassDeclarationSyntax;
            return output;
        }

        public static bool IsInterface(this TypeDeclarationSyntax typeDeclaration)
        {
            var output = typeDeclaration is InterfaceDeclarationSyntax;
            return output;
        }

        public static T RemoveMembers<T>(this T typeDeclaration,
            Func<MemberDeclarationSyntax, bool> membersToRemoveSelector)
            where T : TypeDeclarationSyntax
        {
            var membersToKeep = typeDeclaration.Members
                .ExceptWhere(xMember => membersToRemoveSelector(xMember))
                .ToSyntaxList();

            var output = typeDeclaration.WithMembers(membersToKeep) as T;
            return output;
        }

        public static T RemoveMembers<T>(this T typeDeclaration,
            IEnumerable<MemberDeclarationSyntax> members)
            where T : TypeDeclarationSyntax
        {
            var membersHash = new HashSet<MemberDeclarationSyntax>(members);

            var output = typeDeclaration.RemoveMembers(
                x => membersHash.Contains(x));

            return output;
        }

        public static T RemoveProperties<T>(this T typeDeclaration)
            where T : TypeDeclarationSyntax
        {
            var output = typeDeclaration.RemoveMembers(
                xMember => xMember.IsPropertyDeclaration());

            return output;
        }

        public static T OrderPropertiesBy<T, TKey>(this T typeDeclaration,
            Func<PropertyDeclarationSyntax, TKey> keySelector)
            where T : TypeDeclarationSyntax
        {
            var properties = typeDeclaration.GetProperties();

            var orderedProperties = properties
                .OrderBy(keySelector)
                ;

            var output = typeDeclaration.WithProperties(orderedProperties);
            return output;
        }

        public static T SortMethods<T>(this T @class)
            where T : TypeDeclarationSyntax
        {
            // Get a list of all siblings.
            var members = @class.Members.ToList();

            // Get a list of siblings interest.
            var methods = members
                .Where(x => x is MethodDeclarationSyntax)
                .Cast<MethodDeclarationSyntax>()
                .Now();

            // Remove the nodes of interest from the list of all siblings.
            var newMembers = members.Except(methods).ToList();

            // Sort the nodes.
            var sortedMembers = methods
                .OrderBy(x => x.GetName_Simple())
                .Now();

            // Identify a location within the parent, after which, the sorted nodes will be inserted.
            // For now, insert at end.
            var insertionIndex = newMembers.LastIndexForInsertion();

            // Insert the sorted nodes.
            newMembers.InsertRange(
                insertionIndex,
                sortedMembers);

            @class = @class.WithMembers(newMembers.ToSyntaxList()) as T;

            return @class;
        }

        /// <summary>
        /// Set the leading separting spacing between methods.
        /// </summary>
        public static T SetMethodSpacing<T>(this T @class,
            SyntaxTriviaList leadingSeparatingSpacing)
            where T : TypeDeclarationSyntax
        {
            var methods = @class.GetMethods();

            @class = @class.AnnotateNodes(
                methods,
                out var annotationsByMethod);

            foreach (var annotation in annotationsByMethod.Values)
            {
                @class = @class.SetLeadingSeparatingSpacing(
                    annotation.GetNode(@class),
                    leadingSeparatingSpacing);
            }

            return @class;
        }

        public static T WithoutConstraintClauses<T>(this T typeDeclaration)
            where T : TypeDeclarationSyntax
        {
            var emptyConstraintClausesList = new SyntaxList<TypeParameterConstraintClauseSyntax>();

            var output = typeDeclaration.WithConstraintClauses(emptyConstraintClausesList) as T;
            return output;
        }

        public static T WithoutKeyword<T>(this T typeDeclaration)
            where T : TypeDeclarationSyntax
        {
            var noKeywordToken = SyntaxFactoryHelper.None();

            var output = typeDeclaration.WithKeyword(noKeywordToken) as T;
            return output;
        }

        public static T WithoutMembers<T>(this T typeDeclaration)
            where T : TypeDeclarationSyntax
        {
            var emptyMembersList = new SyntaxList<MemberDeclarationSyntax>();

            var output = typeDeclaration.WithMembers(emptyMembersList) as T;
            return output;
        }

        public static T WithoutTypeParameterList<T>(this T typeDeclaration)
            where T : TypeDeclarationSyntax
        {
            TypeParameterListSyntax emptyTypeParameterList = null;

            var output = typeDeclaration.WithTypeParameterList(emptyTypeParameterList) as T;
            return output;
        }

        public static T WithProperties<T>(this T typeDeclaration,
            params PropertyDeclarationSyntax[] properties)
            where T : TypeDeclarationSyntax
        {
            var output = typeDeclaration
                .RemoveProperties()
                .AddProperties(properties)
                ;

            return output;
        }

        public static T WithProperties<T>(this T typeDeclaration,
            IEnumerable<PropertyDeclarationSyntax> properties)
            where T : TypeDeclarationSyntax
        {
            var propertiesArray = properties.Now();

            var output = typeDeclaration.WithProperties(propertiesArray);
            return output;
        }
    }
}
