using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static class ChildSyntaxListExtensions
    {
        public static WasFound<int> IndexOfChildInNodesAndTokens(this ChildSyntaxList childNodesAndTokens, SyntaxNode childSyntaxNode)
        {
            if (childNodesAndTokens.Count == 0)
            {
                return WasFound.From(false, IndexHelper.NotFound);
            }

            var indexOfChild = IndexHelper.NotFound;
            for (int iIndex = 0; iIndex < childNodesAndTokens.Count; iIndex++)
            {
                var currentChildNodeOrToken = childNodesAndTokens[iIndex];
                if (currentChildNodeOrToken.IsNode)
                {
                    var currentChildNode = currentChildNodeOrToken.AsNode();
                    if (currentChildNode == childSyntaxNode)
                    {
                        indexOfChild = iIndex;

                        break;
                    }
                }
            }

            var wasFound = IndexHelper.IsFound(indexOfChild);

            var output = WasFound.From(wasFound, indexOfChild);
            return output;
        }

        public static WasFound<int> IndexOfChildInNodesAndTokens(this ChildSyntaxList childNodesAndTokens, SyntaxToken childSyntaxToken)
        {
            if (childNodesAndTokens.Count == 0)
            {
                return WasFound.From(false, IndexHelper.NotFound);
            }

            var indexOfChild = IndexHelper.NotFound;
            for (int iIndex = 0; iIndex < childNodesAndTokens.Count; iIndex++)
            {
                var currentChildNodeOrToken = childNodesAndTokens[iIndex];
                if (currentChildNodeOrToken.IsToken)
                {
                    var currentChildToken = currentChildNodeOrToken.AsToken();
                    if (currentChildToken == childSyntaxToken)
                    {
                        indexOfChild = iIndex;

                        break;
                    }
                }
            }

            var wasFound = IndexHelper.IsFound(indexOfChild);

            var output = WasFound.From(wasFound, indexOfChild);
            return output;
        }
    }
}
