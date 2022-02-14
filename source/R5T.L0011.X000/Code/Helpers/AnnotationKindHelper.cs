using System;


namespace R5T.L0011.X000
{
    public static class AnnotationKindHelper
    {
        /// <summary>
        /// Annotations kinds are strings. While an annotation can be created with a null kind, there is a limitation that annotations cannot be retrieved with a null kind.
        /// Thus we supply a default annotation kind.
        /// <default-value-is>The default value is a Guid: 834b0698-4629-4bbd-b5dc-c473e9dbdc5b.</default-value-is>
        /// A Guid was chosen (as opposed to any other string like "null" for example) to avoid collisions with kinds users might actually want to use.
        /// </summary>
        public static string Default => "834b0698-4629-4bbd-b5dc-c473e9dbdc5b";
    }
}
