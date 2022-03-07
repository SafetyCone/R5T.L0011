using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.L0011.X000;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        public static bool IsPropertyDeclaration(this SyntaxNode node)
        {
            var output = node.IsKind(SyntaxKind.PropertyDeclaration);
            return output;
        }
    }
}