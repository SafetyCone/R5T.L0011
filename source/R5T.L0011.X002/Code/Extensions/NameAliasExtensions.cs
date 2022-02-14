using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T004;

using NameAlias = R5T.L0011.T004.NameAlias;

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
