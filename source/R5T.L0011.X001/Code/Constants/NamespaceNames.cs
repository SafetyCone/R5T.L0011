using System;


namespace R5T.L0011.X001
{
    public static class NamespaceNames
    {
        /// <summary>
        /// ALL namespaces are contained within the namespace with this name.
        /// As in, all of "System", "global::System", "R5T", extern alias DependencyX -> "DependencyX::System" are contained within "".
        /// </summary>
        public static string AllNamespacesNamespaceName => "";
        /// <summary>
        /// No namespaces are contained within the namespace with this name.
        /// No matter was namespace name you can dream up, the result of ContainsNamespace(parentNamespaceName, childNamespaceName) will be false of the parent has this namespace name.
        /// </summary>
        public static string NoNamespaceNamespaceName => null;
    }
}
