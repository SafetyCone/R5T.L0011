using System;

using R5T.L0011.T002;


namespace System
{
    public static partial class ISyntaxExtensions
    {
        public static string Tab(this ISyntax syntax)
        {
            var output = syntax.GetSpaces(syntax.DefaultTabSpacesCount());
            return output;
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

        public static int Outdent(this ISyntax syntax, int spacesCount)
        {
            return syntax.DecrementSpacesCountByTab(spacesCount);
        }

        public static string GetIndentation(this ISyntax syntax, int spacesCount)
        {
            return syntax.GetSpaces(spacesCount);
        }
    }
}
