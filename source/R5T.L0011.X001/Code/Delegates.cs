using System;


namespace Microsoft.CodeAnalysis
{
    public delegate T ModifierWithLineLeadingWhitespace<T>(T syntaxNode, SyntaxTriviaList leadingWhitespace)
        where T : SyntaxNode;
}
