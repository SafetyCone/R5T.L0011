using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.L0011.X000;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        public static bool IsFirstNodeInCompilationUnit(this SyntaxNode node)
        {
            var firstToken = node.GetFirstToken();

            var output = firstToken.IsFirstTokenInCompilationUnit();
            return output;
        }

        public static TNode ModifyIf_Synchronous<TNode>(this TNode node,
            bool condition,
            Func<TNode, TNode> ifTrueModificationAction)
        {
            var output = condition
                ? ifTrueModificationAction(node)
                : node
                ;

            return output;
        }

        public static async Task<TNode> ModifyIf<TNode>(this TNode node,
            bool condition,
            Func<TNode, Task<TNode>> ifTrueModificationAction)
        {
            var output = condition
                ? await ifTrueModificationAction(node)
                : node
                ;

            return output;
        }

        public static TChild GetChild_SingleOrDefault<TChild>(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.ChildNodes()
                .OfType<TChild>()
                .SingleOrDefault();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetChild_SingleOrDefault{TChild}(SyntaxNode)"/> as the default.
        /// </summary>
        public static TChild GetChild<TChild>(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetChild_SingleOrDefault<TChild>();
            return output;
        }

        public static bool HasChildOfType<TChild>(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.ChildNodes()
                .OfType<TChild>()
                .Any();

            return output;
        }

        public static bool HasParent(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.Parent is object;
            return output;
        }
    }
}