using System;

using Microsoft.CodeAnalysis.CSharp;


namespace R5T.L0011.X000
{
    public static class Glossary
    {
        public static class ForTrivia
        {
            /// <summary>
            /// <inheritdoc cref="BlankTrivia" path="/definition"/>
            /// </summary>
            /// <definition>Blank trivia is the combination of both <inheritdoc cref="WhitespaceTrivia" path="/name"/> and <inheritdoc cref="NewLineTrivia" path="/name"/>. Blank trivia is what actually determines the spacing between text on the code canvas.</definition>
            /// <name><i>blank trivia</i></name>
            public static readonly object BlankTrivia;
            /// <summary>
            /// <inheritdoc cref="Exceeds" path="/definition"/>
            /// </summary>
            /// <definition>If one trivia exceeds another, it is longer.</definition>
            public static readonly object Exceeds;
            /// <summary>
            /// <inheritdoc cref="ExternalTrivia" path="/definition"/>
            /// </summary>
            /// <definition>External trivia is the combination of leading and trailing trivia.</definition>
            public static readonly object ExternalTrivia;
            /// <summary>
            /// <inheritdoc cref="Indentation" path="/definition"/>
            /// </summary>
            /// <definition>Indentation is leading trivia composed only of <inheritdoc cref="BlankTrivia" path="/name"/>. Indentation usally begins with a new line, followed by <inheritdoc cref="Tabination" path="/name"/>.</definition>
            /// <name><i>indentation</i></name>
            public static readonly object Indentation;
            /// <summary>
            /// <inheritdoc cref="InternalTrivia" path="/definition"/>
            /// </summary>
            /// <definition>Internal trivia is the leading and trailing trivia of child syntax elements within the parent syntax element.</definition>
            public static readonly object InternalTrivia;
            /// <summary>
            /// <inheritdoc cref="NewLineTrivia" path="/definition"/>
            /// </summary>
            /// <definition>New line trivia is composed only of syntax trivia with the <see cref="SyntaxKind.EndOfLineTrivia"/>.</definition>
            /// <name><i>new line trivia</i></name>
            public static readonly object NewLineTrivia;
            /// <summary>
            /// <inheritdoc cref="SeparatingTrivia" path="/definition"/>
            /// </summary>
            /// <definition>Separating trivia is the trivia between two consecutive token.</definition>
            /// <name><i>separating trivia</i></name>
            public static readonly object SeparatingTrivia;
            /// <summary>
            /// <inheritdoc cref="Tabination" path="/definition"/>
            /// </summary>
            /// <definition>Tabination is leading trivia composed only of <inheritdoc cref="WhitespaceTrivia" path="/name"/> with no <inheritdoc cref="NewLineTrivia" path="/name"/>.</definition>
            /// <name><i>tabination</i></name>
            public static readonly object Tabination;
            /// <summary>
            /// <inheritdoc cref="WhitespaceTrivia" path="/definition"/>
            /// </summary>
            /// <definition>Whitespace trivia is composed only of syntax trivia with the <see cref="SyntaxKind.WhitespaceTrivia"/>.</definition>
            /// <name><i>whitespace trivia</i></name>
            public static readonly object WhitespaceTrivia;
        }
    }
}
