using System;

using R5T.Magyar;


namespace R5T.L0011.X001
{
    public static class IExceptionMessageExtensions
    {
        public static string MethodParameterNotFound(this IExceptionMessage _,
            string methodName,
            string parameterName)
        {
            var output = $"Parameter '{parameterName}' on method '{methodName}' not found.";
            return output;
        }

        public static string ParameterNotFound(this IExceptionMessage _,
            string parameterName)
        {
            var output = $"Parameter '{parameterName}' not found.";
            return output;
        }

        public static string StatementNotFound(this IExceptionMessage _,
            string statementText)
        {
            var output = $"Statement not found. Statement:\n{statementText}";
            return output;
        }
    }
}
