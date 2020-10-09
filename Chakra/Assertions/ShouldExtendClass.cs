namespace Chakra.Assertions
{
  public static class ShouldExtendClass
  {
    public static void _ShouldExtendClass(System.Type type, System.Type superType, string testName)
    {
      if (!type.IsSubclassOf(superType))
      {
        throw new TestCaseException(testName, $"{type.FullName} should extend {superType.FullName}");
      }
    }
  }
}