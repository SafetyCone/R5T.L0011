using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.L0011.T001;


namespace System
{
    public static class ClassDeclarationSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static ClassDeclarationSyntax AddConstructor(this ClassDeclarationSyntax @class,
            SyntaxTriviaList outerLeadingWhitespace,
            ModifierWithIndentation<ConstructorDeclarationSyntax> premodifier = default,
            ModifierWithIndentation<ConstructorDeclarationSyntax> modifier = default,
            ModifierWithIndentation<BlockSyntax> bodyModifier = default)
        {
            var constructor = SyntaxFactory.Constructor(@class.Identifier)
                .ModifyWith(outerLeadingWhitespace, premodifier)
                .NormalizeWhitespace()
                .WithLeadingTrivia(outerLeadingWhitespace.GetNewLineLeadingWhitespace())
                .ModifyWith(outerLeadingWhitespace, modifier)
                .AddBody(outerLeadingWhitespace, bodyModifier)
                ;

            var output = @class.AddMembers(constructor);
            return output;
        }

        public static WasFound<MethodDeclarationSyntax> HasMethod(this ClassDeclarationSyntax @class,
            string methodName)
        {
            var methodOrDefault = @class.Members
                .OfType<MethodDeclarationSyntax>()
                .Where(x => x.Identifier.Text == methodName)
                .SingleOrDefault();

            var output = WasFound.From(methodOrDefault);
            return output;
        }
    }
}
