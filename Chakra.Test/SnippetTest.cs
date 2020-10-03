using System;
using Xunit;

using static Chakra.Test.TestHelper;
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
            string snippet = @"
                for (int i = 1; i <= 3; i++){
                    Console.WriteLine($""i={i}"");
                }
            ";
                           
            string result = _executor.ExecuteSnippet(BreakLines(snippet));
            string expected = LinesOf("i=1", "i=2", "i=3");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldRunAsyncScripts()
        {
            string script = @"
                  Console.WriteLine(""before task"");
            var task = Task.Run(() =>
            {
                Console.WriteLine(""In the task"");
            });
            await task;
            Console.WriteLine(""after task"");
            ";

            string expected = LinesOf("before task", "In the task", "after task");
            string result = _executor.ExecuteSnippet(BreakLines(script));
            Assert.Equal(expected, result );
        }
        
    }
    
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