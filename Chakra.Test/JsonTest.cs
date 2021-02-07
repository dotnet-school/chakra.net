using System;
using Xunit;

namespace Chakra.Test
{
  public class JsonTest
  {
    [Fact]
    public void ShouldSupportJson()
    {

      
      string snippet = @"
       var csharp = new
      {
        name = ""nishant"",
        adress = new {city= ""Varanasi"", country = ""India""}
      };
      Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(csharp));  
    ";

      var expected = "{\"name\":\"nishant\",\"adress\":{\"city\":\"Varanasi\",\"country\":\"India\"}}";
      
      string result = Executor.ExecuteSnippet(snippet.Split(Environment.NewLine));

      Assert.Equal(expected, result);
      
    }
  }
}