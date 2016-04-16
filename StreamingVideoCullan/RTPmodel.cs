using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SteamingVideoCullan
{
    class RTPmodel
    {
        //UDP socket to send RTP packets to clients
        Socket UDPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint clientEndPoint;
        RTPpacket _RTPpacket;

        public RTPmodel(String IP, String port) {
            //create client end point
            this.clientEndPoint = new IPEndPoint(IPAddress.Parse(IP), Int32.Parse(port));
            //use to packetize the packet
            this._RTPpacket = new RTPpacket();
        }

        public byte[] sendPacket(byte[] videoPacket)
        {
            //packetize (add header) to the video packet
            byte[] packetizedVideoFrame = _RTPpacket.createPacket(videoPacket);

            try
            {
                //send the packetized video packet to the client
                UDPSocket.SendTo(packetizedVideoFrame, clientEndPoint);
                return packetizedVideoFrame;
            }
            catch (SocketException err)
            {
                Console.WriteLine("SendTo failed: ", err.Message);
                return null;
            }
        }

        //increment packet time
        public void incrementPacketTime()
        {
            _RTPpacket.incrementPacketTime();
        }

    }
}
