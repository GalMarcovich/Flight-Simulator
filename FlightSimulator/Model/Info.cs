using System;
using System.Threading;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace FlightSimulator.Model
{
    class Info
    {
        float lon, lat;

        TcpClient _client;

        private static Info m_Instance = null;

        public static Info Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Info();
                }
                return m_Instance;
            }
        }

        private Info() {}

        public void connect()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightInfoPort);
            TcpListener listener = new TcpListener(ep);
            listener.Start();
            //Console.WriteLine("Waiting for client connections...");
            _client = listener.AcceptTcpClient();
            Console.WriteLine("Info channel: Client connected");
            Thread thread = new Thread(() => listen(_client));
            thread.Start();
        }

        public void listen(TcpClient _client)
        {
            Byte[] bytes;
            //string[] splitMsg = new string[23];
            //string lon, lat;
            NetworkStream ns = _client.GetStream();
            while (true)
            {
                if (_client.ReceiveBufferSize > 0)
                {
                    bytes = new byte[_client.ReceiveBufferSize];
                    ns.Read(bytes, 0, _client.ReceiveBufferSize);
                    string msg = Encoding.ASCII.GetString(bytes); //the message incoming
                    splitMsg(msg);
                    Console.WriteLine(msg);
                }
            }
        }

        public void splitMsg(string msg)
        {
            string[] splitMs = msg.Split(',');
            lon = float.Parse(splitMs[0]);//TODO
            lat = float.Parse(splitMs[1]);
        }
    }
}