using System;


namespace R5T.L0011.T001
{
    /// <summary>
    /// Empty implementation as base for extension methods.
    /// </summary>
    public class SyntaxOperator : ISyntaxOperator
    {
        #region Static
        
        public static SyntaxOperator Instance { get; } = new();

        #endregion
    }
}