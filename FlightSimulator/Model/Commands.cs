using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace FlightSimulator.Model
{
    class Commands
    {
        TcpClient tcpClient;

        public Commands()
        {
            connect();
        }

        public void connect()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
            ApplicationSettingsModel.Instance.FlightCommandPort);
            tcpClient = new TcpClient();
            tcpClient.Connect(ep);
            Console.WriteLine("You are connected");
        }

        public void openThread()
        {
            Thread thread = new Thread(new ThreadStart(sendMessage));
            thread.Start();

        }

        public void sendMessage()
        {
            NetworkStream ns = tcpClient.GetStream();
            using (NetworkStream stream = tcpClient.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                while (true)
                {
                    // Send data to server
                    Console.Write("Please enter a number: ");
                    string num = Console.ReadLine();
                    num += "\r\n";
                    writer.Write(num);
                    writer.Flush();
                    // Get result from server
                    string result = reader.ReadLine();
                    Console.WriteLine("Result = {0}", result);
                }
            }
        }
    }
}
