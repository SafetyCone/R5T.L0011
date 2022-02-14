using System;

using Microsoft.CodeAnalysis;


namespace R5T.L0011.X000
{
    public static class SyntaxAnnotationHelper
    {
        public static SyntaxAnnotation New()
        {
            var output = new SyntaxAnnotation(AnnotationKindHelper.Default);
            return output;
        }

        public static SyntaxAnnotation NewWithoutDefaultAnnotationKind()
        {
            var output = new SyntaxAnnotation();
            return output;
        }
    }
}
