// Represent compilation error in dynamic code
// Error could be in templates or user input

using System;

namespace Chakra
{
  public class DynamicCompilationException : Exception
  {
    public int LineNumber { get; }

    public DynamicCompilationException(string message, int lineNumber) : base(message)
    {
      LineNumber = lineNumber;
    }

    public DynamicCompilationException(DynamicCompilationException e, int offsetLine) : base(e.Message)
    {
      LineNumber = e.LineNumber - offsetLine;
    }
  }
}