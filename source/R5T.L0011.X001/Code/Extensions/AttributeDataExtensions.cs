using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class AttributeDataExtensions
    {
        public static bool ContainsAttributeWithTypeName(this IEnumerable<AttributeData> attributes, string attributeTypeName)
        {
            var output = attributes
                .Where(x => x.AttributeClass.Name == attributeTypeName)
                .Any();

            return output;
        }

        public static bool ContainsAttributeOfType<TAttribute>(this IEnumerable<AttributeData> attributes)
            where TAttribute : Attribute
        {
            var attributeTypeName = typeof(TAttribute).Name;

            var output = attributes.ContainsAttributeWithTypeName(attributeTypeName);
            return output;
        }
    }
}
