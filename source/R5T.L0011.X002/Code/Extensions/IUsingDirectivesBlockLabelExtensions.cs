using System;

using R5T.L0011.T004;


namespace R5T.L0011.X002
{
    public static class IUsingDirectivesBlockLabelExtensions
    {
        public static string Microsoft(this IUsingDirectivesBlockLabel _)
        {
            return UsingDirectiveBlockLabels.Microsoft;
        }

        public static string NamedNamespaces(this IUsingDirectivesBlockLabel _)
        {
            return UsingDirectiveBlockLabels.NamedNamespaces;
        }

        public static string NumberedNamespaces(this IUsingDirectivesBlockLabel _)
        {
            return UsingDirectiveBlockLabels.NumberedNamespaces;
        }

        public static string ProjectNamespaces(this IUsingDirectivesBlockLabel _)
        {
            return UsingDirectiveBlockLabels.ProjectNamespaces;
        }

        public static string System(this IUsingDirectivesBlockLabel _)
        {
            return UsingDirectiveBlockLabels.System;
        }

        public static string Uncategorized(this IUsingDirectivesBlockLabel _)
        {
            return UsingDirectiveBlockLabels.Uncategorized;
        }
    }
}
