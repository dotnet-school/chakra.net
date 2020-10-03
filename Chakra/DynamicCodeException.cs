// Represent error in dynamic code, e.g. template for snippet
// This should not be thrown when error is in client code

using System;

namespace Chakra
{
  public class DynamicCodeException : Exception
  {
    public DynamicCodeException(string message) : base(message)
    {
    }
  }
}