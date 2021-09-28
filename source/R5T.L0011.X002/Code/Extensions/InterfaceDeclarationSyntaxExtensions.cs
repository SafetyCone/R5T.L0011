using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class InterfaceDeclarationSyntaxExtensions
    {
        ///// <summary>
        ///// The <paramref name="attributeTypeName"/> value might included the "-Attribute" suffix, or it may not.
        ///// This method tests for the existence of both forms.
        ///// </summary>
        //public static bool HasAttributeOfTypeSuffixedOrUnsuffixed(this InterfaceDeclarationSyntax @interface,
        //    string attributeTypeName)
        //{
        //    var attributeSuffixedAttributeTypeName = Instances.AttributeTypeName.GetEnsuredAttributeSuffixedTypeName(attributeTypeName);
        //    var hasAttributeSuffixedAttributeTypeName = @interface.HasAttributeOfTypeSimple(attributeSuffixedAttributeTypeName);
        //    if(hasAttributeSuffixedAttributeTypeName)
        //    {
        //        // If the interface has the attribute-suffixed attribute type name, we are done.
        //        return hasAttributeSuffixedAttributeTypeName;
        //    }

        //    var nonAttributeSuffixedAttributeTypeName = Instances.AttributeTypeName.GetEnsuredNonAttributeSuffixedTypeName(attributeTypeName);
        //    var hasNonAttributeSuffixedAttributeTypeName = @interface.HasAttributeOfTypeSimple(nonAttributeSuffixedAttributeTypeName);

        //    // At this point, we have already tested the attribute-suffixed attribute type name, so the interface either has the non-attribute-suffixed type name or it doesn't have the attribute.
        //    return hasNonAttributeSuffixedAttributeTypeName;
        //}

        ///// <summary>
        ///// Selects <see cref="HasAttributeOfTypeSuffixedOrUnsuffixed(InterfaceDeclarationSyntax, string)"/> as the default.
        ///// </summary>
        //public static bool HasAttributeOfType(this InterfaceDeclarationSyntax @interface,
        //    string attributeTypeName)
        //{
        //    var output = @interface.HasAttributeOfTypeSuffixedOrUnsuffixed(attributeTypeName);
        //    return output;
        //}
    }
}
