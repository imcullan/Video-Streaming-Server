using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SteamingVideoCullan
{
    class Controller
    {
        Thread listenThread;
        private static View _view;
        RTSPModel _RTSPModel = null;
        Thread clientThread;
        int clientNum = 1;

        public void Listen_btn_Click(object sender, EventArgs e)
        {
            //Determine which view to control
            _view = (View)((Button)sender).FindForm();
            _view.Disable_Listen();

            //Create a thread to accept from connected clients
            this.listenThread = new Thread(ListenForClients);
            listenThread.IsBackground = true;
            this.listenThread.Start();
        }

        public void incrementTimer(object sender, EventArgs e, ref RTPmodel _RTPmodel, ref MJPEGVideo video)
        {
            //increment packet timer
            _RTPmodel.incrementPacketTime();

            //check if end of file and if it is, then reset file
            if (video.EndofFile())
                video.ResetVideo();

            //get the next frame
            byte[] videoBuffer = video.getnextframe();

            //packetize and send the next frame
            byte[] packetizedVideoFrame = _RTPmodel.sendPacket(videoBuffer);

            //create blank byte array to store header bytes
            byte[] headerBytes = new byte[12];

            //copy first 12 bytes of the packetized video frame into the headerBytes byte array
            System.Buffer.BlockCopy(packetizedVideoFrame, 0, headerBytes, 0, 12);

            //convert the header bytes into bits
            string headerBits = string.Join(" ", headerBytes.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));

            //if print RTP header is checked, then display the header bits
            if (_view.PrintRTPHeaderChecked())
                this.UpdateServerBox(Environment.NewLine + "RTP Header: " + headerBits + Environment.NewLine);
        }

        public void ListenForClients()
        {
            //Create a model to listen from clients
            _RTSPModel = new RTSPModel(Int32.Parse(_view.GetPortNumber()), this.GetServerIP());
            //Update the view
            UpdateServerIP(_RTSPModel.ServerIP.ToString());

            while (true)
            {
                UpdateServerBox("Server is waiting for new connection #" + clientNum + Environment.NewLine);

                //blocks until a client has connected to the server
                Socket RTSPsocket = _RTSPModel.AcceptOneClient();

                clientNum++;
                //create a thread to handle communication with connected client
                clientThread = new Thread(new ParameterizedThreadStart(Communications));
                clientThread.IsBackground = true; //to stop all threads with application is terminated
                clientThread.Start(RTSPsocket);
            }
        }

        public void Communications(object obj)
        {

            //byte[] receiveBuffer = new byte[4096];
            Dictionary<String, String> request = new Dictionary<String, String>();

            MJPEGVideo video = null;
            RTPmodel _RTPmodel = null;

            ClientModel _clientModel = null;

            Socket clientSocket = (Socket)obj;

            //update server box to show that a client has connected
            this.UpdateServerBox("Accepted connection from: "
                + clientSocket.RemoteEndPoint.ToString() + Environment.NewLine);

            _clientModel = new ClientModel(clientSocket);

            //watch for timer to tick and run increment timer function when ticks
            _clientModel.timer.Elapsed += new ElapsedEventHandler((sender, e) => incrementTimer(sender, e, ref _RTPmodel, ref video));

            while (true)
            {
                //client request
                String msg = _clientModel.listen();

                //break loop if error message (ie. client leaves)
                if (msg.Contains("Error"))
                    break;

                //parse client request
                parseMessage(msg, ref request);

                //if dictionary is empty after TEARDOWN
                if (request.Count == 0)
                    continue;

                if (msg.Contains("SETUP"))
                {
                    this.UpdateServerBox("Client " + clientSocket.RemoteEndPoint.ToString()
                        + " has setted up" + Environment.NewLine);

                    //create RTP model
                    _RTPmodel = new RTPmodel(request["clientIP"], request["clientPort"]);

                    //load video
                    video = new MJPEGVideo(request["filename"]);
                }
                else if (msg.Contains("PLAY"))
                {
                    this.UpdateServerBox("Client " + clientSocket.RemoteEndPoint.ToString()
                        + " is playing " + request["filename"] + Environment.NewLine);

                    //start the timer
                    _clientModel.timer.Start();
                }
                else if (msg.Contains("PAUSE"))
                {
                    this.UpdateServerBox("Client " + clientSocket.RemoteEndPoint.ToString()
                        + " paused " + request["filename"] + Environment.NewLine);

                    //pause the timer
                    _clientModel.timer.Stop();
                }
                else if (msg.Contains("TEARDOWN"))
                {
                    this.UpdateServerBox("Client " + clientSocket.RemoteEndPoint.ToString()
                        + " teared down " + request["filename"] + Environment.NewLine);

                    //stop timer
                    _clientModel.timer.Stop();

                    //server reply
                    _clientModel.send(request);

                    //reset everything when tear down so client can set up again
                    request = new Dictionary<String, String>();
                    video = null;
                    _RTPmodel = null;
                    _clientModel = null;
                    clientSocket = (Socket)obj;
                    _clientModel = new ClientModel(clientSocket);
                    _clientModel.timer.Elapsed += new ElapsedEventHandler((sender, e) => incrementTimer(sender, e, ref _RTPmodel, ref video));
                    continue;
                }

                //server reply
                if (msg.Contains("SETUP") || msg.Contains("PLAY") || msg.Contains("PAUSE") || msg.Contains("TEARDOWN"))
                {
                    _clientModel.send(request);
                }
            }
        }

        public void parseMessage(String requestMsg, ref Dictionary<String, String> request)
        {
            requestMsg = requestMsg.Trim();

            //split the request message by blank space (both " " and "\r\n" are considered as blank spaces)
            String[] requestArr = requestMsg.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            //save the request type
            String requestType = requestArr[0];

            if (requestType == "SETUP")
            {
                //save request type
                request.Add("requestType", requestType);

                //save the RTSP version
                String RTSPversion = requestArr[2];
                request.Add("RTSPversion", RTSPversion);

                //save the sequence number
                String sequenceNum = requestArr[4];
                request.Add("sequenceNum", sequenceNum);

                //save the file name
                String[] fileLocArr = requestArr[1].Split('/');
                String filename = fileLocArr[3];
                request.Add("filename", filename);

                //save the client IP
                String[] clientArr = fileLocArr[2].Split(':');
                String clientIP = clientArr[0];
                request.Add("clientIP", clientIP);

                //save the client Port
                String clientPort = requestArr[8];
                request.Add("clientPort", clientPort);

                this.UpdateClientBox(Environment.NewLine + requestMsg + Environment.NewLine);
            }
            else if (requestType == "PLAY")
            {
                //save request type
                request["requestType"] = requestType;

                //increment the sequence number
                String sequenceNum = request["sequenceNum"];
                int seqNum = Int32.Parse(sequenceNum);
                seqNum++;
                request["sequenceNum"] = seqNum.ToString();

                this.UpdateClientBox(Environment.NewLine + requestMsg + " ");
            }
            else if (requestType == "PAUSE")
            {
                //save request type
                request["requestType"] = requestType;

                //increment the sequence number
                String sequenceNum = request["sequenceNum"];
                int seqNum = Int32.Parse(sequenceNum);
                seqNum++;
                request["sequenceNum"] = seqNum.ToString();

                this.UpdateClientBox(Environment.NewLine + requestMsg + " ");
            }
            else if (requestType == "TEARDOWN")
            {
                //save request type
                request["requestType"] = requestType;

                //increment the sequence number
                String sequenceNum = request["sequenceNum"];
                int seqNum = Int32.Parse(sequenceNum);
                seqNum++;
                request["sequenceNum"] = seqNum.ToString();

                this.UpdateClientBox(Environment.NewLine + requestMsg + " " + Environment.NewLine);
            }
            else
            {
                //update client box showing second part of message if not part of first
                this.UpdateClientBox(requestMsg + Environment.NewLine);
            }
        }

        public void UpdateServerIP(string msg)
        {
            _view.SetServerIP(msg);
        }

        public IPAddress GetServerIP()
        {
            IPAddress serverIP = _view.GetServerIP();
            return serverIP;
        }

        public void UpdateServerBox(string msg)
        {
            _view.SetServerBox(msg);
        }

        public void UpdateClientBox(string msg)
        {
            _view.SetClientBox(msg);
        }

        public void UpdateFrameNo(string msg)
        {
            _view.ShowFrameNumber(msg);
        }
    }
}
