using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class InterfaceDeclarationSyntaxExtensions
    {
        ///// <summary>
        ///// Simply check whether an attribute with the specified type name exists.
        ///// (Does not check for X vs. XAttribute varieties of attribute type name.)
        ///// </summary>
        //public static bool HasAttributeOfTypeSimple(this InterfaceDeclarationSyntax @interface,
        //    string attributeTypeName)
        //{
        //    var output = @interface.AttributeLists
        //        .SelectMany(xAttributeList => xAttributeList.Attributes) // Get all attributes across all attribute lists.
        //        .Where(xAttribute => xAttribute.Name.ToString() == attributeTypeName)
        //        .Any()
        //        ;

        //    return output;
        //}
    }
}
