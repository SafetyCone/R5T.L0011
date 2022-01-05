using System;
using System.Text.RegularExpressions;

using R5T.L0011.T002;


namespace System
{
    public static partial class ISyntaxExtensions
    {
        public static int DecrementSpacesCountByTab(this ISyntax _, int spacesCount, int tabWidth)
        {
            var possiblyNegativeSpacesCount = spacesCount - tabWidth;

            var output = possiblyNegativeSpacesCount < 0
                ? 0
                : possiblyNegativeSpacesCount;

            return output;
        }

        public static int DecrementSpacesCountByTab(this ISyntax syntax, int spacesCount)
        {
            return syntax.DecrementSpacesCountByTab(spacesCount, syntax.DefaultTabSpacesCount());
        }

        public static string GetIndentation(this ISyntax syntax, int spacesCount)
        {
            return syntax.GetSpaces(spacesCount);
        }

        public static string GetSpaces(this ISyntax _, int spacesCount)
        {
            var output = new String(Characters.Space, spacesCount);
            return output;
        }

        public static int IncrementSpacesCountByTab(this ISyntax _, int spacesCount, int tabWidth)
        {
            var output = spacesCount + tabWidth;
            return output;
        }

        public static int IncrementSpacesCountByTab(this ISyntax syntax, int spacesCount)
        {
            return syntax.IncrementSpacesCountByTab(spacesCount, syntax.DefaultTabSpacesCount());
        }

        public static int Indent(this ISyntax syntax, int spacesCount)
        {
            return syntax.IncrementSpacesCountByTab(spacesCount);
        }

        /// <summary>
        /// Characters that are invalid in an identifier.
        /// </summary>
        public static char[] InvalidIdentifierCharacters(this ISyntax _)
        {
            var output = new[]
            {
                Characters.At,
                Characters.Colon,
                Characters.Minus,
                Characters.Period,
                Characters.Plus,
                Characters.Space,
            };

            return output;
        }

        public static int Outdent(this ISyntax syntax, int spacesCount)
        {
            return syntax.DecrementSpacesCountByTab(spacesCount);
        }

        public static string Tab(this ISyntax syntax)
        {
            var output = syntax.GetSpaces(syntax.DefaultTabSpacesCount());
            return output;
        }
    }
}
