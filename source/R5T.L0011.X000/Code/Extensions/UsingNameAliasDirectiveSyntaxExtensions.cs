using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class UsingNameAliasDirectiveSyntaxExtensions
    {
        public static NameAlias GetNameAlias(this UsingNameAliasDirectiveSyntax usingNameAliasDirective)
        {
            var (destinationName, sourceNameExpression) = usingNameAliasDirective.GetNameAliasValues();

            var output = NameAlias.From(
                destinationName,
                sourceNameExpression);

            return output;
        }

        public static (string destinationName, string sourceNameExpression) GetNameAliasValues(this UsingNameAliasDirectiveSyntax usingNameAliasDirective)
        {
            var destinationName = usingNameAliasDirective.SyntaxNode.GetDestinationName();
            var sourceNameExpression = usingNameAliasDirective.SyntaxNode.GetSourceNameExpression();

            return (destinationName, sourceNameExpression);
        }
    }
}
