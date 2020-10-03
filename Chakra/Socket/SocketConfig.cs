using System.Net;

namespace Chakra
{
  public static class SocketConfig
  {
    public static string EndOfMessage = "<EOF>";

    public static IPAddress GetServer()
    {
      // Establish the local endpoint for the socket.  
      // The DNS name of the computer  
      IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
      IPAddress ipAddress = ipHostInfo.AddressList[0];

      return ipAddress;
    }
  }
}