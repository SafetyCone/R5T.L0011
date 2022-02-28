using System;


namespace R5T.L0011.Construction
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.CreateMethodFromText();
            //Program.TypeParameterListedMethodName();
        }

#pragma warning disable IDE0051 // Remove unused private members

        private static void CreateMethodFromText()
        {
            var extensionMethodBaseFunctionalityName = "IAnsiColorCode_BlackBackground_R5T_T0089_X001";
            var iExtensionMethodBaseFunctionalityTypeName = "IExtensionMethodBaseFunctionality";
            var extensionMethodBaseFunctionalityIdentity = "d3c731b7-5836-4eb5-b62c-5807e96ed86f";

            var text = $@"
public static string {extensionMethodBaseFunctionalityName}(this {iExtensionMethodBaseFunctionalityTypeName} _)
{{
    return ""{extensionMethodBaseFunctionalityIdentity}"";
}}
";
            //// Requires trim of new lines at beginning and end of above.
            //var method = Instances.SyntaxFactory.ParseMethodDeclaration(text);

            //// Requires indentation by block.
            //var method = Instances.SyntaxFactory.ParseMethodDeclaration(
            //    text.Trim());

            var method = Instances.SyntaxFactory.ParseMethodDeclaration(
                text.Trim())
                .IndentBlock(
                    Instances.Indentation.Method(),
                    false);

            // Check that the method parsing worked.
            method.VerifyNonNull("Failed to parse method declaration.");

            var outputFilePath = @"C:\Temp\Temp.cs";

            method.WriteToSynchronous(outputFilePath);
        }

        //private static void Test<T, T2>(string value)
        //    where T : IComparable
        //{

        //}

        private static void TypeParameterListedMethodName()
        {
            var methodDeclarationText =
@"
private static void Test<T>(string value)
    where T : IComparable
";
            var methodDeclaration = Instances.MethodGenerator.GetMethodDeclarationFromText(methodDeclarationText);

            var fullMethodName = Instances.MethodNameOperator.GetFullName(methodDeclaration);
            var typeParameterListedMethodName = Instances.MethodNameOperator.GetTypeParameterListedMethodName(methodDeclaration);

            Console.WriteLine(fullMethodName);
            Console.WriteLine();
            Console.WriteLine(typeParameterListedMethodName);
        }

#pragma warning restore IDE0051 // Remove unused private members
    }
}
