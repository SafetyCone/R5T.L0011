using System;
using System.Linq;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class ProjectExtensions
    {
        public static Document GetDocumentByFilePath(this Project project,
            string codeFilePath)
        {
            var output = project.Documents
                .Where(x => x.FilePath == codeFilePath)
                .Single();

            return output;
        }
    }
}
