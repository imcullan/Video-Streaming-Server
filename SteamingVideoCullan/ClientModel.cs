using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SteamingVideoCullan
{
    class ClientModel
    {
        //byte array to store request and response messages between client and server
        public byte[] receiveBuffer = new byte[4096];
        Socket clientSocket = null;
        public int sessionNum;

        //timer used to send the images at the video frame rate
        public System.Timers.Timer timer = new System.Timers.Timer();

        public ClientModel(Socket _clientSocket)
        {
            this.clientSocket = _clientSocket;
            // set session ID as a random number between 7000 and 8000
            this.sessionNum = new Random().Next(7000, 8000);

            //set the timer's interval to 100
            this.timer.Interval = 100;
        }

        public String returnMsg(String msg)
        {
            return msg;
        }

        public void send(Dictionary<String, String> clientRequestMsg)
        {
            try
            {
                //server send reply to client
                String serverResponseMsg = clientRequestMsg["RTSPversion"] + " 200 OK\r\n"
                    + "CSeq: " + clientRequestMsg["sequenceNum"] + "\r\n" + "Session: " + sessionNum;
                receiveBuffer = System.Text.Encoding.UTF8.GetBytes(serverResponseMsg);
                clientSocket.Send(receiveBuffer);
            }
            catch (SocketException err)
            {
                Console.WriteLine("Error occurred on accepted socket:" + err.Message + Environment.NewLine + Environment.NewLine);
            }
        }

        public String listen()
        {
            try
            {
                //server block on read for client's reply
                int rc = clientSocket.Receive(receiveBuffer);
                if (rc == 0)
                    return "Error";
                return Encoding.Default.GetString(receiveBuffer) + Environment.NewLine + Environment.NewLine;
            }
            catch (SocketException err)
            {
                return "Error occurred on accepted socket:" + err.Message + Environment.NewLine + Environment.NewLine;
            }
        }
    }
}
