using System;


namespace R5T.L0011.T003
{
    /// <summary>
    /// Empty implementation as base for extension methods.
    /// </summary>
    public class SignatureModel : ISignatureModel
    {
        #region Static

        public static SignatureModel Instance { get; } = new();

        #endregion
    }
}
