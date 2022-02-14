using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using Documentation = R5T.L0011.X000.Documentation;


namespace System
{
    public static class UsingDirectiveSyntaxExtensions
    {
        public static NameAlias GetNameAlias(this UsingDirectiveSyntax usingDirective)
        {
            var destinationName = usingDirective.GetDestinationName();
            var sourceNameExpression = usingDirective.GetSourceNameExpression();

            var output = new NameAlias(
                destinationName,
                sourceNameExpression);

            return output;
        }

        public static (string destinationName, string sourceNameExpression) GetNameAliasValues(this UsingDirectiveSyntax usingDirective)
        {
            var destinationName = usingDirective.GetDestinationName();
            var sourceNameExpression = usingDirective.GetSourceNameExpression();

            return (destinationName, sourceNameExpression);
        }

        public static string GetSourceNameExpression(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.GetQualifiedName();
            return output;
        }

        public static string GetQualifiedName(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.Name.ToString();
            return output;
        }

        public static string GetNamespaceName(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.GetQualifiedName();
            return output;
        }

        public static string GetNameEqualsValue(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.Alias
                .GetChild<IdentifierNameSyntax>()
                .Identifier.ToString();

            return output;
        }

        public static string GetDestinationName(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.GetNameEqualsValue();
            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNamespaceDirectiveSyntaxes(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsUsingNamespaceDirective())
                ;

            return output;
        }

        public static Dictionary<string, UsingDirectiveSyntax> GetUsingNamespaceDirectiveSyntaxesByNamespaceName_First(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .GetUsingNamespaceDirectiveSyntaxes()
                .GroupBy(x => x.GetNamespaceName())
                .ToDictionary(
                    xGroup => xGroup.Key,
                    xGroup => xGroup.First());

            return output;
        }

        public static Dictionary<string, UsingDirectiveSyntax> GetUsingNamespaceDirectiveSyntaxesByNamespaceName_Single(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .GetUsingNamespaceDirectiveSyntaxes()
                .GroupBy(x => x.GetNamespaceName())
                .ToDictionary(
                    xGroup => xGroup.Key,
                    xGroup => xGroup.Single());

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetUsingNamespaceDirectiveSyntaxesByNamespaceName_Single(IEnumerable{UsingDirectiveSyntax})"/> as the default.
        /// </summary>
        public static Dictionary<string, UsingDirectiveSyntax> GetUsingNamespaceDirectiveSyntaxesByNamespaceName(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives.GetUsingNamespaceDirectiveSyntaxesByNamespaceName_Single();
            return output;
        }

        public static IEnumerable<UsingNamespaceDirectiveSyntax> GetUsingNamespaceDirectives(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .GetUsingNamespaceDirectiveSyntaxes()
                .Select(x => UsingNamespaceDirectiveSyntax.From(x))
                ;

            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNameAliasDirectiveSyntaxes(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsUsingNameAliasDirective())
                ;

            return output;
        }

        /// <summary>
        /// Blocks are separated by blank lines (which are two new lines separated only by whitespace).
        /// </summary>
        public static UsingDirectiveSyntax[][] GetUsingBlocks(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            if (usingDirectives.None())
            {
                return Array.Empty<UsingDirectiveSyntax[]>();
            }

            var blocks = new List<UsingDirectiveSyntax[]>();

            // Handle the first element.
            var block = new List<UsingDirectiveSyntax>();

            var firstUsing = usingDirectives.First();

            block.Add(firstUsing);

            foreach (var usingDirective in usingDirectives.SkipFirst())
            {
                var isBeginningOfNewBlock = usingDirective.IsBeginningOfNewBlock();
                if (isBeginningOfNewBlock)
                {
                    blocks.Add(block.ToArray());

                    block = new List<UsingDirectiveSyntax>();
                }

                block.Add(usingDirective);
            }

            // Cleanup.
            blocks.Add(block.ToArray());

            var output = blocks.ToArray();
            return output;
        }

        public static IEnumerable<UsingNameAliasDirectiveSyntax> GetUsingNameAliasDirectives(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .GetUsingNameAliasDirectiveSyntaxes()
                .Select(x => UsingNameAliasDirectiveSyntax.From(x))
                ;

            return output;
        }

        public static UsingDirectiveSyntax GetUsing(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            string namespaceName)
        {
            var wasFound = usingDirectives.HasUsing(namespaceName);
            if (!wasFound)
            {
                throw new Exception($"Using directive for namespace name '{namespaceName}' not found.");
            }

            return wasFound;
        }

        public static UsingDirectiveSyntax GetUsing(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            string destinationName,
            string sourceNameExpression)
        {
            var wasFound = usingDirectives.HasUsing(destinationName, sourceNameExpression);
            if (!wasFound)
            {
                throw new Exception($"Using directive for name alias not found. Looking for:\n'{destinationName} = {sourceNameExpression}'");
            }

            return wasFound;
        }

        public static IEnumerable<string> GetUsingNamespaceNames(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .GetUsingNamespaceDirectiveSyntaxes()
                .Select(x => x.GetNamespaceName())
                ;

            return output;
        }

        public static IEnumerable<NameAlias> GetUsingNameAliases(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .GetUsingNameAliasDirectiveSyntaxes()
                .Select(x => x.GetNameAlias())
                ;

            return output;
        }

        public static Dictionary<string, bool> HasUsingNamespaceNames(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            IEnumerable<string> namespaceNames)
        {
            var uniqueNamespaceNames = namespaceNames.Unique();

            var currentUniqueNamespaceNames = usingDirectives.GetUsingNamespaceNames().Unique();

            var output = uniqueNamespaceNames
                .ToDictionary(
                    xNamespaceName => xNamespaceName,
                    xNamespaceName => currentUniqueNamespaceNames.Contains(xNamespaceName));

            return output;
        }

        public static IEnumerable<string> GetMissingNamespaceNames(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            IEnumerable<string> namespaceNames)
        {
            var currentNamespaceNames = usingDirectives.GetUsingNamespaceNames();

            var output = namespaceNames.Except(currentNamespaceNames);
            return output;
        }

        public static IEnumerable<NameAlias> GetMissingNameAliases(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            IEnumerable<NameAlias> nameAliases)
        {
            var currentNameAliases = usingDirectives.GetUsingNameAliases();

            var output = nameAliases.Except(currentNameAliases);
            return output;
        }

        public static IEnumerable<string> GetDistinctUsingNamespaceNames(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .GetUsingNamespaceNames()
                .Distinct()
                ;

            return output;
        }

        public static bool HasNameEqualsChild(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.HasChildOfType<NameEqualsSyntax>();
            return output;
        }

        public static WasFound<UsingDirectiveSyntax> HasUsing_Single(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            string namespaceName)
        {
            var usingDirectiveOrDefault = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsUsingNamespaceDirective(namespaceName))
                .SingleOrDefault();

            var output = WasFound.From(usingDirectiveOrDefault);

            return output;
        }

