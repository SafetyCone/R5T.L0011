using System;

using R5T.T0036;
using R5T.T0045;

using R5T.L0011.T001;


namespace R5T.L0011.Construction
{
    public static class Instances
    {
        public static IIndentation Indentation { get; } = T0036.Indentation.Instance;
        public static IMethodGenerator MethodGenerator { get; } = T0045.MethodGenerator.Instance;
        public static IMethodNameOperator MethodNameOperator { get; } = T0036.MethodNameOperator.Instance;
        public static ISyntaxFactory SyntaxFactory { get; } = L0011.T001.SyntaxFactory.Instance;
    }
}
