using System;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace Chakra
{
    public class Executor
    {
        private static VirtualStdOut _stdOut;
        
        private readonly SnippetProgramGenerator _generator;
        private readonly MetadataReference[] _assemblies;

        public Executor(SnippetProgramGenerator snippetProgramGenerator)
        {
            _generator = snippetProgramGenerator;
            _assemblies = ExecutorOptions.GetDefaultAssemblies();
        }

        public Executor(SnippetProgramGenerator snippetProgramGenerator, MetadataReference[] assemblies)
        {
            _generator = snippetProgramGenerator;
            _assemblies = assemblies;
        }

        public string ExecuteSnippet(string[] snippet)
        {
            return ExecuteSnippet(snippet, ExecutorOptions.GetDefaultImports());
        }

        public string ExecuteSnippet(string[] snippet, string[] imports)
        {
            try
            {
                return InternalExecuter.CompileAndRun( 
                                _generator.CreateProgramForSnippet(snippet, imports),
                                Array.Empty<string>(),
                                _assemblies);
            }
            catch (DynamicCompilationException e)
            {
                throw new DynamicCompilationException(e, _generator.SnippetLineStart + imports.Length -  1);
            }
            
        }

        public static void CaptureConsole()
        {
            _stdOut = new VirtualStdOut();
            Console.SetOut(_stdOut);
        }
        
        public static void SendConsoleOutput(string [] args)
        {
            int callbackPort = int.Parse(args[0]);

            if (_stdOut == null)
            {
                throw new CodeTemplateException("Make sure to call CaptureConsole() in start of code");
            }
            Console.Out.Flush();
            string? consoleOutput = Regex.Replace(_stdOut.Captured.ToString() ?? string.Empty, "\n$", "");
            Messaging.SendMessage(consoleOutput, callbackPort);
        }
    }
}
