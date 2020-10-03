namespace Chakra
{
  public class Messaging
  {
    public static void SendMessage(string message, int callbackPort)
    {
      var socketClient = new SocketClient();
      socketClient.StartClient(SocketConfig.GetServer(), callbackPort);
      socketClient.SendData(message);
      socketClient.Close();
    }
  }
}