using System;
using System.Linq;

namespace Chakra
{
  public class SnippetProgramGenerator
  {
    public int SnippetLineStart { get; }

    private readonly static string SnippetProgramTemplate = @"
//__IMPORTS__

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

    public SnippetProgramGenerator()
    {
      SnippetLineStart = SnippetProgramTemplate
              .Split(Environment.NewLine)
              .Select((line, index) => new {line, index})
              .First(position => position.line.Contains("//__SNIPPET__")).index;
    }
    public string CreateProgramForSnippet(string[] snippetLines, string[] imports)
    {
      return SnippetProgramTemplate
              .Replace(
              "//__SNIPPET__",
              string.Join(Environment.NewLine, snippetLines))
              .Replace(
              "//__IMPORTS__",
              string.Join(Environment.NewLine, imports));
    }
  }
}