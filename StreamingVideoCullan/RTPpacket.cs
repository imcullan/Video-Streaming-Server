using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamingVideoCullan
{
    class RTPpacket
    {
        int version;
        int padding;
        int extension;
        int cc;
        int marker;
        int payloadType;
        int sequenceNumber;
        long timestamp;
        long ssrc;

        public RTPpacket()
        {
            this.version = 2;
            this.padding = 0;
            this.extension = 0;
            this.cc = 0;
            this.marker = 0;
            //mjpeg is type 26
            this.payloadType = 26;
            //random sequence number
            this.sequenceNumber = new Random().Next(5000, 50000);
            //random timestamp
            this.timestamp = new Random().Next(10000, 100000);
            this.ssrc = 8080;
        }

        public byte[] createPacket(byte[] videoFrame)
        {
            //create packet of size video frame + 12 (header size)
            byte[] packetizedVideoFrame = new byte[videoFrame.Length + 12];

            //first byte - V, P, X, CC (ie. << 6 means bitshift 6 to the left)
            packetizedVideoFrame[0] = (byte)((version & 0x3) << 6 | (padding & 0x1) << 5 | (extension & 0x1) << 4 | (cc & 0xf));

            //second byte - M, PT
            packetizedVideoFrame[1] = (byte)((marker & 0x1) << 7 | payloadType & 0x7f);

            //third and fourth byte - Sequence Number (Big Endian format so MSB first then LSB)
            packetizedVideoFrame[2] = (byte)((sequenceNumber & 0xff00) >> 8);
            packetizedVideoFrame[3] = (byte)(sequenceNumber & 0x00ff);

            //fifth - eigth bytes - Timestamp (Integer is 4 bytes so need to store as 4 bytes then shift)
            packetizedVideoFrame[4] = (byte)((timestamp & 0xff000000) >> 24);
            packetizedVideoFrame[5] = (byte)((timestamp & 0x00ff0000) >> 16);
            packetizedVideoFrame[6] = (byte)((timestamp & 0x0000ff00) >> 8);
            packetizedVideoFrame[7] = (byte)(timestamp & 0x000000ff);

            //ninth - twelfth bytes - ssrc
            packetizedVideoFrame[8] = (byte)((ssrc & 0xff000000) >> 24);
            packetizedVideoFrame[9] = (byte)((ssrc & 0x00ff0000) >> 16);
            packetizedVideoFrame[10] = (byte)((ssrc & 0x0000ff00) >> 8);
            packetizedVideoFrame[11] = (byte)(ssrc & 0x000000ff);

            //add video frame after the header
            System.Buffer.BlockCopy(videoFrame, 0, packetizedVideoFrame, 12, videoFrame.Length);

            //increment sequence number
            sequenceNumber++;

            //return packetized video frame
            return packetizedVideoFrame;
        }

        public void incrementPacketTime()
        {
            //increment timestamp by the applications tick value
            timestamp = timestamp + 100;
        }
    }
}
