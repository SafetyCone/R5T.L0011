using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;


namespace System.Linq
{
    public static partial class SyntaxTokenListExtensions
    {
        public static SyntaxTokenList RemoveAccessModifiers(this IOperation _,
            SyntaxTokenList modifers)
        {
            var output = _.RemoveAccessModifiers_Enumerable(modifers).ToSyntaxTokenList();
            return output;
        }

        public static IEnumerable<SyntaxToken> RemoveAccessModifiers_Enumerable(this IOperation _,
            IEnumerable<SyntaxToken> modifiers)
        {
            // Only keep non-access modifiers.
            var output = modifiers
                .Where(x => x.IsNotAccessModifier())
                ;

            return output;
        }

        public static SyntaxTokenList ToSyntaxTokenList(this IEnumerable<SyntaxToken> tokens)
        {
            var output = new SyntaxTokenList(tokens);
            return output;
        }
    }
}
