using System;
using Xunit;

using static Chakra.Test.TestHelper;
namespace Chakra.Test
{
    public class ExecutorTest
    {
        [Fact]
        public void ShouldPrintConsole()
        {
            string[] snippet =  {"Console.WriteLine(\"Hello World !\");"};
            string result = Executor.ExecuteSnippet(snippet);
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

            ExpectOutput(snippet, "i=1", "i=2", "i=3");
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

            ExpectOutput(script, "before task", "In the task", "after task");
        }
        
        [Fact]
        public void ShouldSupportListsEnumerableArraysAndDictionary()
        {
            string script = @"
              IEnumerable<string> list = new List<string>() {""item one"", ""item two""};
              if (list.ToArray().Equals(Array.Empty<string>()))
              {
                throw new Exception(""This never happens"");
              }
              Console.WriteLine(string.Join("", "", list));
              var days = new Dictionary<string, string> { 
                      [""mo""] =  ""Monday"", 
                      [""tu""] =  ""Tuesday"", 
                      [""we""] =  ""Wednesday"", 
                      [""th""] =  ""Thursday"", 
                      [""fr""] =  ""Friday"", 
                      [""sa""] =  ""Saturday"", 
                      [""su""] =  ""Sunday""
              };
              Console.WriteLine(string.Join("", "", days.Keys));
              Console.WriteLine(string.Join("", "", days.Values));
            ";
            
            ExpectOutput(script,         
                "item one, item two",
                "mo, tu, we, th, fr, sa, su",
                "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday");
        }
        
        [Fact] 
        public void ShouldThrowActualExceptionFromDynamicCode()
        {   string snippet = @"
                string s = null;
                Console.WriteLine(s.Length);
            ";
            Assert.Throws<NullReferenceException>(() => Executor.ExecuteSnippet(BreakLines(snippet)));
        }
        
        [Fact] 
        public void ShouldSupportRegex()
        {   string snippet = @"
                Console.WriteLine(Regex.Replace(""my name"", ""n..e"", ""identity""));
            ";
            ExpectOutput(snippet, "my identity");
        }
        
                
        [Fact] 
        public void ShouldReportCompileErrorsWithLine()
        {  
            string snippet = @"
            for (var i = 0; i < 3; i++)
              {
                Console.WriteLine(i);
              }

              int a = undefined;";
            string expectedMessage = "CS0103: The name 'undefined' does not exist in the current context";
            int expectedLine = 7; 
            DynamicCompilationException exception = Assert.Throws<DynamicCompilationException>(
                            () => Executor.ExecuteSnippet(BreakLines(snippet)));
            Assert.Equal(expectedMessage, exception.Message);
            Assert.Equal(expectedLine, exception.LineNumber);
        }
        
        [Fact] 
        public void ShouldSupportAssertions()
        {  
            string snippet = @"
            int value = 34;
            Assert.Equal(""expected value"" , ""actual value"");";

            string expectedMessage = "Assert.Equal() Failure\nExpected: 3\nActual·";
            int expectedLine = 2;
            var exception = Assert.Throws<ExpectationException>(
                            () => Executor.ExecuteSnippet(BreakLines(snippet)));

            Assert.Equal("actual value", exception.Actual);
            Assert.Equal("expected value", exception.Expected);
        }
        
        private void ExpectOutput(string snippet, params string[] expected)
        {
            Assert.Equal(LinesOf(expected), Executor.ExecuteSnippet(BreakLines(snippet)));
        }
    }
    
    // Todo should report line numbers in errors
    // TODO should throw compile error
    // TODO Should support exceptions
    // TODO should support reading a file
    // Todo should support files, path
    // Todo should support creating classes
    // Todo should support regex
    // Todo should support classes, getter, setter
    // Todo should support namespace
    // Todo should support xunit
    // Todo should support moq
    // Todo should support expectations and reporting
}
