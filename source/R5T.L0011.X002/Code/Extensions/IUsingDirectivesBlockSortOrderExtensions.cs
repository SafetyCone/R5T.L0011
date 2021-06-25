using System;

using R5T.L0011.T004;


namespace R5T.L0011.X002
{
    public static class IUsingDirectivesBlockSortOrderExtensions
    {
        private static IUsingDirectivesBlockLabel UsingDirectivesBlockLabel { get; } = T004.UsingDirectivesBlockLabel.Instance;


        public static string[] Default(this IUsingDirectivesBlockSortOrder _)
        {
            var output = new[]
            {
                UsingDirectivesBlockLabel.System(),
                UsingDirectivesBlockLabel.Microsoft(),
                UsingDirectivesBlockLabel.NamedNamespaces(),
                UsingDirectivesBlockLabel.NumberedNamespaces(),
                UsingDirectivesBlockLabel.ProjectNamespaces(),
            };

            return output;
        }
    }
}
