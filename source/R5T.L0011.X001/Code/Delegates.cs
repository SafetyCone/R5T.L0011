using System;


namespace Microsoft.CodeAnalysis
{
    public delegate T ModifierWithIndentation<T>(T syntaxNode, SyntaxTriviaList indentation)
        where T : SyntaxNode;
}
