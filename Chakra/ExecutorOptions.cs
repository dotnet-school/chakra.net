using System;
using Microsoft.CodeAnalysis;

namespace Chakra
{
  public class ExecutorOptions
  {
    public static MetadataReference[] GetDefaultAssemblies()
    {
      var references = new MetadataReference[]
      {
              MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
              MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
              MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
              MetadataReference.CreateFromFile(typeof(System.Text.RegularExpressions.Regex).Assembly.Location),
              MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location),
              MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).Assembly.Location),
              // To load current dll in the compilation context
              MetadataReference.CreateFromFile(typeof(SocketClient).Assembly.Location),
              // For socket connections
              MetadataReference.CreateFromFile(typeof(System.Net.IPAddress).Assembly.Location),
      };
      return references;
    }

    public static string[] GetDefaultImports()
    {
      return new[]
      {
              "using System;",
              "using System.Collections.Generic;",
              "using System.Linq;",
              "using System.Threading.Tasks;",
              "using System.Text.RegularExpressions;",
      };
    }
  }
}