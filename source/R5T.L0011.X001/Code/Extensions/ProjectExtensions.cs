using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.Magyar;

using R5T.L0011.X001;

using Instances = R5T.L0011.X001.Instances;


namespace System
{
    public static class ProjectExtensions
    {
        public static IEnumerable<Project> RecursiveReferencedProjects(this Project project)
        {
            var recursiveReferencedProjectIds = project.RecursiveProjectReferences().ProjectIds();

            var output = project.Solution.Projects
                .Where(project => recursiveReferencedProjectIds.Contains(project.Id))
                ;

            return output;
        }

        /// <summary>
        /// Gets project references of a project, and project references of those projects references, recursively.
        /// Note: returned projects are constrained to the projects in the <see cref="Project.Solution"/> (similar to <see cref="Project.ProjectReferences"/> instead of <see cref="Project.AllProjectReferences"/>).
        /// </summary>
        public static IEnumerable<ProjectReference> RecursiveProjectReferences(this Project project)
        {
            // The project references property seems to already contain recursive project references, although I can't find anywhere this is stated.
            return project.ProjectReferences;
        }

        public static WasFound<Document> HasDocumentWithinProjectByFilePath(this Project project,
            string codeFilePath)
        {
            var documentOrDefault = project.Documents
                .WhereFilePathIs(codeFilePath)
                .SingleOrDefault()
                ;

            var output = WasFound.From(documentOrDefault);
            return output;
        }

        public static Document GetDocumentWithinProjectByFilePath(this Project project,
            string codeFilePath)
        {
            var wasFound = project.HasDocumentWithinProjectByFilePath(codeFilePath);
            if (!wasFound)
            {
                throw Instances.ExceptionGenerator.DocumentNotFoundWithinProjectByFilePath(
                    project,
                    codeFilePath,
                    nameof(codeFilePath));
            }

            return wasFound;
        }

        //public static WasFound<Document> HasDocumentWithinProjectOrReferencesByFilePath(this Project project,
        //    string codeFilePath)
        //{

        //}

        //public static Document GetDocumentWithinProjectOrReferencesByFilePath(this Project project,
        //    string codeFilePath)
        //{
        //    var meta = project.MetadataReferences.First();
        //    var proj = project.ProjectReferences.First();

        //    var document = project.refer.Solution.GetDocumentByFilePath(codeFilePath);
        //    return document;
        //}

        public static IEnumerable<ProjectId> ProjectReferenceIds(this Project project)
        {
            var output = project.ProjectReferences
                .Select(projectReference => projectReference.ProjectId)
                ;

            return output;
        }

        public static IEnumerable<Project> WhereFilePathIs(this IEnumerable<Project> projects,
            string projectFilePath)
        {
            var output = projects
                .Where(project => project.FilePath == projectFilePath)
                ;

            return output;
        }

        public static IEnumerable<Project> WhereNameIs(this IEnumerable<Project> projects,
            string projectName)
        {
            var output = projects
                .Where(project => project.Name == projectName)
                ;

            return output;
        }
    }
}
