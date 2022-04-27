using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class NameAliasExtensions
    {
        public static UsingDirectiveSyntax GetUsingDirectiveSyntax(this NameAlias nameAlias)
        {
            var output = Instances.SyntaxFactory
                .Using(nameAlias)
                ;

            return output;
        }
    }
}
