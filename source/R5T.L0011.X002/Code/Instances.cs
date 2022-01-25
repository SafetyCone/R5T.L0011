using System;

using R5T.L0011.T001;
using R5T.L0011.T002;
using R5T.T0034;
using R5T.T0035;


namespace R5T.L0011.X002
{
    public static class Instances
    {
        public static IAttributeTypeName AttributeTypeName { get; } = T0034.AttributeTypeName.Instance;
        public static INamespaceName NamespaceName { get; } = T0035.NamespaceName.Instance;
        public static ISyntax Syntax { get; } = T002.Syntax.Instance;
        public static ISyntaxFactory SyntaxFactory { get; } = T001.SyntaxFactory.Instance;
    }
}
