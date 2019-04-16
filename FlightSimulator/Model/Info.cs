using System;
using System.Threading;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using FlightSimulator.ViewModels;

namespace FlightSimulator.Model
{
    class Info : BaseNotify
    {
        private float lon, lat;

        Thread threadI;

        TcpClient _client;

        TcpListener listener;

        public bool shouldStop
        {
            get;
            set;
        }

        public float Lon
        {
            get
            {
                return lon;
            }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        public float Lat
        {
            get
            {
                return lat;
            }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }

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

        private Info() {
            shouldStop = false;
        }

        public void closeThread()
        {
            threadI.Abort();
        }

        public void disConnect()
        {
            shouldStop = true;
            _client.Close();
        }

        public void connect()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightInfoPort);
            listener = new TcpListener(ep);
            listener.Start();
            //Console.WriteLine("Waiting for client connections...");
            _client = listener.AcceptTcpClient();
            Console.WriteLine("Info channel: Client connected");
            threadI = new Thread(() => listen(_client));
            threadI.Start();
        }

        public void listen(TcpClient _client)
        {
            Byte[] bytes;
            //string[] splitMsg = new string[23];
            //string lon, lat;
            NetworkStream ns = _client.GetStream();
            while (!shouldStop)
            {
                if (_client.ReceiveBufferSize > 0)
                {
                    bytes = new byte[_client.ReceiveBufferSize];
                    ns.Read(bytes, 0, _client.ReceiveBufferSize);
                    string msg = Encoding.ASCII.GetString(bytes); //the message incoming
                    splitMsg(msg);
                    Console.WriteLine("info");
                    Console.WriteLine(Lon);
                    Console.WriteLine(Lat);
                    Console.WriteLine(msg);
                }
            }
            ns.Close();
            _client.Close();
            listener.Stop();
        }

        public void splitMsg(string msg)
        {
            string[] splitMs = msg.Split(',');
            Lon = float.Parse(splitMs[0]);//TODO
            Lat = float.Parse(splitMs[1]);
        }
    }
}