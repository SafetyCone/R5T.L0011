using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static class ClassDeclarationSyntaxExtensions
    {
        /// <inheritdoc cref="GetExtensionMethods(ClassDeclarationSyntax)"/>
        public static IEnumerable<MethodDeclarationSyntax> GetExtensionMethods_Enumerable(this ClassDeclarationSyntax @class)
        {
            var output = @class.GetMethods()
                .Where(xMethod => xMethod.IsExtensionMethod())
                ;

            return output;
        }

        /// <summary>
        /// Gets extension methods in the class.
        /// </summary>
        public static MethodDeclarationSyntax[] GetExtensionMethods(this ClassDeclarationSyntax @class)
        {
            var output = @class.GetExtensionMethods_Enumerable().ToArray();
            return output;
        }
    }
}
