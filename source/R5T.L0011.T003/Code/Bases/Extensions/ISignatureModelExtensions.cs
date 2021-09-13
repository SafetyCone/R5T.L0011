using System;


namespace R5T.L0011.T003
{
    public static class ISignatureModelExtensions
    {
        public static ClassSignatureModel GetPrivateClassDefault(this ISignatureModel _)
        {
            var output = new ClassSignatureModel
            {
                AccessibilityLevel = AccessibilityLevel.Unspecified, // Classes are private by default.
            };

            return output;
        }

        public static MemberSignatureModel GetClassDefault(this ISignatureModel _)
        {
            var output = new MemberSignatureModel
            {
                AccessibilityLevel = AccessibilityLevel.Public,
            };

            return output;
        }

        public static MemberSignatureModel GetInterfaceDefault(this ISignatureModel _)
        {
            var output = new MemberSignatureModel
            {
                AccessibilityLevel = AccessibilityLevel.Public,
            };

            return output;
        }
    }
}
