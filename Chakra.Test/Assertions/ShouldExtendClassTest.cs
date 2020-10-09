using Chakra.Assertions;
using Xunit;
using static Chakra.Test.TestHelper;

namespace Chakra.Test
{
  public class AssertionsTest
  {
    private static string snippet = @"
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
    [Fact]
    public void ShouldApplyAssertionsOnPrograms()
    {
      string validation =  @"
                _ShouldExtendClass(typeof(MyNameSpace.MyClass), typeof(System.Object), ""Extends object"");
      ";

      Executor.ExecuteClass(BreakLines(snippet), BreakLines(validation) );
    }
    
    [Fact]
    public void ShouldThrowTestCaseException()
    {
      string validation =  @"
                _ShouldExtendClass(typeof(MyNameSpace.MyClass), typeof(String), ""shouldExtendList"");
      ";

      TestCaseException e = Assert.Throws<TestCaseException>(() =>
              Executor.ExecuteClass(BreakLines(snippet), BreakLines(validation)));
      
      Assert.Equal("shouldExtendList", e.TestCaseName);
      Assert.Equal("MyNameSpace.MyClass should extend System.String", e.Message);
    }
  }
}