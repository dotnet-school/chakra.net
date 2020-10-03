using System;
using System.Text.RegularExpressions;

namespace Chakra
{
    public class Executor
    {
        private SnippetProgramGenerator _generator;
        private static VirtualStdOut _stdOut;

        public Executor(SnippetProgramGenerator snippetProgramGenerator)
        {
            _generator = snippetProgramGenerator;
        }

        public string ExecuteSnippet(string[] snippet)
        {
            try
            {
                return InternalExecuter.CompileAndRun( _generator.CreateProgramForSnippet(snippet),
                                Array.Empty<string>());
            }
            catch (DynamicCompilationException e)
            {
                throw new DynamicCompilationException(e, _generator.SnippetLineStart);
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
