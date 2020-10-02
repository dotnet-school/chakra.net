using System;
using Xunit;

namespace Chakra.Test
{
    public class ExecutorTest
    {
        private Executor _executor = new Executor();
        [Fact]
        public void ShouldExecute()
        {
            string snippet = @"
                Console.WriteLine(""Hello World !"");
            ";
            string result = _executor.ExecuteSnippet(snippet);
            string expected = "Hello World !";
            Assert.Equal(expected, result);
        }
    }
}
