using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class ClassDeclarationSyntaxExtensions
    {
        public static ClassDeclarationSyntax AddMethod(this ClassDeclarationSyntax @class,
            MethodDeclarationSyntax method,
            ModifierSynchronous<MethodDeclarationSyntax> modifier = default)
        {
            var modifiedClass = method.ModifyWith(modifier);

            var output = @class.AddMembers(modifiedClass);
            return output;
        }

        public static ClassDeclarationSyntax AddMethodWithoutModification(this ClassDeclarationSyntax @class,
               MethodDeclarationSyntax method)
        {
            var output = @class.AddMembers(method);
            return output;
        }

        public static ClassDeclarationSyntax AddMethodsWithoutModification(this ClassDeclarationSyntax @class,
               MethodDeclarationSyntax[] methods)
        {
            var output = @class.AddMembers(methods);
            return output;
        }

        public static ClassDeclarationSyntax AddProperty(this ClassDeclarationSyntax @class,
            PropertyDeclarationSyntax property)
        {
            var output = @class.AddMembers(property);
            return output;
        }

        public static ClassDeclarationSyntax AddPropertyAfter(this ClassDeclarationSyntax @class,
            PropertyDeclarationSyntax property,
            string priorPropertyName)
        {
            var priorProperty = @class.GetProperty(priorPropertyName);

            var output = @class.InsertNodesAfter(priorProperty, EnumerableHelper.From(property));
            return output;
        }

        public static ClassDeclarationSyntax AddPropertyBefore(this ClassDeclarationSyntax @class,
            PropertyDeclarationSyntax property,
            string nextPropertyName)
        {
            var nextProperty = @class.GetProperty(nextPropertyName);

            var output = @class.InsertNodesBefore(nextProperty, EnumerableHelper.From(property));
            return output;
        }

        public static string GetClassName(this ClassDeclarationSyntax @class)
        {
            var output = @class.Identifier.Text;
            return output;
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

        public static IEnumerable<PropertyDeclarationSyntax> GetStaticProperties(this ClassDeclarationSyntax @class)
        {
            var output = @class.GetProperties()
                .Where(xProperty => xProperty.IsStatic())
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

        /// <summary>
        /// Simply check whether an attribute with the specified type name exists.
        /// (Does not check for X vs. XAttribute varieties of attribute type name.)
        /// </summary>
        public static bool HasAttributeOfTypeSimple(this ClassDeclarationSyntax @class,
            string attributeTypeName)
        {
            var output = @class.AttributeLists
                .SelectMany(xAttributeList => xAttributeList.Attributes) // Get all attributes across all attribute lists.
                .Where(xAttribute => xAttribute.Name.ToString() == attributeTypeName)
                .Any()
                ;

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

        public static WasFound<MethodDeclarationSyntax> HasFirstMethod(this ClassDeclarationSyntax @class)
        {
            var firstMethodOrDefault = @class.Members
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault();

            var wasFound = WasFound.From(firstMethodOrDefault);
            return wasFound;
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
        /// Gets all <see cref="BaseMethodDeclarationSyntax"/> methods, including <see cref="MethodDeclarationSyntax"/>, <see cref="OperatorDeclarationSyntax"/>, etc.
        /// NOTE: does not include constructors?
        /// </summary>
        public static IEnumerable<BaseMethodDeclarationSyntax> GetAllMethods(this ClassDeclarationSyntax @class)
        {
            var output = @class.Members
                .OfType<MethodDeclarationSyntax>();

            return output;
        }

        /// <summary>
        /// Gets only <see cref="MethodDeclarationSyntax"/> methods (not including other <see cref="BaseMethodDeclarationSyntax"/> method types like <see cref="OperatorDeclarationSyntax"/>, etc.).
        /// </summary>
        public static IEnumerable<MethodDeclarationSyntax> GetMethods(this ClassDeclarationSyntax @class)
        {
            var output = @class.Members
                .OfType<MethodDeclarationSyntax>();

            return output;
        }

        public static bool IsStatic(this ClassDeclarationSyntax @class)
        {
            var isStatic = @class.Modifiers
                .Where(xToken => xToken.IsKind(SyntaxKind.StaticKeyword))
                .Any();

            return isStatic;
        }

        public static bool IsClassName(this ClassDeclarationSyntax @class,
            string className)
        {
            var output = @class.GetClassName() == className;
            return output;
        }

        public static async Task<ClassDeclarationSyntax> ModifyMethod(this ClassDeclarationSyntax @class,
            Func<ClassDeclarationSyntax, MethodDeclarationSyntax> methodSelector,
            Func<MethodDeclarationSyntax, Task<MethodDeclarationSyntax>> methodAction = default)
        {
            var method = methodSelector(@class);

            var outputMethod = await methodAction(method);

            var outputClass = @class.ReplaceNode(method, outputMethod);
            return outputClass;
        }

        public static async Task<ClassDeclarationSyntax> ModifyMethodBody(this ClassDeclarationSyntax @class,
            Func<ClassDeclarationSyntax, MethodDeclarationSyntax> methodSelector,
            Func<BlockSyntax, Task<BlockSyntax>> methodBodyAction = default)
        {
            var method = methodSelector(@class);

            var outputMethod = await method.ModifyMethodBody(methodBodyAction);

            var outputClass = @class.ReplaceNode(method, outputMethod);
            return outputClass;
        }

        public static ClassDeclarationSyntax RemoveMember(this ClassDeclarationSyntax @class,
            MemberDeclarationSyntax member)
        {
            var output = @class.WithMembers(@class.Members.Remove(member));
            return output;
        }
    }
}
