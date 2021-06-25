using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;

// The 'CSharpSyntaxFactory' name is not used to allow seeing where the ISyntaxFactory extension parameter is mistakenly not used.
using SyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        public static SyntaxTrivia EmptyTrivia(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.Whitespace(String.Empty);
            return output;
        }

        public static EndRegionDirectiveTriviaSyntax EndRegionDirectiveTriviaSyntaxOnly(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.EndRegionDirectiveTrivia(true);
            return output;
        }

        public static RegionDirectiveTriviaSyntax RegionDirectiveTriviaSyntaxOnly(this ISyntaxFactory _)
        {
            var output = SyntaxFactory.RegionDirectiveTrivia(true);
            return output;
        }

        public static SyntaxTrivia PreprocessingMessage(this ISyntaxFactory _, string text)
        {
            var output = SyntaxFactory.PreprocessingMessage(text);
            return output;
        }

        public static SyntaxTrivia Trivia(this ISyntaxFactory _, StructuredTriviaSyntax structuredTriviaSyntax)
        {
            var output = SyntaxFactory.Trivia(structuredTriviaSyntax);
            return output;
        }
    }
}
