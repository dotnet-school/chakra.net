using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace Chakra
{
    public class Executor
    {
        private VirtualStdOut? _stdOut;
        private  TextWriter _defaultStdOut;
        
        private readonly SnippetProgramGenerator _generator;
        private readonly MetadataReference[] _assemblies;
        private readonly Object _monitor = new Object();

        public Executor(SnippetProgramGenerator snippetProgramGenerator)
        {
            _generator = snippetProgramGenerator;
            _assemblies = ExecutorOptions.GetDefaultAssemblies();
        }

        public Executor(SnippetProgramGenerator snippetProgramGenerator, MetadataReference[] assemblies)
        {
            _generator = snippetProgramGenerator;
            _assemblies = ExecutorOptions.GetDefaultAssemblies().Union(assemblies).ToArray();
        }

        public string ExecuteSnippet(string[] snippet)
        {
            return ExecuteSnippet(snippet, ExecutorOptions.GetDefaultImports());
        }

        public string ExecuteSnippet(string[] snippet, string[] imports)
        {
            lock (_monitor)
            {
                try
                {
                    CaptureConsole();
                    var sourceCode = _generator
                                    .CreateProgramForSnippet(snippet, 
                                        ExecutorOptions.GetDefaultImports().Union(imports).ToArray());
                    var compiler = new Compiler();
                    var runner = new Runner();
                    byte[] compiled = compiler.Compile(sourceCode, _assemblies);
      
                    runner.Execute(compiled, Array.Empty<string>());
                    return GetConsoleOutput();
                }
                catch (DynamicCompilationException e)
                {
                    throw new DynamicCompilationException(e, _generator.SnippetLineStart + imports.Length - 1);
                }
            }
        }

        public void CaptureConsole()
        {
            _defaultStdOut = Console.Out;
            _stdOut = new VirtualStdOut();
            Console.SetOut(_stdOut);
        }
        
        public string GetConsoleOutput()
        {
            if (_stdOut == null)
            {
                throw new CodeTemplateException("Make sure to call CaptureConsole() in start of code");
            }
            Console.Out.Flush();
            string? consoleOutput = Regex.Replace(_stdOut.Captured.ToString() ?? string.Empty, "\n$", "");
            Console.SetOut(_defaultStdOut);
            return consoleOutput;
        }
    }
}
