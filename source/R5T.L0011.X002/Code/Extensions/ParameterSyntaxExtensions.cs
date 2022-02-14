using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class ParameterSyntaxExtensions
    {
        public static string ToStringStandard(this ParameterSyntax parameter)
        {
            var isExtensionParameter = parameter.IsExtensionParameter();

            var extensionSignifier = isExtensionParameter
                ? $"{Instances.Syntax.This()} "
                : Strings.Empty
                ;

            var output = $"{extensionSignifier}{parameter.Type} {parameter.Identifier}";
            return output;
        }
    }
}
