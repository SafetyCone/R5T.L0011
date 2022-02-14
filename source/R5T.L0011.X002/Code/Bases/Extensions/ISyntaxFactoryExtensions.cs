using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.L0011.T001;
using R5T.L0011.T002;
using R5T.L0011.T004;

using NameAlias = R5T.L0011.T004.NameAlias;

using CSharpSyntaxFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace System
{
    public static partial class ISyntaxFactoryExtensions
    {
        private static ISyntax Syntax { get; } = R5T.L0011.T002.Syntax.Instance;


        /// <summary>
        /// Provide just the text of the comment (no need for the leading comment marks like "//", or the wrapping marks like "/*" and "*/" for a multi-line comment).
        /// </summary>
        public static SyntaxTrivia Comment_SingleLineTextOnly(this ISyntaxFactory syntaxFactory, string text)
        {
            var entireText = $"{Syntax.SingleLineCommentPrefix()}{Strings.Space}{text}";

            var output = syntaxFactory.Comment_EntireText(entireText);
            return output;
        }

        /// <summary>
        /// Selects <see cref="Comment_SingleLineTextOnly(ISyntaxFactory, string)"/> as the default comment method.
        /// Provide just the text of the comment (no need for the leading comment marks like "//", or the wrapping marks like "/*" and "*/" for a multi-line comment).
        /// </summary>  
        public static SyntaxTrivia Comment(this ISyntaxFactory syntaxFactory, string text)
        {
            var output = syntaxFactory.Comment_SingleLineTextOnly(text);
            return output;
        }

        public static UsingDirectiveSyntax[] GetUsingDirectives(this ISyntaxFactory syntaxFactory,
            IUsingDirectivesBlock block)
        {
            var output = block.NamespaceNames
                .Select(namespaceName =>
                {
                    var usingDiretctive = syntaxFactory.Using_WithoutLeadingNewLine(namespaceName)
                        .WithEndOfLine();

                    return usingDiretctive;
                })
                .ToArray();

            return output;
        }

        public static UsingDirectiveSyntax[] GetUsingDirectives(this ISyntaxFactory syntaxFactory,
            IUsingDirectivesBlockList blockList)
        {
            var usingDirectiveSets = blockList.Blocks
                .Select(block =>
                {
                    var usingDirectiveSet = syntaxFactory.GetUsingDirectives(block);
                    return usingDirectiveSet;
                })
                ;

            var usingDirectives = new List<UsingDirectiveSyntax>();

            if(!usingDirectiveSets.Any())
            {
                return Array.Empty<UsingDirectiveSyntax>();
            }

            // Add the first set without modification.
            usingDirectiveSets.First().For(set => usingDirectives.AddRange(set));

            // For each following set, add a new line before each to separate blocks.
            usingDirectiveSets.SkipFirst().ForEach(set =>
            {
                // Modify the first using and add it.
                var modifiedFirstUsingInSet = set.First()
                    .AddLeadingLeadingTrivia(syntaxFactory.NewLine());

                usingDirectives.Add(modifiedFirstUsingInSet);

                // Just add all the rest.
                usingDirectives.AddRange(set.SkipFirst());
            });

            var output = usingDirectives.ToArray();
            return output;
        }

        public static SyntaxTrivia Space(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.Whitespace(Strings.Space);
            return output;
        }

        public static SyntaxTrivia Tab(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.Whitespace(Syntax.Tab());
            return output;
        }

        public static UsingDirectiveSyntax Using(this ISyntaxFactory _,
            NameAlias nameAlias)
        {
            var output = _.Using_WithoutLeadingNewLine(
                nameAlias.DestinationName,
                nameAlias.SourceNameExpression);

            return output;
        }

        public static SyntaxToken VarIdentifier(this ISyntaxFactory syntaxFactory)
        {
            var output = syntaxFactory.Identifier(SyntaxKind.VarKeyword, Syntax.Var());
            return output;
        }

        public static IdentifierNameSyntax VarIdentifierName(this ISyntaxFactory syntaxFactory)
        {
            var varToken = syntaxFactory.VarIdentifier();

            var varIdentifierName = syntaxFactory.IdentifierName(varToken);
            return varIdentifierName;
        }

        public static VariableDeclarationSyntax VarVariableDeclaration(this ISyntaxFactory syntaxFactory)
        {
            var varIdentifierName = syntaxFactory.VarIdentifierName();

            var varVariableDeclaration = CSharpSyntaxFactory.VariableDeclaration(varIdentifierName);
            return varVariableDeclaration;
        }

        public static VariableDeclarationSyntax VarVariableDeclaration(this ISyntaxFactory syntaxFactory,
            VariableDeclaratorSyntax variableDeclarator)
        {
            var output = syntaxFactory.VarVariableDeclaration()
                .AddVariables(variableDeclarator);

            return output;
        }
    }
}
