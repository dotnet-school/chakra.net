using System;

namespace Chakra.Test
{
  public class TestHelper
  {
    public static string[] BreakLines(string str)
    {
      return  str.Split(Environment.NewLine);
    }
    
    public static string LinesOf(params string[] str)
    {
      return  string.Join(Environment.NewLine, str);
    }
  }
}