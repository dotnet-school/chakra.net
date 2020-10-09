using System;
using Xunit;
using static Chakra.Test.TestHelper;

namespace Chakra.Test
{
  public class ClassTest
  {
    [Fact]
    public void ShouldApplyAssertionsOnPrograms()
    {
      string snippet =  @"
            using System;
            namespace MyNameSpace
            {
                  class MyClass
                  {
                    public string Name { get; }

                    public MyClass(string name)
                    {
                      Name = name;
                    }
                  }

            }";
            
      string validation =  @"
                MyNameSpace.MyClass obj = new MyNameSpace.MyClass(""MyName"");
                Assert.Equal(""MyName"", obj.Name);
            ";

      Executor.ExecuteClass(BreakLines(snippet), BreakLines(validation) );
    }
    [Fact]
    public void ShouldReportCompileErrorInClass()
    {
      string snippet =  @"
            using System;
            namespace MyNameSpace
            {
                  class MyClass
                  {
                    public string Name { get; }

                    public MyClass(string name)
                    {
                      Name = name;
                      undefined = name;
                    }
                  }

            }";

      string expectedMessage = "CS0103: The name 'undefined' does not exist in the current context";
      int expectedLine = 12; 
      DynamicCompilationException exception = Assert.Throws<DynamicCompilationException>(
              () => Executor.ExecuteClass(BreakLines(snippet), Array.Empty<string>()));

      Assert.Equal(expectedMessage, exception.Message);
      Assert.Equal(expectedLine, exception.LineNumber);
    }
    
  }
}