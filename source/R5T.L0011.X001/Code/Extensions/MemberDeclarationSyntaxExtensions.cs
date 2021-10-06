using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class MemberDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Simply check whether an attribute with the specified type name exists.
        /// (Does not check for X vs. XAttribute varieties of attribute type name.)
        /// </summary>
        public static bool HasAttributeOfTypeSimple<TMemberDeclaration>(this TMemberDeclaration member,
            string attributeTypeName)
            where TMemberDeclaration : MemberDeclarationSyntax
        {
            var output = member.AttributeLists
                .SelectMany(xAttributeList => xAttributeList.Attributes) // Get all attributes across all attribute lists.
                .Where(xAttribute => xAttribute.Name.ToString() == attributeTypeName)
                .Any()
                ;

            return output;
        }

        public static bool IsStatic(this MemberDeclarationSyntax member)
        {
            var output = member.Modifiers
                .Where(xModifer => xModifer.IsStatic())
                .Any();

            return output;
        }
    }
}
