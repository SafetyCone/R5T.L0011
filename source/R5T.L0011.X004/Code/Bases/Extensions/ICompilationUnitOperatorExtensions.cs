using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;

using Instances = R5T.L0011.X004.Instances;


namespace System
{
    public static class ICompilationUnitOperatorExtensions
    {
        public static Task<CompilationUnitSyntax> LoadCompilationUnit(this ICompilationUnitOperator _,
            string filePath)
        {
            return Instances.CodeFileOperator.LoadCompilationUnit(filePath);
        }

        /// <summary>
        /// Quality-of-life overload for <see cref="LoadCompilationUnit(ICodeFile, string)"/>.
        /// </summary>
        public static Task<CompilationUnitSyntax> Load(this ICompilationUnitOperator _,
            string filePath)
        {
            return _.LoadCompilationUnit(filePath);
        }
    }
}
