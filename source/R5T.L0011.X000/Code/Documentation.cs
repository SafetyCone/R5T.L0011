using System;


namespace R5T.L0011.X000
{
    public static class Documentation
    {
        /// <summary>
        /// all we care about is whether the using exists, not if the using is unique.
        /// </summary>
        public static readonly object AllWeCareAboutIsUsingExistsNotUnique;
        /// <summary>
        /// A blank line is defined as two new lines either consecutive, or separated only by whitespace.
        /// </summary>
        public static readonly object BlankLineTriviaDefinition;
        /// <summary>
        /// Separating trivia is either leading or trailing, and is the concatenation of the trailing trivia of the prior token with the leading trivia of the token (leading separating trivia), or the concatenation of the trailing trivia of the token with the leading trivia of the following token (trailing separating trivia).
        /// </summary>
        public static readonly object DefinitionOfSeparatingTrivia;
        /// <summary>
        /// assumption that the using namespace directives in a namespace are unique.
        /// This is to say, it's assumed that there will never be a situation where two using directives for the same namespace exist:
        /// <code>
        /// using System;
        /// using System;
        /// </code>
        /// This situation is technically allowed, since it is only a compiler warning "CS0105: The using directive for 'System' appeared previously in this namespace" and not a compilation error.
        /// </summary>
        public static readonly object UsingNamespaceDirectivesAreUniqueAssumption;
    }
}