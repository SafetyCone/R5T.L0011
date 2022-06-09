using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;



namespace R5T.L0011.X000.Generation.Initial.Parse
{
    public static partial class SyntaxParser
    {
        public static WasSuccess<TNode> SimpleParse_ValidateResult<TNode>(
            TNode parseResult,
            Func<TNode, WasSuccess> validator)
            where TNode : SyntaxNode
        {
            var validateWasSuccess = validator(parseResult);

            var output = WasSuccess.From(
                parseResult,
                validateWasSuccess);

            return output;
        }

        public static TNode SimpleParse_ThrowIfNotSuccessful<TNode>(
            WasSuccess<TNode> parseWasSuccess)
        {
            if(!parseWasSuccess)
            {
                throw new Exception($"Simple parse failed: {parseWasSuccess.FailureMessage}");
            }

            return parseWasSuccess.Result;
        }
    }
}
