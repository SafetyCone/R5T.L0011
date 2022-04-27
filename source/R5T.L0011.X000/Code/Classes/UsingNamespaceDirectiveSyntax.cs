using System;


namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
    /// <summary>
    /// A type for using directives that are using name alias directives.
    /// </summary>
    public class UsingNamespaceDirectiveSyntax : TypedSyntaxNode<UsingDirectiveSyntax>
    {
        #region Static

        public static UsingNamespaceDirectiveSyntax From(UsingDirectiveSyntax usingDirective)
        {
            var output = new UsingNamespaceDirectiveSyntax(usingDirective);
            return output;
        }

        #endregion


        public UsingNamespaceDirectiveSyntax(UsingDirectiveSyntax syntaxNode)
            : base(syntaxNode)
        {
        }
    }
}
