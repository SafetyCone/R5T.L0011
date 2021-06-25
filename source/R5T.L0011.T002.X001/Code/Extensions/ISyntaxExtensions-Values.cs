using System;

using R5T.L0011.T002;
using R5T.L0011.T002.X001;


namespace System
{
    public static partial class ISyntaxExtensions
    {
        public static string CloseParenthesis(this ISyntax _)
        {
            return Strings.CloseParenthesis;
        }

        public static int DefaultTabSpacesCount(this ISyntax _)
        {
            return SyntaxValues.DefaultTabSpacesCount;
        }

        public static string DocumentationCommentExterior(this ISyntax _)
        {
            return SyntaxValues.DocumentationCommentExterior;
        }

        public static string EmptyParentheses(this ISyntax syntax)
        {
            var output = $"{syntax.OpenParenthesis()}{syntax.CloseParenthesis()}";
            return output;
        }

        public static string OpenParenthesis(this ISyntax _)
        {
            return Strings.OpenParenthesis;
        }

        public static string SingleLineCommentPrefix(this ISyntax _)
        {
            return SyntaxValues.SingleLineCommentPrefix;
        }

        public static string This(this ISyntax _)
        {
            return SyntaxValues.This;
        }

        public static string TypeArgumentsListClose(this ISyntax _)
        {
            return SyntaxValues.TypeArgumentsListClose;
        }

        public static string TypeArgumentsListOpen(this ISyntax _)
        {
            return SyntaxValues.TypeArgumentsListOpen;
        }

        public static string VarText(this ISyntax _)
        {
            return SyntaxValues.VarText;
        }
    }
}
