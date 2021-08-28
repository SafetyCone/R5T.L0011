using System;
using System.Linq;
using System.Xml.Linq;

using Microsoft.CodeAnalysis;

using R5T.Magyar;

using R5T.L0011.T005;

using Instances = R5T.L0011.T005.X002.Instances;


namespace System
{
    public static class ITypeSymbolOperatorExtensions
    {
        public static WasFound<string> HasXmlDocumentationComment(this ITypeSymbolOperator _,
            ITypeSymbol typeSymbol)
        {
            var documentationXml = typeSymbol.GetDocumentationCommentXml();

            var hasXmlDocumentationComment = StringHelper.IsNotNullOrEmpty(documentationXml);
            if(hasXmlDocumentationComment)
            {
                var comment = XElement.Parse(documentationXml);

                var summaryElement = comment.Element("summary");

                var summary = summaryElement.Value.Trim();

                var output = WasFound.From(summary);
                return output;
            }
            else
            {
                return WasFound.NotFound<string>();
            }
        }

        public static bool HasAcceptedDeclarationCodeFilePath(this ITypeSymbolOperator _,
            ITypeSymbol typeSymbol,
            Func<string, bool> declarationCodeFilePathPredicate)
        {
            foreach (var location in typeSymbol.Locations)
            {
                var linespan = location.GetLineSpan();

                // If the linespan path is null, then false. (TODO: investigate? Seems like services should always have a line span? Or what about Nuget packages?)
                if (linespan.Path is null)
                {
                    return false;
                }

                var declarationCodeFilePath = linespan.Path;

                var declarationCodeFilePathIsAccepted = declarationCodeFilePathPredicate(declarationCodeFilePath);
                if (declarationCodeFilePathIsAccepted)
                {
                    return true;
                }
            }

            // Else, false.
            return false;
        }

        public static bool HasAnyMembers(this ITypeSymbolOperator _,
            ITypeSymbol typeSymbol)
        {
            var output = typeSymbol.GetMembers().Any();
            return output;
        }

        public static bool HasNoMembers(this ITypeSymbolOperator _,
            ITypeSymbol typeSymbol)
        {
            var output = !_.HasAnyMembers(typeSymbol);
            return output;
        }

        public static bool HasDeclarationInDirectory(this ITypeSymbolOperator _,
           ITypeSymbol typeSymbol,
           string codeDirectoryPath)
        {
            var output = _.HasAcceptedDeclarationCodeFilePath(
                typeSymbol,
                declarationCodeFilePath => Instances.PathOperator.IsFileInDirectoryOrSubDirectories(declarationCodeFilePath, codeDirectoryPath));

            return output;
        }

        /// <summary>
        /// Determines if a type symbols is decorated with an attribute, or has a declaration in file in a specific code directory.
        /// This is useful for various type category identifiers (like service implementation identifier).
        /// </summary>
        public static bool HasAttributeWithTypeNameOrAcceptedDeclarationCodeFilePath(this ITypeSymbolOperator _,
            ITypeSymbol typeSymbol,
            string attributeTypeName,
            Func<string, bool> declarationCodeFilePathPredicate)
        {
            // Does the input type have the required attribute?
            var hasAttribute = typeSymbol.HasAttributeWithTypeName(attributeTypeName);
            if (hasAttribute)
            {
                // TODO: allow extra tests on the attribute (for example, if it specified certain values for certain properties).

                return true;
            }

            // Now see if any of the type symbol's declarations (there can be multiple for partial types) are in the specified directory.
            var hasAcceptedDeclarationCodeFilePath = _.HasAcceptedDeclarationCodeFilePath(typeSymbol, declarationCodeFilePathPredicate);
            return hasAcceptedDeclarationCodeFilePath;
        }

        /// <summary>
        /// Determines if a type symbols is decorated with an attribute, or has a declaration in file in a specific code directory.
        /// This is useful for various type category identifiers (like service implementation identifier).
        /// </summary>
        public static bool HasAttributeWithTypeNameOrDeclarationInCodeDirectory(this ITypeSymbolOperator _,
            ITypeSymbol typeSymbol,
            string attributeTypeName,
            string codeDirectoryPath)
        {
            var output = _.HasAttributeWithTypeNameOrAcceptedDeclarationCodeFilePath(
                typeSymbol,
                attributeTypeName,
                declarationCodeFilePath => Instances.PathOperator.IsFileInDirectoryOrSubDirectories(declarationCodeFilePath, codeDirectoryPath));

            return output;
        }

        public static bool IsInterface(this ITypeSymbolOperator _,
            ITypeSymbol typeSymbol)
        {
            var output = typeSymbol.TypeKind == TypeKind.Interface;
            return output;
        }
    }
}
