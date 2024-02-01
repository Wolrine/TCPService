﻿using System.Net;
using System.Net.Sockets;

namespace ServerApp
{
    public class ServerCon
    {
        public void ProcessMessage(object parm)
        {
            string data;
            int count;

            try
            {
                TcpClient client = parm as TcpClient;
                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();
                // Loop to receive all the data sent by the client
                while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, count);
                    Console.WriteLine($"Received: {data} at {DateTime.Now:t}");
                    // Process the data sent by the client
                    data = $"{data.ToUpper()}";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                    // Send back a response
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine($"Sent: {data}");
                }
                // Shutdown and end connection
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                Console.WriteLine("Waiting message...");
            }
        }

        public void ExecuteServer(string host, int port)
        {
            int Count = 0;
            TcpListener? server = null;
            try
            {
                Console.Title = "Server Application";
                IPAddress localAddr = IPAddress.Parse(host);
                server = new TcpListener(localAddr, port);
                // Start listening for client request
                server.Start();
                Console.WriteLine(new string('*', 40));
                Console.WriteLine("Waiting for a connection...");
                // Enter the listening loop
                while (true)
                {
                    // Perform a blocking call to accept requests
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine($"Number of client connected: {++Count}");
                    Console.WriteLine(new string('*', 40));
                    // Create a thread to receive and send message
                    Thread thread = new(new ParameterizedThreadStart(ProcessMessage));
                    thread.Start(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            finally
            {
                server.Stop();
                Console.WriteLine("Server stopped. Press any key to exit !");
            }
            Console.Read();
        }
    }
}
