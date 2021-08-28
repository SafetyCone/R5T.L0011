using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.Magyar;

using Instances = R5T.L0011.X001.Instances;


namespace System
{ 
    public static class SolutionExtensions
    {
        /// <summary>
        /// Chooses <see cref="GetProjectByName(Solution, string)"/> as the default.
        /// </summary>
        public static Project GetProject(this Solution solution,
            string projectName)
        {
            var output = solution.GetProjectByName(projectName);
            return output;
        }

        public static Project GetProjectByName(this Solution solution,
            string projectName)
        {
            var wasFound = solution.HasProject(projectName);
            if (!wasFound)
            {
                throw Instances.ExceptionGenerator.SolutionDidNotHaveProjectWithName(
                    solution,
                    projectName,
                    nameof(projectName));
            }

            return wasFound;
        }

        public static Project GetProjectByFilePath(this Solution solution,
            string projectFilePath)
        {
            var wasFound = solution.HasProjectByFilePath(projectFilePath);
            if (!wasFound)
            {
                throw Instances.ExceptionGenerator.SolutionDidNotHaveProjectWithFilePath(
                    solution,
                    projectFilePath,
                    nameof(projectFilePath));
            }

            return wasFound;
        }

        /// <summary>
        /// Chooses <see cref="HasProjectByName(Solution, string)"/> as the default.
        /// </summary>
        public static WasFound<Project> HasProject(this Solution solution,
            string projectName)
        {
            var output = solution.HasProjectByName(projectName);
            return output;
        }

        public static WasFound<Project> HasProjectByName(this Solution solution,
            string projectName)
        {
            var projectOrDefault = solution.Projects
                .WhereNameIs(projectName)
                .SingleOrDefault(); // Be robust with Single().

            var output = WasFound.From(projectOrDefault);
            return output;
        }

        public static WasFound<Project> HasProjectByFilePath(this Solution solution,
            string projectFilePath)
        {
            var projectOrDefault = solution.Projects
                .WhereFilePathIs(projectFilePath)
                .SingleOrDefault(); // Be robust with Single().

            var output = WasFound.From(projectOrDefault);
            return output;
        }
    }
}


namespace R5T.L0011.X001
{
    public static class SolutionExtensions
    {
        public static WasFound<Document> HasDocumentByFilePath(this Solution solution,
            string codeFilePath)
        {
            var documentOrDefault = solution.GetAllDocuments()
                .WhereFilePathIs(codeFilePath)
                .SingleOrDefault()
                ;

            var output = WasFound.From(documentOrDefault);
            return output;
        }

        public static IEnumerable<Document> GetAllDocuments(this Solution solution)
        {
            var output = solution.Projects.
                SelectMany(project => project.Documents)
                ;

            return output;
        }

        public static Document GetDocumentByFilePath(this Solution solution,
            string codeFilePath)
        {
            var wasFound = solution.HasDocumentByFilePath(codeFilePath);
            if (!wasFound)
            {
                throw Instances.ExceptionGenerator.DocumentNotFoundWithinSolutionByFilePath(
                    solution,
                    codeFilePath,
                    nameof(codeFilePath));
            }

            return wasFound;
        }
    }
}
