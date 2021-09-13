using System;


namespace R5T.L0011.T003
{
    public class MethodSignatureModel : SignatureModelBase
    {
        public bool Async { get; set; }
        public bool Override { get; set; }
        public bool Partial { get; set; }
    }
}
