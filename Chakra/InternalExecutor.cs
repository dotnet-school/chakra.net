using System;
using System.Net;
using System.Threading.Tasks;

namespace Chakra
{
  public class InternalExecuter
  {
    public static string CompileAndRun(string fileName, string sourceCode, string[] args)
    {
      IPAddress ipAddress = SocketConfig.GetServer();
      Random random = new Random();
      int port = random.Next(50000, 65000);
      Console.WriteLine($"using port {port}");

      SocketServer server = Listen(ipAddress, port);

      var compiler = new Compiler();
      var runner = new Runner();
      byte[] compiled = compiler.Compile(fileName, sourceCode);

      if (compiled == null)
      {
        throw new DynamicCompilationException($"Please check the code syntax : {Environment.NewLine} {sourceCode}");
      }
      runner.Execute(compiled, args, port);

      return server.GetMessage();
    }

    private static SocketServer Listen(IPAddress ipAddress, int port)
    {
      SocketServer server = new SocketServer();
      Task.Run(() =>
      {
        server.StartListening(ipAddress, port);
      });
      return server;
    }
  }
}