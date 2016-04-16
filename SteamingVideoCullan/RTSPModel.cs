using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SteamingVideoCullan
{
    class RTSPModel
    {
        public int port;
        public IPAddress ServerIP;
        public Socket tcpServer = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);

        public RTSPModel(int _port, IPAddress serverIP)
        {
            this.port = _port;
            this.ServerIP = serverIP;

            IPEndPoint listenEndPoint = new IPEndPoint(ServerIP, port);

            //bind the server socket to the server IP and port and start listening
            tcpServer.Bind(listenEndPoint);
            tcpServer.Listen(int.MaxValue);
        }

        public Socket AcceptOneClient()
        {
            //accept one client and return that socket
            Socket tcpClient = tcpServer.Accept();
            return tcpClient;
        }
    }
}
