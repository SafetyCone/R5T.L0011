using System;


namespace Microsoft.CodeAnalysis
{
    public delegate T ModifierWithIndentationSynchronous<T>(T syntaxNode, SyntaxTriviaList indentation)
        where T : SyntaxNode;
}
