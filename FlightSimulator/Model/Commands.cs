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

        Thread threadC;

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

        public Commands(){}

        public void disConnect()
        {
            // when pressing the button disConnect, change the boollean to false
            IsConnect = false;
            tcpClient.Close();
        }

        public void connect()
        {
            // connecting as client
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
            ApplicationSettingsModel.Instance.FlightCommandPort);
            tcpClient = new TcpClient();
            tcpClient.Connect(ep);
            IsConnect = true;
            //Console.WriteLine("You are connected");
        }

        public void openThread(string text)
        {
            // split the message received
            string[] splited = Parse(text);
            // open a thread
            threadC = new Thread(() => sendMessage(splited, tcpClient));
            threadC.Start();
        }

        public void closeThread()
        {
            threadC.Abort();
        }

        public bool getIsConnect()
        {
            return IsConnect;
        }

        public void sendMessage(string[] splited, TcpClient tcpClient)
        {
            // if not connected anymore - return and do not execute the command received
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
                command += "\r\n";
                byte[] buffer = Encoding.ASCII.GetBytes(command);
                ns.Write(buffer, 0, buffer.Length);
                Thread.Sleep(2000);
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
