using System;
using System.Linq;

using R5T.L0011.T001;

using Microsoft.CodeAnalysis;

using CSharpSyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static class SyntaxNodeExtensions
    {
        private static ISyntaxFactory SyntaxFactory { get; } = R5T.L0011.T001.SyntaxFactory.Instance;


        public static TSyntaxNode AddLineStart<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddLeadingLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static TSyntaxNode AppendBlankLine<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddTrailingTrailingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static TSyntaxNode InsertLineStart<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddTrailingLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static TSyntaxNode PrependBlankLine<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList leadingWhitespace,
            bool actuallyPrependTheLine = true)
            where TSyntaxNode : SyntaxNode
        {
            if(!actuallyPrependTheLine)
            {
                return syntaxNode;
            }

            var output = syntaxNode.AddLeadingLeadingTrivia(leadingWhitespace.GetNewLineLeadingWhitespace().ToArray());
            return output;
        }

        public static TSyntaxNode PrependBlankLine<TSyntaxNode>(this TSyntaxNode syntaxNode)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.PrependBlankLine(new SyntaxTriviaList());
            return output;
        }

        public static TSyntaxNode ModifyWith<TSyntaxNode>(this TSyntaxNode syntaxNode, Modifier<TSyntaxNode> modifier)
            where TSyntaxNode : SyntaxNode
        {
            var output = modifier is object
                ? modifier(syntaxNode)
                : syntaxNode;

            return output;
        }

        public static TSyntaxNode ModifyWith<TSyntaxNode, TData>(this TSyntaxNode syntaxNode, ModifierWith<TSyntaxNode, TData> modifier, TData data)
            where TSyntaxNode : SyntaxNode
        {
            var output = modifier is object
                ? modifier(syntaxNode, data)
                : syntaxNode;

            return output;
        }

        public static TSyntaxNode WrapWithRegion<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList leadingWhitespace,
            string regionName)
            where TSyntaxNode : SyntaxNode
        {
            //var region = CSharpSyntaxFactory.RegionDirectiveTrivia(true)
            //    .AddLineStart(leadingWhitespace)
            //    .AppendBlankLine(leadingWhitespace);

            //var endRegion = CSharpSyntaxFactory.EndRegionDirectiveTrivia(true)
            //    .AddLineStart(leadingWhitespace)
            //    .PrependBlankLine(leadingWhitespace);

            //var output = syntaxNode
            //    .AddLeadingLeadingTrivia(SyntaxFactory.Trivia(region))
            //    .AddTrailingTrailingTrivia(SyntaxFactory.Trivia(endRegion))
            //    ;

            var region = SyntaxFactory.Region(leadingWhitespace, regionName);

            var endRegion = SyntaxFactory.EndRegion(leadingWhitespace);

            var output = syntaxNode
                .AddLeadingLeadingTrivia(region)
                .AddTrailingTrailingTrivia(endRegion)
                ;

            return output;
        }
    }
}
