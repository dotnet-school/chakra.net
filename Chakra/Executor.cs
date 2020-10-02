using System;
using DynamicRun.Builder;

namespace Chakra
{
    public class Executor
    {
        private SnippetProgramGenerator _generator;

        public Executor(SnippetProgramGenerator snippetProgramGenerator)
        {
            _generator = snippetProgramGenerator;
        }

        public string ExecuteSnippet(string[] snippet)
        {
            return InternalExecuter.CompileAndRun("Snippet.cs", _generator.CreateProgramForSnippet(snippet), Array.Empty<string>());
        }
    }
}
