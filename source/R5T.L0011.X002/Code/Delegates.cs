using System;

using R5T.L0011.T004;


namespace Microsoft.CodeAnalysis
{
    public delegate T SyntaxNodeModifier<T>(T syntaxNode, SyntaxTriviaList outerIndentation, INamespaceNameSet namespaceNames)
        where T : SyntaxNode;
}
