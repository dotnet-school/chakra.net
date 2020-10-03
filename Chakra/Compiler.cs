using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Chakra
{
    internal class Compiler
    {
        public byte[] Compile(string sourceCode)
        {
            using (var peStream = new MemoryStream())
            {
                var result = GenerateCode(sourceCode).Emit(peStream);

                if (!result.Success)
                {
                    StringBuilder errorMessage = new StringBuilder();
                    var failures = result.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);
                    int lineNumber = failures.FirstOrDefault().Location.GetLineSpan().Span.Start.Line;
                    foreach (var diagnostic in failures)
                    {
                        errorMessage.Append($"Line:{diagnostic.Location.GetLineSpan().Span.Start.Line}-");
                        errorMessage.Append($"{diagnostic.Id}, {diagnostic.GetMessage()}");
                    }
                    Console.WriteLine(errorMessage.ToString());

                    throw new DynamicCompilationException( errorMessage.ToString(), lineNumber);
                }
                
                peStream.Seek(0, SeekOrigin.Begin);

                return peStream.ToArray();
            }
        }

        private static CSharpCompilation GenerateCode(string sourceCode)
        {
            var codeString = SourceText.From(sourceCode);
            var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp8);

            var parsedSyntaxTree = SyntaxFactory.ParseSyntaxTree(codeString, options);

            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Text.RegularExpressions.Regex).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).Assembly.Location),
                // To load current dll in the compilation context
                MetadataReference.CreateFromFile(typeof(SocketClient).Assembly.Location),
                // For socket connections
                MetadataReference.CreateFromFile(typeof(System.Net.IPAddress).Assembly.Location),
            };

            return CSharpCompilation.Create("Hello.dll",
                new[] { parsedSyntaxTree }, 
                references: references, 
                options: new CSharpCompilationOptions(OutputKind.ConsoleApplication, 
                    optimizationLevel: OptimizationLevel.Release,
                    assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
        }
    }
}