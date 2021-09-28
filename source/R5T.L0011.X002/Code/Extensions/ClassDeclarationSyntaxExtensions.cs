using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.L0011.T001;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class ClassDeclarationSyntaxExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static ClassDeclarationSyntax AddConstructor(this ClassDeclarationSyntax @class,
            SyntaxTriviaList outerLeadingWhitespace,
            ModifierWithIndentationSynchronous<ConstructorDeclarationSyntax> premodifier = default,
            ModifierWithIndentationSynchronous<ConstructorDeclarationSyntax> modifier = default,
            ModifierWithIndentationSynchronous<BlockSyntax> bodyModifier = default)
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

        /// <summary>
        /// The <paramref name="attributeTypeName"/> value might included the "-Attribute" suffix, or it may not.
        /// This method tests for the existence of both forms.
        /// </summary>
        public static bool HasAttributeOfTypeSuffixedOrUnsuffixed(this ClassDeclarationSyntax @class,
            string attributeTypeName)
        {
            var attributeSuffixedAttributeTypeName = Instances.AttributeTypeName.GetEnsuredAttributeSuffixedTypeName(attributeTypeName);
            var hasAttributeSuffixedAttributeTypeName = @class.HasAttributeOfTypeSimple(attributeSuffixedAttributeTypeName);
            if (hasAttributeSuffixedAttributeTypeName)
            {
                // If the interface has the attribute-suffixed attribute type name, we are done.
                return hasAttributeSuffixedAttributeTypeName;
            }

            var nonAttributeSuffixedAttributeTypeName = Instances.AttributeTypeName.GetEnsuredNonAttributeSuffixedTypeName(attributeTypeName);
            var hasNonAttributeSuffixedAttributeTypeName = @class.HasAttributeOfTypeSimple(nonAttributeSuffixedAttributeTypeName);

            // At this point, we have already tested the attribute-suffixed attribute type name, so the interface either has the non-attribute-suffixed type name or it doesn't have the attribute.
            return hasNonAttributeSuffixedAttributeTypeName;
        }

        /// <summary>
        /// Selects <see cref="HasAttributeOfTypeSuffixedOrUnsuffixed(InterfaceDeclarationSyntax, string)"/> as the default.
        /// </summary>
        public static bool HasAttributeOfType(this ClassDeclarationSyntax @class,
            string attributeTypeName)
        {
            var output = @class.HasAttributeOfTypeSuffixedOrUnsuffixed(attributeTypeName);
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
