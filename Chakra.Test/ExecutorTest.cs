using System;
using Xunit;

namespace Chakra.Test
{
    public class ExecutorTest
    {
        private Executor _executor = new Executor(new SnippetProgramGenerator());
        [Fact]
        public void ShouldPrintConsole()
        {
            string[] snippet =  {"Console.WriteLine(\"Hello World !\");"};
            string result = _executor.ExecuteSnippet(snippet);
            string expected = "Hello World !";
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void ShouldExecuteASnippet()
        {
            string[] snippet =
            {
                            "for (int i = 1; i <= 3; i++)", 
                            "{", 
                            "Console.WriteLine($\"i={i}\");", 
                            "}"
            }; 
                           
            string result = _executor.ExecuteSnippet(snippet);
            string expected = string.Join(Environment.NewLine, new[]{"i=1", "i=2", "i=3"});
            Assert.Equal(expected, result);
        }
    }
}
