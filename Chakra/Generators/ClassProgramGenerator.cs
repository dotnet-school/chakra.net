using System;
using System.Linq;

namespace Chakra
{
  public class ClassProgramGenerator
  {
    public int SnippetLineStart { get; }

    private readonly static string SnippetProgramTemplate = @"
//__IMPORTS__
//__SNIPPET__

namespace __CLASS_VALIDATIONS__
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      //__VALIDATIONS__
    }
  }
}
     ";

    public ClassProgramGenerator()
    {
      SnippetLineStart = SnippetProgramTemplate
              .Split(Environment.NewLine)
              .Select((line, index) => new {line, index})
              .First(position => position.line.Contains("//__SNIPPET__")).index;
    }
    public string CreateProgramForSnippet(string[] snippetLines, string[] validations, string[] import = null)
    {
      return SnippetProgramTemplate
              .Replace(
              "//__SNIPPET__",
              string.Join(Environment.NewLine, snippetLines))
              .Replace(
              "//__IMPORTS__",
              string.Join(Environment.NewLine, import ?? Array.Empty<string>()))
              .Replace(
              "//__VALIDATIONS__",
              string.Join(Environment.NewLine, validations));
    }
  }
}