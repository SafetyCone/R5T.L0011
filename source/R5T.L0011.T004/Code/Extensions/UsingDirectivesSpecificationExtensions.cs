using System;
using System.Linq;

using R5T.Magyar;

using R5T.L0011.T004;


namespace System
{
    public static class UsingDirectivesSpecificationExtensions
    {
        public static WasFound<NameAlias> HasNameAliasFor(this UsingDirectivesSpecification usingDirectivesSpecification,
            string name)
        {
            var nameAliasOrDefault = usingDirectivesSpecification.NameAliases
                .Where(xNameAlias => xNameAlias.DestinationName == name)
                .SingleOrDefault();

            var output = WasFound.From(nameAliasOrDefault);
            return output;
        }
    }
}
