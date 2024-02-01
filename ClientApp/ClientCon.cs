using System.Net.Sockets;

namespace ClientApp
{
    public class ClientCon
    {
        public void ConnectServer(string server, int port)
        {
            string message, responseData;
            int bytes;
            try
            {
                //Create a TcpClient
                TcpClient client = new(server, port);
                Console.Title = "Client Application";
                NetworkStream stream = null;
                while (true)
                {
                    Console.WriteLine("Input message <press Enter to exit>:");
                    message = Console.ReadLine();
                    if (message == string.Empty)
                    {
                        break;
                    }
                    // Translate the passed message into ASCII and store it as a byte array
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes($"{message}");
                    // Get a client for reading and writing
                    stream = client.GetStream();
                    // Send the message to the connected TcpServer
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine($"Sent: {message}");

                    /* Receive the TcpServer response*/
                    // Use buffer to store the response bytes
                    data = new Byte[256];
                    // Read the first batch of the TcpServer response bytes
                    bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine($"Received: {responseData}");
                }
                // Shutdown and end connection
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }
    }
}
