using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;



namespace System
{
    public static partial class SyntaxTokenExtensions
    {
        public static SyntaxToken Annotate(this SyntaxToken token,
            out SyntaxAnnotation annotation)
        {
            annotation = SyntaxFactoryHelper.Annotation();

            var output = token.WithAdditionalAnnotations(annotation);
            return output;
        }
    }
}
