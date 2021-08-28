using System;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class ISymbolExtensions
    {
        public static bool HasAttributeWithTypeName(this ISymbol symbol, string attributeTypeName)
        {
            var output = symbol.GetAttributes()
                .ContainsAttributeWithTypeName(attributeTypeName);
            
            return output;
        }

        public static bool HasAttributeOfType<TAttribute>(this ISymbol symbol)
            where TAttribute : Attribute
        {
            var attributeTypeName = typeof(TAttribute).Name;

            var output = symbol.HasAttributeWithTypeName(attributeTypeName);
            return output;
        }
    }
}
