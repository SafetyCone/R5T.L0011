using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


namespace System
{
    public static partial class SyntaxTriviaExtensions
    {
        public static SyntaxTrivia Annotate(this SyntaxTrivia syntaxTrivia, out SyntaxAnnotation annotation)
        {
            annotation = SyntaxFactoryHelper.Annotation();

            var output = syntaxTrivia.WithAdditionalAnnotations(annotation);
            return output;
        }
    }
}
