using System;


namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
    public class UsingNameAliasDirectiveSyntax : TypedSyntaxNode<UsingDirectiveSyntax>
    {
        #region Static

        public static UsingNameAliasDirectiveSyntax From(UsingDirectiveSyntax usingDirective)
        {
            var output = new UsingNameAliasDirectiveSyntax(usingDirective);
            return output;
        }

        #endregion


        public UsingNameAliasDirectiveSyntax(UsingDirectiveSyntax syntaxNode)
            : base(syntaxNode)
        {
        }
    }
}
