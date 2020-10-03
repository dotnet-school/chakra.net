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
    
    // TODO should support async
    // TODO should support lists, enumerable, arrays and dictionary
    // TODO should support tasks
    // TODO should support linq
    // TODO should support reading a file
    // Todo should support files, path
    // Todo should support creating classes
    // Todo should support regex
    // Todo should support classes, getter, setter
    // Todo should support namespace
    // Todo should support xunit
    // Todo should support moq
    // Todo should support expectations and reporting
    // Todo should report line numbers in errors
}
