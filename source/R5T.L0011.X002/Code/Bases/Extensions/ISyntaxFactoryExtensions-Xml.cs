using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        public static XmlElementSyntax XmlSummary(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList indentation)
        {
            var output = syntaxFactory.XmlSummaryElementOnly()
                .AddTagLineStarts(indentation)
                ;

            return output;
        }
    }
}