        public static WasFound<UsingDirectiveSyntax> HasUsing_First(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            string namespaceName)
        {
            var usingDirectiveOrDefault = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsUsingNamespaceDirective(namespaceName))
                .FirstOrDefault();

            var output = WasFound.From(usingDirectiveOrDefault);

            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasUsing_First(IEnumerable{UsingDirectiveSyntax}, string)"/> as the default, since <inheritdoc cref="Documentation.AllWeCareAboutIsUsingExistsNotUnique"/>.
        /// </summary>
        public static WasFound<UsingDirectiveSyntax> HasUsing(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            string namespaceName)
        {
            var output = usingDirectives.HasUsing_First(namespaceName);
            return output;
        }

        //public static Dictionary<string, WasFound<UsingDirectiveSyntax>> HasUsings(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
        //    IEnumerable<string> namespaceNames)
        //{
        //    var uniqueNamespaceNames = namespaceNames.Unique();

        //    var usingDirectiveOrDefault = usingDirectives
        //        .Where(xUsingDirective => xUsingDirective.IsUsingNamespaceDirective()
        //            && uniqueNamespaceNames.Contains(xUsingDirective.GetNamespaceName()))
        //        // Use robust single logic.
        //        .SingleOrDefault();

        //    var output = WasFound.From(usingDirectiveOrDefault);

        //    return output;
        //}

        public static WasFound<UsingDirectiveSyntax> HasUsingForDestinationName_First(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            string destinationName)
        {
            var usingDirectiveOrDefault = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsUsingNameAliasDirective(
                    destinationName))
                .FirstOrDefault();

            var output = WasFound.From(usingDirectiveOrDefault);

            return output;
        }

        public static WasFound<UsingDirectiveSyntax> HasUsingForDestinationName_Single(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            string destinationName)
        {
            var usingDirectiveOrDefault = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsUsingNameAliasDirective(
                    destinationName))
                .SingleOrDefault();

            var output = WasFound.From(usingDirectiveOrDefault);

