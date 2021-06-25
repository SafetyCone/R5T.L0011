using System;
using System.Collections.Generic;
using System.Linq;


namespace R5T.L0011.T004
{
    public class NamespaceNameSet : INamespaceNameSet
    {
        #region Static

        public static NamespaceNameSet New()
        {
            var output = new NamespaceNameSet();
            return output;
        }

        #endregion


        public HashSet<string> NamespaceNames { get; }

        string[] INamespaceNameSet.NamespaceNames => this.NamespaceNames.ToArray();


        public NamespaceNameSet()
        {
            this.NamespaceNames = new();
        }

        public bool AddValue(string namespaceName)
        {
            var output = this.NamespaceNames.Add(namespaceName);
            return output;
        }

        public bool Contains(string namespaceName)
        {
            var output = this.NamespaceNames.Contains(namespaceName);
            return output;
        }

        public bool RemoveValue(string namespaceName)
        {
            var output = this.NamespaceNames.Remove(namespaceName);
            return output;
        }
    }
}
