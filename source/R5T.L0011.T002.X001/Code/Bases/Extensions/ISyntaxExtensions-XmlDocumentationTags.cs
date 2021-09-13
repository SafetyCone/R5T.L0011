using System;

using R5T.L0011.T002;
using R5T.L0011.T002.X001;


namespace System
{
    public static partial class ISyntaxExtensions
    {
        public static string SummaryTag(this ISyntax _)
        {
            return XmlDocumentationTags.Summary;
        }
    }
}
