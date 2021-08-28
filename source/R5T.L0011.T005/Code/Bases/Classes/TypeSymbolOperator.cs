using System;


namespace R5T.L0011.T005
{
    /// <summary>
    /// Empty implementation as base for extension methods.
    /// </summary>
    public class TypeSymbolOperator : ITypeSymbolOperator
    {
        #region Static

        public static TypeSymbolOperator Instance { get; } = new();

        #endregion
    }
}