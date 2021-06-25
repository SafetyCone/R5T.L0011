using System;
using System.Collections.Generic;


namespace R5T.L0011.T004
{
    public class UsingDirectivesBlock : IUsingDirectivesBlock
    {
        #region Static

        public static UsingDirectivesBlock New(string label, params string[] namespaceNames)
        {
            var output = new UsingDirectivesBlock(label, namespaceNames);
            return output;
        }

        #endregion


        public string Label { get; set; }
        public List<string> NamespaceNames { get; }

        string[] IUsingDirectivesBlock.NamespaceNames => this.NamespaceNames.ToArray();


        public UsingDirectivesBlock()
        {
            this.NamespaceNames = new();
        }

        public UsingDirectivesBlock(string label, string[] namespaceNames)
            : this()
        {
            this.Label = label;

            this.NamespaceNames.AddRange(namespaceNames);
        }
    }
}
