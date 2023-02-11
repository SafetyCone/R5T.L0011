using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.Magyar;

using R5T.L0011.X001;


namespace System
{
    public static class MethodSymbolExtensions
    {
        /// <summary>
        /// Test whether the method is an extension method, and if the type of the method's first parameter matches the input reciever type symbol. (It is a CS1100 compilation error to have an extension method where the first symbol does not have the 'this' modifier.)
        /// Note: there does not seem to be an explicit "is extension of" property on IMethodSymbol. The IMethodSymbol.ReceiverType should be it, but somehow is not. Also, IParameterSymbol.IsThis seems like it would work, but does not.
        /// </summary>
        public static bool IsExtensionOf(this IMethodSymbol methodSymbol,
            ITypeSymbol receiverTypeSymbol)
        {
            var output =
                methodSymbol.IsExtensionMethod
                && SymbolEqualityComparer.Default.Equals(methodSymbol.Parameters.First().Type, receiverTypeSymbol) // Somehow IMethodSymbol.ReceiverType is not the actual extension method type.
                ;

            return output;
        }

        public static IEnumerable<IParameterSymbol> ParametersExceptExtensionMethodReceiverFirstParameter(this IMethodSymbol methodSymbol)
        {
            // Check that the method is an extention method (since this method should only be used on extension methods).
            if (!methodSymbol.IsExtensionMethod)
            {
                throw Instances.ExceptionGenerator.MethodWasNotAnExtensionMethod(methodSymbol);
            }

            var output = methodSymbol.Parameters.SkipFirst(); // The first parameter will the extension method receiver type parameter.
            return output;
        }
    }
}


namespace R5T.L0011.X001.Extension
{
    public static class MethodSymbolExtensions
    {
        // Note: preserved here in an obscure namespace as an example of an initial attempt that has issues.
        public static bool IsExtensionOf_Unreliable(this IMethodSymbol methodSymbol,
            ITypeSymbol receiverTypeSymbol)
        {
            var output =
                methodSymbol.IsExtensionMethod
                // This test does not say specifically that the method *is* an extension on the receiver type, 
                // Not to mention that test of null return is bad.
                && methodSymbol.ReduceExtensionMethod(receiverTypeSymbol) is object
                ;

            return output;
        }
    }
}