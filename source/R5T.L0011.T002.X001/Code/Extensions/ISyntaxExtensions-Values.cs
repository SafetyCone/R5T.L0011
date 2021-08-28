using System;

using R5T.L0011.T002;
using R5T.L0011.T002.X001;


namespace System
{
    public static partial class ISyntaxExtensions
    {
        public static string ArrayClose(this ISyntax _)
        {
            return SyntaxValues.ArrayClose;
        }

        public static string ArrayEmpty(this ISyntax _)
        {
            var output = $"{_.ArrayOpen()}{_.ArrayClose()}";
            return output;
        }

        public static string ArrayOpen(this ISyntax _)
        {
            return SyntaxValues.ArrayOpen;
        }

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

        public static string EmptyParentheses(this ISyntax _)
        {
            var output = $"{_.OpenParenthesis()}{_.CloseParenthesis()}";
            return output;
        }

        public static string Private(this ISyntax _)
        {
            return SyntaxValues.Private;
        }

        public static string Public(this ISyntax _)
        {
            return SyntaxValues.Public;
        }

        public static string OpenParenthesis(this ISyntax _)
        {
            return Strings.OpenParenthesis;
        }

        public static string SingleLineCommentPrefix(this ISyntax _)
        {
            return SyntaxValues.SingleLineCommentPrefix;
        }

        public static string Static(this ISyntax _)
        {
            return SyntaxValues.Static;
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

        public static string Var(this ISyntax _)
        {
            return SyntaxValues.Var;
        }

        public static string Void(this ISyntax _)
        {
            return SyntaxValues.Void;
        }
    }
}
