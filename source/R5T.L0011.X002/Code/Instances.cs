using System;

using R5T.L0011.T001;
using R5T.L0011.T002;


namespace R5T.L0011.X002
{
    public static class Instances
    {
        public static ISyntax Syntax { get; } = T002.Syntax.Instance;
        public static ISyntaxFactory SyntaxFactory { get; } = T001.SyntaxFactory.Instance;
    }
}
