using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class ClassDeclarationSyntaxExtensions
    {
        public static ClassDeclarationSyntax AddMethod(this ClassDeclarationSyntax @class,
               MethodDeclarationSyntax method)
        {
            var output = @class.AddMembers(method);
            return output;
        }

        public static WasFound<MethodDeclarationSyntax> HasFirstMethod(this ClassDeclarationSyntax @class)
        {
            var firstMethodOrDefault = @class.Members
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault();

            var wasFound = WasFound.From(firstMethodOrDefault);
            return wasFound;
        }

        public static string GetClassName(this ClassDeclarationSyntax @class)
        {
            var output = @class.Identifier.Text;
            return output;
        }

        public static WasFound<ConstructorDeclarationSyntax> HasConstructor(this ClassDeclarationSyntax @class)
        {
            var constructorOrDefault = @class.Members
                .OfType<ConstructorDeclarationSyntax>()
                .SingleOrDefault();

            var wasFound = WasFound.From(constructorOrDefault);
            return wasFound;
        }

        public static ConstructorDeclarationSyntax GetConstructor(this ClassDeclarationSyntax @class)
        {
            var constructorWasFound = @class.HasConstructor();
            if(!constructorWasFound)
            {
                throw new Exception("No constructor found.");
            }

            return constructorWasFound.Result;
        }

        public static IEnumerable<PropertyDeclarationSyntax> GetProperties(this ClassDeclarationSyntax @class)
        {
            var output = @class.Members
                .OfType<PropertyDeclarationSyntax>()
                ;

            return output;
        }

        public static IEnumerable<string> GetPropertyNames(this ClassDeclarationSyntax @class)
        {
            var output = @class.GetProperties()
                .Select(property => property.Identifier.Text)
                ;

            return output;
        }

        public static WasFound<PropertyDeclarationSyntax> HasProperty(this ClassDeclarationSyntax @class,
            string propertyName)
        {
            var propertyOrDefault = @class.GetProperties()
                .Where(x => x.Identifier.Text == propertyName)
                .SingleOrDefault();

            var wasFound = WasFound.From(propertyOrDefault);
            return wasFound;
        }

        public static PropertyDeclarationSyntax GetProperty(this ClassDeclarationSyntax @class,
            string propertyName)
        {
            var propertyWasFound = @class.HasProperty(propertyName);
            if(!propertyWasFound)
            {
                throw new Exception($"Property '{propertyName}' was not found.");
            }

            return propertyWasFound.Result;
        }

        public static bool HasMethodMembers(this ClassDeclarationSyntax @class)
        {
            var output = @class.Members
                .OfType<MethodDeclarationSyntax>()
                .Any();

            return output;
        }

        public static IEnumerable<BaseMethodDeclarationSyntax> GetAllMethodsIncludingConstructors(this ClassDeclarationSyntax @class)
        {
            var output = @class.Members
                .OfType<BaseMethodDeclarationSyntax>();

            return output;
        }

        /// <summary>
        /// NOTE: does not include constructors.
        /// </summary>
        public static IEnumerable<BaseMethodDeclarationSyntax> GetMethods(this ClassDeclarationSyntax @class)
        {
            var output = @class.Members
                .OfType<MethodDeclarationSyntax>();

            return output;
        }

        public static ClassDeclarationSyntax RemoveMember(this ClassDeclarationSyntax @class,
            MemberDeclarationSyntax member)
        {
            var output = @class.WithMembers(@class.Members.Remove(member));
            return output;
        }
    }
}
