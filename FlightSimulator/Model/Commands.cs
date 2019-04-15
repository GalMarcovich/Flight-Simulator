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
        bool IsConnect = false;

        TcpClient tcpClient;

        private static Commands m_Instance = null;

        public static Commands Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Commands();
                }
                return m_Instance;
            }
        }

        public Commands()
        {
            //connect();
        }

        public void connect()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
            ApplicationSettingsModel.Instance.FlightCommandPort);
            tcpClient = new TcpClient();
            tcpClient.Connect(ep);
            IsConnect = true;
            Console.WriteLine("You are connected");
        }

        public void openThread(string text)
        {
            string[] splited = Parse(text);
            Thread thread = new Thread(() => sendMessage(splited, tcpClient));
            thread.Start();
        }

        public void sendMessage(string[] splited, TcpClient tcpClient)
        {
            if (!IsConnect)
            {
                return;
            }
            NetworkStream ns = tcpClient.GetStream();
            foreach (string split in splited)
            {
                // Send data to server
                Console.Write("Please enter a number: ");
                string command = split;
                //string num = Console.ReadLine();
                command += "\r\n";
                byte[] buffer = Encoding.ASCII.GetBytes(command);
                ns.Write(buffer, 0, buffer.Length);
                //writer.Write(num);
                //writer.Flush();
                // Get result from server
                //string result = reader.ReadLine();
                //Console.WriteLine("Result = {0}", result);
            }
        }

        private string[] Parse(string line)
        {
            string[] newLine = { "\r\n" };
            string[] input = line.Split(newLine, StringSplitOptions.None);
            return input;
        }
    }
}
