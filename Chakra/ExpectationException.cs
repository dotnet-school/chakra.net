// Thrown when there is an expectation error in code from XUnit assertion

using System;

namespace Chakra
{
  public class ExpectationException : Exception
  {
    public string Expected { get; }
    public string Actual { get; }
    
    public ExpectationException(string expected, string actual)
    {
      Expected = expected;
      Actual = actual;
    }
  }
}