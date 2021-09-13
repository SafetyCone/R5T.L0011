using System;

using R5T.Magyar;

using R5T.L0011.T003;

using Instances = R5T.L0011.X002.Instances;


namespace System
{
    public static class ISignatureModelExtensions
    {
        public static string GetSeparatedToken(this ISignatureModel _,
            string token)
        {
            var output = $"{token}{Strings.Space}";
            return output;
        }

        public static string GetAccessibilityLevelTokenOnly(this ISignatureModel _,
            AccessibilityLevel accessibilityLevel)
        {
            return accessibilityLevel switch
            {
                AccessibilityLevel.Internal => Instances.Syntax.Internal(),
                AccessibilityLevel.Private => Instances.Syntax.Private(),
                AccessibilityLevel.Protected => Instances.Syntax.Protected(),
                AccessibilityLevel.Public => Instances.Syntax.Public(),
                AccessibilityLevel.Unspecified => Instances.Syntax.None(),
                _ => throw EnumerationHelper.SwitchDefaultCaseException(accessibilityLevel),
            };
        }

        public static string GetAccessibilityLevelToken(this ISignatureModel _,
            AccessibilityLevel accessibilityLevel)
        {
            var token = _.GetAccessibilityLevelTokenOnly(accessibilityLevel);

            var output = _.GetSeparatedToken(token);
            return output;
        }

        public static string GetStaticTokenOnly(this ISignatureModel _,
            bool @static)
        {
            var output = @static
                ? Instances.Syntax.Static()
                : Instances.Syntax.None()
                ;

            return output;
        }

        public static string GetStaticToken(this ISignatureModel _,
            bool @static)
        {
            var token = _.GetStaticTokenOnly(@static);

            var output = _.GetSeparatedToken(token);
            return output;
        }

        public static string GetPartialTokenOnly(this ISignatureModel _,
            bool partial)
        {
            var output = partial
                ? Instances.Syntax.Partial()
                : Instances.Syntax.None()
                ;

            return output;
        }

        public static string GetPartialToken(this ISignatureModel _,
            bool partial)
        {
            var token = _.GetPartialTokenOnly(partial);

            var output = _.GetSeparatedToken(token);
            return output;
        }

        public static string GetAsyncTokenOnly(this ISignatureModel _,
            bool async)
        {
            var output = async
                ? Instances.Syntax.Async()
                : Instances.Syntax.None()
                ;

            return output;
        }

        public static string GetAsyncToken(this ISignatureModel _,
            bool async)
        {
            var token = _.GetAsyncTokenOnly(async);

            var output = _.GetSeparatedToken(token);
            return output;
        }

        public static string GetOverrideTokenOnly(this ISignatureModel _,
            bool @override)
        {
            var output = @override
                ? Instances.Syntax.Override()
                : Instances.Syntax.None()
                ;

            return output;
        }

        public static string GetOverrideToken(this ISignatureModel _,
            bool @override)
        {
            var token = _.GetOverrideTokenOnly(@override);

            var output = _.GetSeparatedToken(token);
            return output;
        }

        public static string GetSignature(this ISignatureModel _,
            ClassSignatureModel classSignatureModel)
        {
            var accessibility = _.GetAccessibilityLevelToken(classSignatureModel.AccessibilityLevel);
            var @static = _.GetStaticToken(classSignatureModel.Static);

            var partial = _.GetPartialToken(classSignatureModel.Partial);

            var output = $"{accessibility}{@static}{partial}";
            return output;
        }

        public static string GetSignature(this ISignatureModel _,
            MethodSignatureModel methodSignatureModel)
        {
            var accessibility = _.GetAccessibilityLevelToken(methodSignatureModel.AccessibilityLevel);
            var @static = _.GetStaticToken(methodSignatureModel.Static);

            var partial = _.GetPartialToken(methodSignatureModel.Partial);

            var async = _.GetAsyncToken(methodSignatureModel.Async);
            var @override = _.GetOverrideToken(methodSignatureModel.Override);

            var output = $"{accessibility}{@static}{@override}{partial}{async}";
            return output;
        }
    }
}
