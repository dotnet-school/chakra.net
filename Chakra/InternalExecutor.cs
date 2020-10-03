using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace Chakra
{
  public class InternalExecuter
  {
    public static string CompileAndRun(string sourceCode, string[] args, MetadataReference[] assemblies)
    {
      IPAddress ipAddress = SocketConfig.GetServer();
      Random random = new Random();
      int port = random.Next(50000, 65000);

      SocketServer server = Listen(ipAddress, port);

      var compiler = new Compiler();
      var runner = new Runner();
      byte[] compiled = compiler.Compile(sourceCode, assemblies);
      
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