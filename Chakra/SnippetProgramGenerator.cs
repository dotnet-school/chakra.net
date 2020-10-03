using System;

namespace Chakra
{
  public class SnippetProgramGenerator
  {
    private readonly static string SnippetProgramTemplate = @"
      using System;
      using System.Linq;
      using System.Collections;
      using System.Collections.Generic;
      using System.Threading.Tasks;
      using SystemConsole = System.Console; // To capture console

      namespace Chakra
      {
        public class Program
        {
          public static async Task Main(string[] args)
          {
            int callbackPort = int.Parse(args[0]);
            __SNIPPET__
            await Task.Run(() =>
            {
              var socketClient = new SocketClient();
              socketClient.StartClient(SocketConfig.GetServer(), callbackPort);
              socketClient.SendData(string.Join(Environment.NewLine, Console.Lines));
              socketClient.Close();
            });
          }
        }
      }


      namespace Chakra
      {
        public static class Console
        {
          // public static ErrorConsole Error = new ErrorConsole();
          public static IList<string> Lines = new List<string>(); 
          public static void WriteLine(string message)
          {
            Lines.Add(message);
          }
        }
      }
    ";

    public string CreateProgramForSnippet(string[] snippetLines)
    {
      return SnippetProgramTemplate.Replace(
              "__SNIPPET__",
              string.Join(Environment.NewLine, snippetLines));
    }
  }
}