// Represent compilation error in dynamic code
// Error could be in templates or user input

using System;

namespace Chakra
{
  public class DynamicCompilationException : Exception
  {
    public DynamicCompilationException(string message) : base(message)
    {
    }
  }
}