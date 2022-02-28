using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class ParameterListSyntaxExtensions
    {
        public static string ToTextParenthesiedSeparated(this ParameterListSyntax parameterList)
        {
            var separatedText = parameterList.ToTextSeparated();

            var output = $"{Strings.OpenParenthesis}{separatedText}{Strings.CloseParenthesis}";
            return output;
        }

        public static string ToTextSeparated(this ParameterListSyntax parameterList)
        {
            var parameterStandardTexts = parameterList.Parameters
                .Select(xParameter => xParameter.ToStringWithSingleSpacing());

            var output = String.Join(Strings.CommaSeparatedListSpacedSeparator, parameterStandardTexts);
            return output;
        }

        public static string ToTextStandard(this ParameterListSyntax parameterList)
        {
            var output = parameterList.ToTextParenthesiedSeparated();
            return output;
        }
    }
}
