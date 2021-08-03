using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.T001;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        public static EndRegionDirectiveTriviaSyntax EndRegionDirectiveTriviaSyntax(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList indentation)
        {
            var output = syntaxFactory.EndRegionDirectiveTriviaSyntaxOnly()
                .PrependBlankLine(indentation);
            ;

            return output;
        }

        public static SyntaxTrivia EndRegionDirectiveTrivia(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList indentation)
        {
            var output = syntaxFactory.Trivia(syntaxFactory.EndRegionDirectiveTriviaSyntax(
                indentation));

            return output;
        }

        public static SyntaxTrivia EndRegion(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList indentation)
        {
            var output = syntaxFactory.EndRegionDirectiveTrivia(indentation);
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
            SyntaxTriviaList indentation,
            string regionName)
        {
            var output = syntaxFactory.Trivia(syntaxFactory.RegionDirectiveTriviaSyntax(
                indentation,
                regionName));

            return output;
        }

        public static SyntaxTrivia Region(this ISyntaxFactory syntaxFactory,
            SyntaxTriviaList indentation,
            string regionName)
        {
            var output = syntaxFactory.RegionDirectiveTrivia(indentation, regionName);
            return output;
        }
    }
}
