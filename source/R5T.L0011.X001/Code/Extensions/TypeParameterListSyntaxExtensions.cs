using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class TypeParameterListSyntaxExtensions
    {
        public static bool HasParameters(this TypeParameterListSyntax typeParameterList)
        {
            var output = typeParameterList.Parameters.Any();
            return output;
        }

        public static string ToTextAngleBracedSeparated(this TypeParameterListSyntax typeParameterList)
        {
            var hasParameters = typeParameterList.HasParameters();
            if(!hasParameters)
            {
                return Strings.Empty;
            }

            var separatedString = typeParameterList.ToTextSeparated();

            var output = $"{Strings.LessThan}{separatedString}{Strings.GreaterThan}";
            return output;
        }

        public static string ToTextSeparated(this TypeParameterListSyntax typeParameterList)
        {
            var hasParameters = typeParameterList.HasParameters();
            if(!hasParameters)
            {
                return Strings.Empty;
            }

            var parameterStandardStrings = typeParameterList.Parameters.Select(
                xParameter => xParameter.ToTextStandard());

            var output = String.Join(Strings.CommaSeparatedListSpacedSeparator, parameterStandardStrings);
            return output;
        }

        public static string ToTextStandard(this TypeParameterListSyntax typeParameterList)
        {
            var output = typeParameterList.ToTextAngleBracedSeparated();
            return output;
        }
    }
}
