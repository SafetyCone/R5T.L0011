using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class CompilationExtensions
    {
        public static Compilation VerifyNoCompilationErrors(this Compilation compilation)
        {
            var diagnostics = compilation.GetDiagnostics();

            var anyErrors = diagnostics
                .Where(x => x.Severity == DiagnosticSeverity.Error)
                .Any();

            if (anyErrors)
            {
                throw new Exception("There were errors in the compilation.");
            }

            return compilation;
        }

        public static async Task<Compilation> VerifyNoCompilationErrors(this Task<Compilation> gettingCompilation)
        {
            var compilation = await gettingCompilation;

            compilation.VerifyNoCompilationErrors();

            return compilation;
        }
    }
}
