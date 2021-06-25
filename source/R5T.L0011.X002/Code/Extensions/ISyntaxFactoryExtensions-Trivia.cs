using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;
using R5T.L0011.T002;

using CSharpSyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        public static EndRegionDirectiveTriviaSyntax EndRegionDirectiveTriviaSyntax(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList leadingWhitespace)
        {
            var output = syntaxFactory.EndRegionDirectiveTriviaSyntaxOnly()
                .AddLineStart(leadingWhitespace)
                .PrependBlankLine(leadingWhitespace);

            return output;
        }

        public static SyntaxTrivia EndRegionDirectiveTrivia(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList leadingWhitespace)
        {
            var output = syntaxFactory.Trivia(syntaxFactory.EndRegionDirectiveTriviaSyntax(
                leadingWhitespace));

            return output;
        }

        public static SyntaxTrivia EndRegion(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList leadingWhitespace)
        {
            var output = syntaxFactory.EndRegionDirectiveTrivia(leadingWhitespace);
            return output;
        }

        public static RegionDirectiveTriviaSyntax RegionDirectiveTriviaSyntaxOnly(this ISyntaxFactory syntaxFactory, string regionName)
        {
            var output = syntaxFactory.RegionDirectiveTriviaSyntaxOnly()
                .AddTrailingTrivia(
                    syntaxFactory.Space(),
                    syntaxFactory.PreprocessingMessage(regionName));

            return output;
        }

        public static RegionDirectiveTriviaSyntax RegionDirectiveTriviaSyntax(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList leadingWhitespace,
            string regionName)
        {
            var output = syntaxFactory.RegionDirectiveTriviaSyntaxOnly(regionName)
                .AddLineStart(leadingWhitespace)
                .AppendBlankLine(leadingWhitespace);

            return output;
        }

        public static SyntaxTrivia RegionDirectiveTrivia(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList leadingWhitespace,
            string regionName)
        {
            var output = syntaxFactory.Trivia(syntaxFactory.RegionDirectiveTriviaSyntax(
                leadingWhitespace,
                regionName));

            return output;
        }

        public static SyntaxTrivia Region(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList leadingWhitespace,
            string regionName)
        {
            var output = syntaxFactory.RegionDirectiveTrivia(leadingWhitespace, regionName);
            return output;
        }
    }
}
