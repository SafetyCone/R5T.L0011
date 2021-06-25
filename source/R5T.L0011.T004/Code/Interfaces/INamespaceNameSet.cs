using System;


namespace R5T.L0011.T004
{
    public interface INamespaceNameSet
    {
        string[] NamespaceNames { get; }

        bool AddValue(string namespaceName);
        bool Contains(string namespaceName);
        bool RemoveValue(string namespaceName);
    }
}
