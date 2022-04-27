using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class GenericNameSyntaxExtensions
    {
        public static string GetArityToken(this GenericNameSyntax genericName)
        {
            var output = $"{Strings.Tick}{genericName.Arity}";
            return output;
        }

        public static string GetIdentifierText(this GenericNameSyntax genericName)
        {
            var output = genericName.Identifier.GetText();
            return output;
        }

        public static string GetTypeName_HandlingTypeParameters(this GenericNameSyntax genericName)
        {
            var identifier = genericName.GetIdentifierText();

            var arityToken = genericName.GetArityToken();

            var typeName = $"{identifier}{arityToken}";
            return typeName;
        }
    }
}
