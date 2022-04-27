using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace R5T.L0011.T004
{
    public class UsingDirectivesSpecification
    {
        public List<string> UsingNamespaceNames { get; } = new List<string>();
        public List<NameAlias> NameAliases { get; } = new List<NameAlias>();
    }
}
