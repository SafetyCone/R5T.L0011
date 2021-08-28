using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class ProjectReferenceExtensions
    {
        public static IEnumerable<ProjectId> ProjectIds(this IEnumerable<ProjectReference> projectReferences)
        {
            var output = projectReferences.Select(x => x.ProjectId);
            return output;
        }
    }
}
