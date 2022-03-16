using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class TypeParameterSyntaxExtensions
    {
        public static bool IsVariant(this TypeParameterSyntax typeParameter)
        {
            var output = !typeParameter.VarianceKeyword.IsNone();
            return output;
        }

        /// <inheritdoc cref="IsOutVariant(TypeParameterSyntax)"/>
        public static bool IsCovariant(this TypeParameterSyntax typeParameter)
        {
            var output = typeParameter.IsOutVariant();
            return output;
        }

        /// <inheritdoc cref="IsInVariant(TypeParameterSyntax)"/>
        public static bool IsContravariant(this TypeParameterSyntax typeParameter)
        {
            var output = typeParameter.IsInVariant();
            return output;
        }

        /// <summary>
        /// A generic type parameter is contravariant if it uses the 'in' keywod.
        /// Contravariant type parameters can only be inputs, and this means that a reference to a input-taking more derived type can be set to an input-taking less derived type.
        /// For example, Action{string} can be instantiated from an Action{object}, since Action{T} is contravariant (an action taking an object can take a string, since string is an object).
        /// </summary>
        public static bool IsInVariant(this TypeParameterSyntax typeParameter)
        {
            var output = typeParameter.VarianceKeyword.IsKind(SyntaxKind.InKeyword);
            return output;
        }

        /// <summary>
        /// A generic type parameter is covariant if it uses the 'out' keyword.
        /// Covariant type parameters can only be outputs, and this means that a reference to an output-making less derived type can be set to an output-making more derived type.
        /// For example, IEnumerable{object} can be instantiated from an IEnumerable{string}, since IEnumerable{string} is covariant (an enumerable producing objects can produce a string, since string is an object).
        /// </summary>
        public static bool IsOutVariant(this TypeParameterSyntax typeParameter)
        {
            var output = typeParameter.VarianceKeyword.IsKind(SyntaxKind.OutKeyword);
            return output;
        }

        public static string GetVarianceKeywordText(this TypeParameterSyntax typeParameter)
        {
            var output = typeParameter.IsVariant()
                ? typeParameter.VarianceKeyword.Text
                : Strings.Empty
                ;

            return output;
        }

        public static string GetVarianceSegmentText(this TypeParameterSyntax typeParameter)
        {
            var output = typeParameter.IsVariant()
                ? $"{typeParameter.GetVarianceKeywordText()}{Strings.Space}"
                : Strings.Empty
                ;

            return output;
        }

        public static string ToTextStandard(this TypeParameterSyntax typeParameter)
        {
            var varianceSegment = typeParameter.GetVarianceSegmentText();

            var output = $"{varianceSegment}{typeParameter.Identifier.Text}";
            return output;
        }
    }
}
