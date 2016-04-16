using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamingVideoCullan
{
    public partial class View : Form
    {
        Controller myController = new Controller();

        public View()
        {
            InitializeComponent();
            listen.Click += myController.Listen_btn_Click;
        }

        //return the port number
        public String GetPortNumber()
        {
            String port = this.port.Text;
            return port;
        }

        public void Disable_Listen()
        {
            this.listen.Enabled = false;
        }

        //return the server IP
        public IPAddress GetServerIP()
        {
            IPAddress serverIP = IPAddress.Parse(this.ServerIPAddress.Text);
            return serverIP;
        }

        //delegate to use function as a parameter
        delegate void SetInfoCallback(String info);

        //append text to the server box
        public void SetServerBox(String _msg) {
            String text = _msg;
            SetInfoCallback callback = new SetInfoCallback(add_server_text);
            this.Invoke(callback, new object[] { text });
        }

        public void add_server_text (String _msg)
        {
            this.serverStatus.Text += _msg;
        }
        
        //append text to the client box
        public void SetClientBox(String _msg)
        {
            String text = _msg;
            SetInfoCallback callback = new SetInfoCallback(add_client_text);
            this.Invoke(callback, new object[] { text });
        }

        public void add_client_text(String _msg)
        {
            this.clientRequests.Text += _msg;
        }

        //if we want to change the server IP
        public void SetServerIP(String _msg)
        {
            String text = _msg;
            this.ServerIPAddress.Text = text;
        }

        public void ShowFrameNumber(String _msg)
        {

        }

        //check if the RTP header is checked
        public Boolean PrintRTPHeaderChecked()
        {
            if (PrintRTPHeader.CheckState.ToString() == "Checked")
                return true;
            else
                return false;
        }
    }
}
