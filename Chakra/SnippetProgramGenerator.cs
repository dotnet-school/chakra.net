using System;

namespace Chakra
{
  public class SnippetProgramGenerator
  {
    private readonly static string SnippetProgramTemplate = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Chakra
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      Executor.CaptureConsole();
      //__SNIPPET__
      Executor.SendConsoleOutput(args);
    }
  }
}
     ";

    public string CreateProgramForSnippet(string[] snippetLines)
    {
      return SnippetProgramTemplate.Replace(
              "//__SNIPPET__",
              string.Join(Environment.NewLine, snippetLines));
    }
  }
}