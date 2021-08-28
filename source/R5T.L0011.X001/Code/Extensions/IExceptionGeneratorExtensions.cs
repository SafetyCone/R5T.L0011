using System;

using Microsoft.CodeAnalysis;

using R5T.Magyar.T002;

using R5T.L0011.X001;

using Instances = R5T.L0011.X001.Instances;


namespace System
{
    public static class IExceptionGeneratorExtensions
    {
        public static ArgumentException DocumentNotFoundWithinProjectByFilePath(this IExceptionGenerator _,
            Project project,
            string codeFilePath,
            string argumentName)
        {
            var message = Instances.ExceptionMessageGenerator.DocumentNotFoundWithinProjectByFilePath(
                project,
                codeFilePath);

            var output = new ArgumentException(message, argumentName);
            return output;
        }

        public static ArgumentException DocumentNotFoundWithinSolutionByFilePath(this IExceptionGenerator _,
            Solution solution,
            string codeFilePath,
            string argumentName)
        {
            var message = Instances.ExceptionMessageGenerator.DocumentNotFoundWithinSolutionByFilePath(
                solution,
                codeFilePath);

            var output = new ArgumentException(message, argumentName);
            return output;
        }

        public static InvalidOperationException MethodWasNotAnExtensionMethod(this IExceptionGenerator _,
            IMethodSymbol methodSymbol)
        {
            var message = Instances.ExceptionMessageGenerator.MethodWasNotAnExtensionMethod(
                methodSymbol);

            var output = new InvalidOperationException(message);
            return output;
        }

        public static ArgumentException SolutionDidNotHaveProjectWithFilePath(this IExceptionGenerator _,
            Solution solution,
            string projectFilePath,
            string parameterName)
        {
            var message = Instances.ExceptionMessageGenerator.SolutionDidNotHaveProjectWithFilePath(
                solution,
                projectFilePath);

            var output = new ArgumentException(message, parameterName);
            return output;
        }

        public static ArgumentException SolutionDidNotHaveProjectWithName(this IExceptionGenerator _,
            Solution solution,
            string projectName,
            string argumentName)
        {
            var message = Instances.ExceptionMessageGenerator.SolutionDidNotHaveProjectWithName(
                solution,
                projectName);

            var output = new ArgumentException(message, argumentName);
            return output;
        }

        public static ArgumentException TypeNotFoundInCompilation(this IExceptionGenerator _,
            Compilation compilation,
            string namespacedTypeName,
            string argumentName)
        {
            var message = Instances.ExceptionMessageGenerator.TypeNotFoundInCompilation(
                compilation,
                namespacedTypeName);

            var output = new ArgumentException(message, argumentName);
            return output;
        }
    }
}