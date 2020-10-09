using System;

namespace Chakra.Assertions
{
  public class TestCaseException : Exception
  {
    public string TestCaseName { get; }

    public TestCaseException(string testCaseName, string message) : base(message)
    {
      TestCaseName = testCaseName;
    }
  }
}