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
  }
}