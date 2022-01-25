﻿using System;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.L0011.T001;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class SyntaxTokenExtensions
    {
        public static SyntaxToken Annotate(this SyntaxToken syntaxToken, out SyntaxAnnotation annotation)
        {
            annotation = Instances.SyntaxFactory.Annotation();

            var output = syntaxToken.WithAdditionalAnnotations(annotation);
            return output;
        }

        public static SyntaxToken AppendComment(this SyntaxToken syntaxToken, string text)
        {
            var comment = Instances.SyntaxFactory.Comment(text);

            var output = syntaxToken.AddTrailingTrivia(comment);
            return output;
        }

        public static SyntaxToken AppendNewLineLeadingWhitespace(this SyntaxToken syntaxToken, SyntaxTriviaList leadingWhitespace)
        {
            var output = syntaxToken.AddTrailingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static SyntaxToken PrependNewLine(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.AddLeadingLeadingTrivia(Instances.SyntaxFactory.NewLine());
            return output;
        }
    }
}
