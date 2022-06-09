using System;

using Microsoft.CodeAnalysis;

using R5T.Magyar;


namespace R5T.L0011.X000.Generation.Initial.Parse
{
    public static class SyntaxNodeExtensions
    {
        public static WasSuccess<TNode> SimpleParse_ValidateResult<TNode>(this TNode parseResult,
            Func<TNode, WasSuccess> validator)
            where TNode : SyntaxNode
        {
            var output = SyntaxParser.SimpleParse_ValidateResult(
                parseResult,
                validator);

            return output;
        }

        public static TNode SimpleParse_ThrowIfNotSuccessful<TNode>(this WasSuccess<TNode> parseWasSuccess)
            where TNode : SyntaxNode
        {
            var output = SyntaxParser.SimpleParse_ThrowIfNotSuccessful(parseWasSuccess);
            return output;
        }

        public static TNode SimpleParse_ThrowIfNotSuccessful<TNode>(this TNode parseResult,
            Func<TNode, WasSuccess> validator)
            where TNode : SyntaxNode
        {
            return parseResult
                .SimpleParse_ValidateResult(validator)
                .SimpleParse_ThrowIfNotSuccessful()
                ;
        }
    }
}
