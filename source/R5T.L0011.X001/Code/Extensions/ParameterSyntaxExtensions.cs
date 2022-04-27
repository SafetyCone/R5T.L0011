using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class ParameterSyntaxExtensions
    {
        public static string ToTextStandard(this ParameterSyntax parameter)
        {
            var output = parameter.ToStringWithSingleSpacing();
            return output;
        }
    }
}
