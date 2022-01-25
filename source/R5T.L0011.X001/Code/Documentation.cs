using System;


namespace R5T.L0011.X001
{
    /// <summary>
    /// <para><see cref="Microsoft.CodeAnalysis"/> (Roslyn) basic extensions. (No dependencies on any other library, but allowing for <see cref="R5T.Magyar"/>.)</para>
    /// </summary>
    public sealed class Documentation
    {
        /// <summary>
        /// Dummy object for line separator XML documentation nodes.
        /// </summary>
        /// <definition>
        /// Line separator trivia is the first new line trivia after which there is no non-whitespace trivia, and that following trivia, until the end of the syntax list.
        /// Line separator trivia is defined to include the initial new line trivia.
        /// </definition>
        public readonly object LineSeparatorTrivia = null;
    }
}
