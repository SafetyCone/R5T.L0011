using System;
using System.Collections.Generic;


namespace R5T.L0011.T004
{
    public class UsingDirectivesBlockList : IUsingDirectivesBlockList
    {
        #region Static

        public static UsingDirectivesBlockList New()
        {
            var output = new UsingDirectivesBlockList();
            return output;
        }

        #endregion


        public List<IUsingDirectivesBlock> Blocks { get; }


        public UsingDirectivesBlockList()
        {
            this.Blocks = new List<IUsingDirectivesBlock>();
        }
    }
}
