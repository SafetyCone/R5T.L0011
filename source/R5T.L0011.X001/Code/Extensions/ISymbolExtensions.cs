using System;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class ISymbolExtensions
    {
        public static bool HasAttributeOfType(this ISymbol symbol, string attributeTypeName)
        {
            var output = symbol.GetAttributes()
                .ContainsAttributeOfType(attributeTypeName);
            
            return output;
        }

        public static bool HasAttributeOfType<TAttribute>(this ISymbol symbol)
            where TAttribute : Attribute
        {
            var attributeTypeName = typeof(TAttribute).Name;

            var output = symbol.HasAttributeOfType(attributeTypeName);
            return output;
        }
    }
}
