using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;
using R5T.L0011.T002;

using CSharpSyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        public static XmlElementSyntax XmlSummary(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList leadingWhitespace)
        {
            var output = syntaxFactory.XmlSummaryElementOnly()
                .AddTagLineStarts(leadingWhitespace)
                ;

            return output;
        }
    }
}
