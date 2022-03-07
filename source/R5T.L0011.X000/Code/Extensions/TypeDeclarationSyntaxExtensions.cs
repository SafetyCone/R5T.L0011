using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


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
