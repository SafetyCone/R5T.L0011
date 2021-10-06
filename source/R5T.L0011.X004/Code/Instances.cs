using System;

using R5T.T0034;
using R5T.T0036;
using R5T.T0045;


namespace R5T.L0011.X004
{
    public static class Instances
    {
        public static ICodeFileOperator CodeFileOperator { get; } = T0045.CodeFileOperator.Instance;
        public static IClassOperator ClassOperator { get; } = T0045.ClassOperator.Instance;
        public static IMethodOperator MethodOperator { get; } = T0045.MethodOperator.Instance;
        public static IMethodNameOperator MethodNameOperator { get; } = T0036.MethodNameOperator.Instance;
        public static INamespacedTypeName NamespacedTypeName { get; } = T0034.NamespacedTypeName.Instance;
    }
}
