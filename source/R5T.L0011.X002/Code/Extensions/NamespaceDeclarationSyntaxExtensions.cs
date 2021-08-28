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
            ModifierWithIndentationSynchronous<ClassDeclarationSyntax> modifier)
        {
            var indentedLeadingWhitespace = leadingWhitespace.IndentByTab();

            var @class = SyntaxFactory.Class(className)
                .ApplyMemberSignatureModel(signatureModel)
                .NormalizeWhitespace()
                .WithLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace())
                .WithOpenBraceToken(SyntaxFactory.OpenBrace2(leadingWhitespace, false))
                .WithCloseBraceToken(SyntaxFactory.CloseBrace2(leadingWhitespace))
                .ModifyWith(indentedLeadingWhitespace, modifier)
                ;

            var output = namespaceDeclarationSyntax.AddMembers(@class);
            return output;
        }

        public static NamespaceDeclarationSyntax AddClass(this NamespaceDeclarationSyntax namespaceDeclarationSyntax, string className,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithIndentationSynchronous<ClassDeclarationSyntax> modifier)
        {
            var signatureModel = SignatureModel.GetClassDefault();

            var output = namespaceDeclarationSyntax.AddClass(className, signatureModel, leadingWhitespace, modifier);
            return output;
        }

        public static NamespaceDeclarationSyntax AddInterfaceV01(this NamespaceDeclarationSyntax namespaceDeclarationSyntax, string interfaceName,
            MemberSignatureModel signatureModel,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithIndentationSynchronous<InterfaceDeclarationSyntax> modifier)
        {
            var indentedLeadingWhitespace = leadingWhitespace.IndentByTab();

            var @interface = SyntaxFactory.Interface(interfaceName)
                .ApplyMemberSignatureModel(signatureModel)
                .NormalizeWhitespace()
                .WithLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace())
                .WithOpenBraceToken(SyntaxFactory.OpenBrace2(leadingWhitespace, false))
                .WithCloseBraceToken(SyntaxFactory.CloseBrace2(leadingWhitespace))
                .ModifyWith(indentedLeadingWhitespace, modifier)
                ;

            var output = namespaceDeclarationSyntax.AddMembers(@interface);
            return output;
        }

        public static NamespaceDeclarationSyntax AddInterfaceV01(this NamespaceDeclarationSyntax namespaceDeclarationSyntax, string interfaceName,
            SyntaxTriviaList leadingWhitespace,
            ModifierWithIndentationSynchronous<InterfaceDeclarationSyntax> modifier)
        {
            var signatureModel = SignatureModel.GetInterfaceDefault();

            var output = namespaceDeclarationSyntax.AddInterfaceV01(interfaceName, signatureModel, leadingWhitespace, modifier);
            return output;
        }

        public static NamespaceDeclarationSyntax WithCloseBrace(this NamespaceDeclarationSyntax namespaceDeclarationSyntax,
            SyntaxTriviaList indentation)
        {
            var output = namespaceDeclarationSyntax
                .WithCloseBraceToken(SyntaxFactory.CloseBrace(indentation));

            return output;
        }

        public static NamespaceDeclarationSyntax WithOpenBrace(this NamespaceDeclarationSyntax namespaceDeclarationSyntax,
            SyntaxTriviaList indentation)
        {
            // Obnoxiously, the namespace adds its own new line after its declaration, before its open brace.
            // So we need to remove the new line from the desired indentation of the open brace.
            var actualIndentation = indentation.RemoveLeadingNewLineNotIncludingStructuredTrivia();

            var output = namespaceDeclarationSyntax
                .WithOpenBraceToken(SyntaxFactory.OpenBrace(actualIndentation));

            return output;
        }
    }
}
