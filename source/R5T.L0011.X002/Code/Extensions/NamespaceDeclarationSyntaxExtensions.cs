using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;
using R5T.L0011.T003;


namespace System
{
    public static class NamespaceDeclarationSyntaxExtensions
    {
        private static ISignatureModel SignatureModel { get; } = R5T.L0011.T003.SignatureModel.Instance;
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static NamespaceDeclarationSyntax AddClass(this NamespaceDeclarationSyntax namespaceDeclarationSyntax, string className,
            MemberSignatureModel signatureModel,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithLineLeadingWhitespace<ClassDeclarationSyntax> modifier)
        {
            var indentedLeadingWhitespace = leadingWhitespace.IndentByTab();

            var @class = SyntaxFactory.Class(className)
                .ApplyMemberSignatureModel(signatureModel)
                .NormalizeWhitespace()
                .WithLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace())
                .WithOpenBraceToken(SyntaxFactory.OpenBrace(leadingWhitespace, false))
                .WithCloseBraceToken(SyntaxFactory.CloseBrace(leadingWhitespace))
                .ModifyWith(indentedLeadingWhitespace, modifier)
                ;

            var output = namespaceDeclarationSyntax.AddMembers(@class);
            return output;
        }

        public static NamespaceDeclarationSyntax AddClass(this NamespaceDeclarationSyntax namespaceDeclarationSyntax, string className,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithLineLeadingWhitespace<ClassDeclarationSyntax> modifier)
        {
            var signatureModel = SignatureModel.GetClassDefault();

            var output = namespaceDeclarationSyntax.AddClass(className, signatureModel, leadingWhitespace, modifier);
            return output;
        }

        public static NamespaceDeclarationSyntax AddInterfaceV01(this NamespaceDeclarationSyntax namespaceDeclarationSyntax, string interfaceName,
            MemberSignatureModel signatureModel,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithLineLeadingWhitespace<InterfaceDeclarationSyntax> modifier)
        {
            var indentedLeadingWhitespace = leadingWhitespace.IndentByTab();

            var @interface = SyntaxFactory.Interface(interfaceName)
                .ApplyMemberSignatureModel(signatureModel)
                .NormalizeWhitespace()
                .WithLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace())
                .WithOpenBraceToken(SyntaxFactory.OpenBrace(leadingWhitespace, false))
                .WithCloseBraceToken(SyntaxFactory.CloseBrace(leadingWhitespace))
                .ModifyWith(indentedLeadingWhitespace, modifier)
                ;

            var output = namespaceDeclarationSyntax.AddMembers(@interface);
            return output;
        }

        public static NamespaceDeclarationSyntax AddInterfaceV01(this NamespaceDeclarationSyntax namespaceDeclarationSyntax, string interfaceName,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithLineLeadingWhitespace<InterfaceDeclarationSyntax> modifier)
        {
            var signatureModel = SignatureModel.GetInterfaceDefault();

            var output = namespaceDeclarationSyntax.AddInterfaceV01(interfaceName, signatureModel, leadingWhitespace, modifier);
            return output;
        }

        public static NamespaceDeclarationSyntax WithCloseBrace(this NamespaceDeclarationSyntax namespaceDeclarationSyntax,
            SyntaxTriviaList leadingWhitespace, bool appendNewLine = false)
        {
            var output = namespaceDeclarationSyntax
                .WithCloseBraceToken(SyntaxFactory.CloseBrace(leadingWhitespace, appendNewLine));

            return output;
        }

        public static NamespaceDeclarationSyntax WithOpenBrace(this NamespaceDeclarationSyntax namespaceDeclarationSyntax,
            SyntaxTriviaList leadingWhitespace, bool prependNewLine = true)
        {
            var output = namespaceDeclarationSyntax
                .WithOpenBraceToken(SyntaxFactory.OpenBrace(leadingWhitespace, prependNewLine));

            return output;
        }
    }
}
