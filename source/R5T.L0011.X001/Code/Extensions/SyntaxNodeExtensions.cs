using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class SyntaxNodeExtensions
    {
        public static TChild GetChildOfType_SingleOrDefault<TChild>(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.ChildNodes()
                .OfType<TChild>()
                .SingleOrDefault();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetChildOfType_SingleOrDefault{TChild}(SyntaxNode)"/> as the default.
        /// </summary>
        public static TChild GetChildOfType<TChild>(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetChildOfType_SingleOrDefault<TChild>();
            return output;
        }

        public static bool HasChildOfType<TChild>(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.ChildNodes()
                .OfType<TChild>()
                .Any();

            return output;
        }

        public static TSyntaxNode SetIndentation<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.WithLeadingTrivia(indentation.ToArray());
            return output;
        }

        public static TSyntaxNode SetIndentationWithoutLeadingNewLine<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var actualIndentation = indentation.RemoveAt(0);

            var output = syntaxNode.WithLeadingTrivia(actualIndentation.ToArray());
            return output;
        }

        public static TSyntaxNode Indent<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddLeadingLeadingTrivia(indentation.ToArray());
            return output;
        }

        public static TSyntaxNode IndentWihtoutNewLine<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList indentation)
            where TSyntaxNode : SyntaxNode
        {
            var actualIndentation = indentation.RemoveAt(0);

            var output = syntaxNode.AddLeadingLeadingTrivia(actualIndentation.ToArray());
            return output;
        }

        public static TSyntaxNode AddLeadingLeadingTrivia<TSyntaxNode>(this TSyntaxNode syntaxNode, params SyntaxTrivia[] trivia)
            where TSyntaxNode : SyntaxNode
        {
            var newLeadingTrivia = syntaxNode.HasLeadingTrivia
                ? syntaxNode.GetLeadingTrivia().InsertRange(0, trivia)
                : new SyntaxTriviaList(trivia);

            var output = syntaxNode.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static TSyntaxNode AddTrailingLeadingTrivia<TSyntaxNode>(this TSyntaxNode syntaxNode, params SyntaxTrivia[] trivia)
            where TSyntaxNode : SyntaxNode
        {
            var newLeadingTrivia = syntaxNode.HasLeadingTrivia
                ? syntaxNode.GetLeadingTrivia().AddRange(trivia)
                : new SyntaxTriviaList(trivia);

            var output = syntaxNode.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static TSyntaxNode AddLeadingTrivia<TSyntaxNode>(this TSyntaxNode syntaxNode, params SyntaxTrivia[] trivia)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddTrailingLeadingTrivia(trivia);
            return output;
        }

        public static TSyntaxNode AddLeadingWhitespace<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddLeadingLeadingTrivia(leadingWhitespace.ToArray());
            return output;
        }

        public static IEnumerable<TSyntaxNode> AddLeadingWhitespace<TSyntaxNode>(this IEnumerable<TSyntaxNode> syntaxNodes,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNodes
                .Select(syntaxNode =>
                {
                    var syntaxNodeOutput = syntaxNode.AddLeadingWhitespace(leadingWhitespace);
                    return syntaxNodeOutput;
                });

            return output;
        }

        public static TSyntaxNode AddTrailingTrailingTrivia<TSyntaxNode>(this TSyntaxNode syntaxNode, params SyntaxTrivia[] trivia)
            where TSyntaxNode : SyntaxNode
        {
            var newTrailingTrivia = syntaxNode.HasTrailingTrivia
                ? syntaxNode.GetTrailingTrivia().AddRange(trivia)
                : new SyntaxTriviaList(trivia);

            var output = syntaxNode.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static TSyntaxNode AddLeadingTrailingTrivia<TSyntaxNode>(this TSyntaxNode syntaxNode, params SyntaxTrivia[] trivia)
            where TSyntaxNode : SyntaxNode
        {
            var newTrailingTrivia = syntaxNode.HasTrailingTrivia
                ? syntaxNode.GetTrailingTrivia().InsertRange(0, trivia)
                : new SyntaxTriviaList(trivia);

            var output = syntaxNode.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static TSyntaxNode AddTrailingTrivia<TSyntaxNode>(this TSyntaxNode syntaxNode, params SyntaxTrivia[] trivia)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.AddTrailingTrailingTrivia(trivia);
            return output;
        }

        public static IEnumerable<TDescendantSyntaxNodeType> GetDescendantsOfType<TSyntaxNode, TDescendantSyntaxNodeType>(this TSyntaxNode syntaxNode)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.DescendantNodes()
                .OfType<TDescendantSyntaxNodeType>()
                ;

            return output;
        }

        public static IEnumerable<InterfaceDeclarationSyntax> GetInterfaces<TSyntaxNode>(this TSyntaxNode syntaxNode)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>()
                ;

            return output;
        }

        public static TSyntaxNode ModifyWith<TSyntaxNode>(this TSyntaxNode syntaxNode, SyntaxTriviaList lineLeadingWhitespace,
            ModifierWithIndentationSynchronous<TSyntaxNode> modifier)
            where TSyntaxNode : SyntaxNode
        {
            var output = modifier is object
                ? modifier(syntaxNode, lineLeadingWhitespace)
                : syntaxNode;

            return output;
        }

        public static TSyntaxNode PrependLeadingWhitespace<TSyntaxNode>(this TSyntaxNode syntaxNode,
            SyntaxAnnotation annotation,
            SyntaxTriviaList leadingWhitespace)
            where TSyntaxNode : SyntaxNode
        {
            var output = syntaxNode.ReplaceNodesAndTokens(
                theSyntaxNode => theSyntaxNode.GetAnnotatedNodesAndTokens(annotation),
                node => node.AddLeadingWhitespace(leadingWhitespace),
                token => token.AddLeadingWhitespace(leadingWhitespace));

            return output;
        }

        public static TSyntaxNode ReplaceNodesAndTokens<TSyntaxNode>(this TSyntaxNode syntaxNode,
            Func<TSyntaxNode, IEnumerable<SyntaxNode>> syntaxNodeSelector,
            Func<TSyntaxNode, IEnumerable<SyntaxToken>> syntaxTokenSelector,
            Func<SyntaxNode, SyntaxNode> nodeTransformer,
            Func<SyntaxToken, SyntaxToken> tokenTransformer)
            where TSyntaxNode : SyntaxNode
        {
            var modifiedSyntaxNode = syntaxNode;

            var nodes = syntaxNodeSelector(modifiedSyntaxNode);

            modifiedSyntaxNode = modifiedSyntaxNode.ReplaceNodes(nodes, (originalNode, node) =>
            {
                return nodeTransformer(node);
            });

            var tokens = syntaxTokenSelector(modifiedSyntaxNode);

            return modifiedSyntaxNode.ReplaceTokens(tokens, (originalToken, token) =>
            {
                return tokenTransformer(token);
            });
        }

        public static TSyntaxNode ReplaceNodesAndTokens<TSyntaxNode>(this TSyntaxNode syntaxNode,
            Func<TSyntaxNode, IEnumerable<SyntaxNodeOrToken>> selector,
            Func<SyntaxNode, SyntaxNode> nodeTransformer,
            Func<SyntaxToken, SyntaxToken> tokenTransformer)
            where TSyntaxNode : SyntaxNode
        {
            IEnumerable<SyntaxNode> GetNodes(TSyntaxNode compilationUnit)
            {
                return selector(compilationUnit)
                    .Where(x => x.IsNode)
                    .Select(x => x.AsNode());
            }

            IEnumerable<SyntaxToken> GetTokens(TSyntaxNode compilationUnit)
            {
                return selector(compilationUnit)
                    .Where(x => x.IsToken)
                    .Select(x => x.AsToken());
            }

            return syntaxNode.ReplaceNodesAndTokens(GetNodes, GetTokens, nodeTransformer, tokenTransformer);
        }

        public static void WriteTo(this SyntaxNode syntaxNode, string filePath)
        {
            using var fileWriter = new StreamWriter(filePath);

            syntaxNode.WriteTo(fileWriter);
        }
    }
}
