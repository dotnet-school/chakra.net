// Represent error in generated code, e.g. template for snippet
// This should not be thrown when error is in client code

using System;

namespace Chakra
{
  public class CodeTemplateException : Exception
  {
    public CodeTemplateException(string message) : base(message)
    {
    }
  }
}