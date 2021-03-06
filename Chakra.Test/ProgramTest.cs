using System;
using Xunit;

using static Chakra.Test.TestHelper;
namespace Chakra.Test
{
    public class ProgramTest
    {
        [Fact]
        public void ShouldRunAProgram()
        {
            string snippet =  @"
            using System;
            namespace Client
            {
                class Program
                {
                    static void Main(string[] args)
                    {
                      Console.WriteLine(""Hello"");
                        Console.WriteLine(""World !"");
                    }
                }
            }";

            string expected = "Hello World !";

            ExpectOutput(snippet, "Hello", "World !");
        }
     
        [Fact]
        public void ShouldReportCompileErrorInProgram()
        {
            string snippet =  @"
            using System;
            namespace Client
            {
                class Program
                {
                    static void Main(string[] args)
                    {
                        Console.WriteLine(""Hello"");
                        Console.WriteLine(""World !"");
                        Console.WriteLine(undefined);                        
                    }
                }
            }";

            string expectedMessage = "CS0103: The name 'undefined' does not exist in the current context";
            int expectedLine = 10; 
            DynamicCompilationException exception = Assert.Throws<DynamicCompilationException>(
                            () => Executor.Execute(snippet));

            Assert.Equal(expectedMessage, exception.Message);
            Assert.Equal(expectedLine, exception.LineNumber);
        }
        
        [Fact]
        public void ShouldThrowActualExceptionFromPrograms()
        {
            string snippet =  @"
            using System;
            namespace Client
            {
                class Program
                {
                    static void Main(string[] args)
                    {
                        string s = null;
                        Console.WriteLine(s.Length);
                    }
                }
            }";

            string expectedMessage = "Object reference not set to an instance of an object.";
            var exception =  Assert.Throws<NullReferenceException>(
                            () => Executor.Execute(snippet));

            Assert.Equal(expectedMessage, exception.Message);
        }
        
        private void ExpectOutput(string snippet, params string[] expected)
        {
            Assert.Equal(LinesOf(expected), Executor.Execute(snippet));
        }
    }
}