            return output;
        }

        /// <summary>
        /// Selects <see cref="HasUsingForDestinationName_First(IEnumerable{UsingDirectiveSyntax}, string)"/> as the default, since <see cref="Documentation.AllWeCareAboutIsUsingExistsNotUnique"/>.
        /// </summary>
        public static WasFound<UsingDirectiveSyntax> HasUsingForDestinationName(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            string destinationName)
        {
            var output = usingDirectives.HasUsingForDestinationName_First(destinationName);
            return output;
        }

        public static WasFound<UsingDirectiveSyntax> HasUsing_First(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            string destinationName,
            string sourceNameExpression)
        {
            var usingDirectiveOrDefault = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsUsingNameAliasDirective(
                    destinationName,
                    sourceNameExpression))
                .FirstOrDefault();

            var output = WasFound.From(usingDirectiveOrDefault);

            return output;
        }

        public static WasFound<UsingDirectiveSyntax> HasUsing_Single(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            string destinationName,
            string sourceNameExpression)
        {
            var usingDirectiveOrDefault = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsUsingNameAliasDirective(
                    destinationName,
                    sourceNameExpression))
                .SingleOrDefault();

            var output = WasFound.From(usingDirectiveOrDefault);

            return output;
        }

        /// <summary>
        /// Selects <see cref="HasUsing_First(IEnumerable{UsingDirectiveSyntax}, string, string)"/> as the default, since <inheritdoc cref="Documentation.AllWeCareAboutIsUsingExistsNotUnique"/>.
        /// </summary>
        public static WasFound<UsingDirectiveSyntax> HasUsing(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            string destinationName,
            string sourceNameExpression)
        {
            var output = usingDirectives.HasUsing_First(destinationName, sourceNameExpression);
            return output;
        }

        public static bool IsNamespaceName(this UsingDirectiveSyntax usingDirective,
            string namespaceName)
        {
            var currentNamespaceName = usingDirective.GetNamespaceName();

            var output = currentNamespaceName == namespaceName;
            return output;
        }

        /// <summary>
        /// A using directive is the beginning of a new block if it is preceeded by a blank line (i.e. its separating leading trivia contains a blank line).
        /// </summary>
        public static bool IsBeginningOfNewBlock(this UsingDirectiveSyntax usingDirective)
        {
            var leadingSeparatingTrivia = usingDirective.GetSeparatingLeadingTrivia();

            var output = leadingSeparatingTrivia.ContainsBlankLine();
            return output;
        }

        public static bool IsNameAlias(this UsingDirectiveSyntax usingDirective,
            string destinationName)
        {
            var currentDestinationName = usingDirective.GetDestinationName();

            var output = destinationName == currentDestinationName;

            return output;
        }

        public static bool IsNameAlias(this UsingDirectiveSyntax usingDirective,
            string destinationName,
            string sourceNameExpression)
        {
            var (currentDestinationName, currentSourceNameExpression) = usingDirective.GetNameAliasValues();

            var output = destinationName == currentDestinationName
                && sourceNameExpression == currentSourceNameExpression;

            return output;
        }

        public static bool IsUsingNameAliasDirective(this UsingDirectiveSyntax usingDirective)
        {
            // If the node has a NameEquals child node, then it is a name alias directive. Else, it is a using namespace directive.
            var output = usingDirective.HasNameEqualsChild();
            return output;
        }

        public static bool IsUsingNameAliasDirective(this UsingDirectiveSyntax usingDirective,
            string destinationName)
        {
            var output = usingDirective.IsUsingNameAliasDirective()
                && usingDirective.IsNameAlias(destinationName);

            return output;
        }

        public static bool IsUsingNameAliasDirective(this UsingDirectiveSyntax usingDirective,
            string destinationName,
            string sourceNameExpression)
        {
            var output = usingDirective.IsUsingNameAliasDirective()
                && usingDirective.IsNameAlias(destinationName, sourceNameExpression);

            return output;
        }

        public static bool IsUsingNamespaceDirective(this UsingDirectiveSyntax usingDirective)
        {
            // If the node has a NameEquals child node, then it is a name alias directive. Else, it is a using namespace directive.
            var output = !usingDirective.HasNameEqualsChild();
            return output;
        }

        public static bool IsUsingNamespaceDirective(this UsingDirectiveSyntax usingDirective,
            string namespaceName)
        {
            var output = usingDirective.IsUsingNamespaceDirective() && usingDirective.IsNamespaceName(namespaceName);
            return output;
        }
    }
}
