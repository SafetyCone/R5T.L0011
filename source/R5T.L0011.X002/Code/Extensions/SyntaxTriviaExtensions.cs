using System;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class SyntaxTriviaExtensions
    {
        public static SyntaxTrivia Annotate(this SyntaxTrivia syntaxTrivia, out SyntaxAnnotation annotation)
        {
            annotation = Instances.SyntaxFactory.Annotation();

            var output = syntaxTrivia.WithAdditionalAnnotations(annotation);
            return output;
        }
    }
}
