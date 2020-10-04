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
        public byte[] Compile(string sourceCode, MetadataReference[] assemblies)
        {
            using (var peStream = new MemoryStream())
            {
                var result = GenerateCode(sourceCode, assemblies).Emit(peStream);

                if (!result.Success)
                {
                    StringBuilder errorMessage = new StringBuilder();
                    var failures = result.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);
                    int lineNumber = failures.FirstOrDefault().Location.GetLineSpan().Span.Start.Line;
                    foreach (var diagnostic in failures)
                    {
                        errorMessage.Append($"{diagnostic.Id}: {diagnostic.GetMessage()}");
                    }
                    throw new DynamicCompilationException( errorMessage.ToString(), lineNumber);
                }
                
                peStream.Seek(0, SeekOrigin.Begin);

                return peStream.ToArray();
            }
        }

        private static CSharpCompilation GenerateCode(string sourceCode, MetadataReference[] references)
        {
            var codeString = SourceText.From(sourceCode);
            var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp8);

            var parsedSyntaxTree = SyntaxFactory.ParseSyntaxTree(codeString, options);

            return CSharpCompilation.Create("Hello.dll",
                new[] { parsedSyntaxTree }, 
                references: references, 
                options: new CSharpCompilationOptions(OutputKind.ConsoleApplication, 
                    optimizationLevel: OptimizationLevel.Release,
                    assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
        }
    }
}