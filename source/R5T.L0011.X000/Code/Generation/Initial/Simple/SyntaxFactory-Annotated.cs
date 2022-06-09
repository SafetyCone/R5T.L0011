using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace R5T.L0011.X000.Generation.Initial.Simple
{
    /// <summary>
    /// Static methods for *simple* generation of syntax elements.
    /// </summary>
    public static partial class SyntaxFactory
    {
        public static AnnotatedNode<AccessorDeclarationSyntax> CreateAccessorGet_NoSemicolon_Annotated()
        {
            var accessorGet = SyntaxFactory.CreateAccessorGet();

            var output = AnnotatedNode.From(accessorGet);
            return output;
        }

        public static AnnotatedNode<PropertyDeclarationSyntax> CreateProperty_Annotated(
            string typeName,
            string propertyName)
        {
            var property = SyntaxFactory.CreateProperty(
                typeName,
                propertyName);

            var output = AnnotatedNode.From(property);
            return output;
        }
    }
}
