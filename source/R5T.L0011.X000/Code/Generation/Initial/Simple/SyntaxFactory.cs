using System;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using CSharpSyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace R5T.L0011.X000.Generation.Initial.Simple
{
    /// <summary>
    /// Static methods for *simple* generation of syntax elements.
    /// </summary>
    public static partial class SyntaxFactory
    {
        public static AccessorDeclarationSyntax CreateAccessorGet(
            bool withSemicolon = true)
        {
            var output = CSharpSyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                .ModifyIf(withSemicolon,
                    x => x.WithSemicolonToken())
                ;

            return output;
        }

        public static BaseTypeSyntax CreateBaseType(string typeName)
        {
            var type = SyntaxFactory.CreateType(typeName);

            var output = CSharpSyntaxFactory.SimpleBaseType(type);
            return output;
        }

        public static PropertyDeclarationSyntax CreateProperty(
            string typeName,
            string propertyName)
        {
            var type = SyntaxFactory.CreateType(typeName);

            var output = CSharpSyntaxFactory.PropertyDeclaration(type, propertyName);
            return output;
        }

        public static TypeSyntax CreateType(string name)
        {
            return CSharpSyntaxFactory.IdentifierName(name);
        }
    }
}
