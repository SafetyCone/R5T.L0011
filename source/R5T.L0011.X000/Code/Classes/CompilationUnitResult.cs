using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Microsoft.CodeAnalysis.CSharp
{
    public class CompilationUnitResult<T>
    {
        public CompilationUnitSyntax CompilationUnit { get; }
        public T Result { get; }


        public CompilationUnitResult(
            CompilationUnitSyntax compilationUnit,
            T result)
        {
            this.CompilationUnit = compilationUnit;
            this.Result = result;
        }
    }


    public static class CompilationUnitResult
    {
        public static CompilationUnitResult<T> From<T>(
            CompilationUnitSyntax compilationUnit,
            T result)
        {
            var output = new CompilationUnitResult<T>(
                compilationUnit,
                result);

            return output;
        }
    }
}
