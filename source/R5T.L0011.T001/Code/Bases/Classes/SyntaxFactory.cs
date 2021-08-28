using System;



namespace R5T.L0011.T001
{
    /// <summary>
    /// Empty implementation as base for extension methods.
    /// </summary>
    public class SyntaxFactory : ISyntaxFactory
    {
        #region Static

        public static SyntaxFactory Instance { get; } = new();

        #endregion
    }
}
