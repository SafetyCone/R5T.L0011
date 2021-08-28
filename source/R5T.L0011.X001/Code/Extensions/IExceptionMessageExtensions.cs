using System;

using Microsoft.CodeAnalysis;

using R5T.Magyar.T002;


namespace R5T.L0011.X001
{
    public static class IExceptionMessageExtensions
    {
        public static string DocumentNotFoundWithinProjectByFilePath(this IExceptionMessageGenerator _,
            Project project,
            string codeFilePath)
        {
            var output = $"Code file:\n{codeFilePath}\n\nnot found within project '{project.Name}':\n{project.FilePath}";
            return output;
        }

        public static string DocumentNotFoundWithinSolutionByFilePath(this IExceptionMessageGenerator _,
            Solution solution,
            string codeFilePath)
        {
            var output = $"Code file:\n{codeFilePath}\n\nnot found within solution:\n{solution.FilePath}";
            return output;
        }

        public static string MethodParameterNotFound(this IExceptionMessageGenerator _,
            string methodName,
            string parameterName)
        {
            var output = $"Parameter '{parameterName}' on method '{methodName}' not found.";
            return output;
        }

        public static string MethodWasNotAnExtensionMethod(this IExceptionMessageGenerator _,
            IMethodSymbol methodSymbol)
        {
            var output = $"Method '{methodSymbol}' was not an extension method.";
            return output;
        }

        public static string ParameterNotFound(this IExceptionMessageGenerator _,
            string parameterName)
        {
            var output = $"Parameter '{parameterName}' not found.";
            return output;
        }

        public static string SolutionDidNotHaveProjectWithFilePath(this IExceptionMessageGenerator _,
            Solution solution,
            string projectFilePath)
        {
            var output = $"Project with project file path not found. Project:\n{projectFilePath}.\nSolution: {solution.Id}\n{solution.FilePath}";
            return output;
        }

        public static string SolutionDidNotHaveProjectWithName(this IExceptionMessageGenerator _,
            Solution solution,
            string projectName)
        {
            var output = $"Project '{projectName}' not found. Solution: {solution.Id}\n{solution.FilePath}";
            return output;
        }

        public static string StatementNotFound(this IExceptionMessageGenerator _,
            string statementText)
        {
            var output = $"Statement not found. Statement:\n{statementText}";
            return output;
        }

        public static string TypeNotFoundInCompilation(this IExceptionMessageGenerator _,
            Compilation compilation,
            string namespacedTypeName)
        {
            var output = $"Type '{namespacedTypeName}' not found in compilation with assembly name '{compilation.AssemblyName}'.";
            return output;
        }
    }
}
