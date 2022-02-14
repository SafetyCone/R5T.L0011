using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;


namespace System
{
    public static class ISyntaxFactoryExtensions
    {
        public static UsingDirectiveSyntax Using(this ISyntaxFactory _,
            string namespaceName,
            bool prependNewLine = true)
        {
            var output = _.Using_WithoutLeadingNewLine(namespaceName)
                .PrependNewLineIf(prependNewLine)
                ;
            return output;
        }

        public static UsingDirectiveSyntax Using(this ISyntaxFactory _,
            string destinationName,
            string sourceNameExpression,
            bool prependNewLine = true)
        {
            var output = _.Using_WithoutLeadingNewLine(
                destinationName,
                sourceNameExpression)
                .PrependNewLineIf(prependNewLine)
                ;

            return output;
        }

        public static UsingDirectiveSyntax Using(this ISyntaxFactory _,
            NameAlias nameAlias,
            bool prependNewLine = true)
        {
            var output = _.Using_WithoutLeadingNewLine(
                nameAlias.DestinationName,
                nameAlias.SourceNameExpression)
                .PrependNewLineIf(prependNewLine)
                ;

            return output;
        }
    }
}
