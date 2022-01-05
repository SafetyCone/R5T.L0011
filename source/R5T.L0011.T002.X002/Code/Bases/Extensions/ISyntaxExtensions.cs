using System;
using System.Text.RegularExpressions;

using R5T.L0011.T002;

using Instances = R5T.L0011.T002.X002.Instances;


namespace System
{
    public static class ISyntaxExtensions
    {
        public static char GetIdentifierCompliantSpaceCharacter(this ISyntax _)
        {
            return Characters.Underscore;
        }

        public static string GetIdentifierCompliantName(this ISyntax _,
            string name)
        {
            // Replace anything that's not alphanumeric with an underscore.
            var pattern = Instances.RegexPattern.AllNonAlphanumeric();

            var replacement = _.GetIdentifierCompliantSpaceCharacter();

            var output = Regex.Replace(name, pattern, replacement.ToString());
            return output;
        }
    }
}