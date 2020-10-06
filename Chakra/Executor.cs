using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace Chakra
{
    public static class Executor
    {
        private  static VirtualStdOut? _stdOut;
        private  static TextWriter? _defaultStdOut;
        
        private static readonly SnippetProgramGenerator Generator = new SnippetProgramGenerator();
        private static  MetadataReference[] _assemblies = ExecutorOptions.GetDefaultAssemblies();
        private static readonly Object Monitor = new Object();
        
        
        public static void AddAssemblies(MetadataReference[] assemblies)
        {
            _assemblies = ExecutorOptions.GetDefaultAssemblies().Union(assemblies).ToArray();
        }

        public static void ResetAssemblies()
        {
            _assemblies = ExecutorOptions.GetDefaultAssemblies();
        }
        public static string ExecuteSnippet(string[] snippet)
        {
            return ExecuteSnippet(snippet, ExecutorOptions.GetDefaultImports());
        }

        public static string Execute(string sourceCode)
        {
            lock (Monitor)
            {
                try
                {
                    CaptureConsole();

                    var compiler = new Compiler();
                    var runner = new Runner();
                    byte[] compiled = compiler.Compile(sourceCode, _assemblies);
                    runner.Execute(compiled, Array.Empty<string>());
                    return GetConsoleOutput();
                }
                catch (Xunit.Sdk.AssertActualExpectedException e)
                {
                    throw new ExpectationException(e.Expected, e.Actual);
                }
                finally
                {
                    ResetConsole();
                }
            }
 
        }
        public static string ExecuteSnippet(string[] snippet, string[] imports)
        {
            var usingNamespaces = ExecutorOptions.GetDefaultImports().Union(imports).ToArray();
            var sourceCode = Generator
                            .CreateProgramForSnippet(snippet, usingNamespaces);
            try
            { 
                return Execute(sourceCode);
            } catch (DynamicCompilationException e) { 
                throw new DynamicCompilationException(e, Generator.SnippetLineStart + imports.Length - 2); 
            }
        }

        public static void CaptureConsole()
        {
            _defaultStdOut = Console.Out;
            _stdOut = new VirtualStdOut();
            Console.SetOut(_stdOut);
        }
        
        public static string GetConsoleOutput()
        {
            if (_stdOut == null)
            {
                throw new CodeTemplateException("Make sure to call CaptureConsole() in start of code");
            }
            Console.Out.Flush();
            string? consoleOutput = Regex.Replace(_stdOut.Captured.ToString() ?? string.Empty, "\n$", "");
            return consoleOutput;
        }

        private static void ResetConsole()
        {
            Console.SetOut(_defaultStdOut);
        }
    }
}
