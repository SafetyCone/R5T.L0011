using System;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class SemanticModelExtensions
    {
        public static SemanticModel VerifyNoCompilationErrors(this SemanticModel semanticModel)
        {
            semanticModel.Compilation.VerifyNoCompilationErrors();

            return semanticModel;
        }
    }
}
