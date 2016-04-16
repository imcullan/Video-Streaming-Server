using System;

namespace SteamingVideoCullan
{
    partial class View
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.port = new System.Windows.Forms.TextBox();
            this.ServerIPAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PrintRTPHeader = new System.Windows.Forms.CheckBox();
            this.frameLabel = new System.Windows.Forms.Label();
            this.frameNo = new System.Windows.Forms.TextBox();
            this.listen = new System.Windows.Forms.Button();
            this.serverLabel = new System.Windows.Forms.Label();
            this.serverStatus = new System.Windows.Forms.TextBox();
            this.clientRequests = new System.Windows.Forms.TextBox();
            this.clientLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(103, 39);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(100, 20);
            this.port.TabIndex = 0;
            // 
            // ServerIPAddress
            // 
            this.ServerIPAddress.Location = new System.Drawing.Point(126, 83);
            this.ServerIPAddress.Name = "ServerIPAddress";
            this.ServerIPAddress.ReadOnly = true;
            this.ServerIPAddress.Size = new System.Drawing.Size(100, 20);
            this.ServerIPAddress.TabIndex = 1;
            this.ServerIPAddress.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Listen on Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Server IP Address";
            // 
            // PrintRTPHeader
            // 
            this.PrintRTPHeader.AutoSize = true;
            this.PrintRTPHeader.Location = new System.Drawing.Point(320, 42);
            this.PrintRTPHeader.Name = "PrintRTPHeader";
            this.PrintRTPHeader.Size = new System.Drawing.Size(110, 17);
            this.PrintRTPHeader.TabIndex = 5;
            this.PrintRTPHeader.Text = "Print RTP Header";
            this.PrintRTPHeader.UseVisualStyleBackColor = true;
            // 
            // frameLabel
            // 
            this.frameLabel.AutoSize = true;
            this.frameLabel.Location = new System.Drawing.Point(278, 86);
            this.frameLabel.Name = "frameLabel";
            this.frameLabel.Size = new System.Drawing.Size(46, 13);
            this.frameLabel.TabIndex = 7;
            this.frameLabel.Text = "Frame #";
            // 
            // frameNo
            // 
            this.frameNo.Location = new System.Drawing.Point(327, 83);
            this.frameNo.Name = "frameNo";
            this.frameNo.ReadOnly = true;
            this.frameNo.Size = new System.Drawing.Size(100, 20);
            this.frameNo.TabIndex = 6;
            this.frameNo.Text = "0";
            // 
            // listen
            // 
            this.listen.Location = new System.Drawing.Point(209, 33);
            this.listen.Name = "listen";
            this.listen.Size = new System.Drawing.Size(75, 31);
            this.listen.TabIndex = 2;
            this.listen.Text = "Listen";
            this.listen.UseVisualStyleBackColor = true;
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(29, 135);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(71, 13);
            this.serverLabel.TabIndex = 8;
            this.serverLabel.Text = "Server Status";
            // 
            // serverStatus
            // 
            this.serverStatus.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.serverStatus.Location = new System.Drawing.Point(32, 154);
            this.serverStatus.Multiline = true;
            this.serverStatus.Name = "serverStatus";
            this.serverStatus.ReadOnly = true;
            this.serverStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.serverStatus.Size = new System.Drawing.Size(398, 129);
            this.serverStatus.TabIndex = 9;
            // 
            // clientRequests
            // 
            this.clientRequests.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clientRequests.Location = new System.Drawing.Point(32, 322);
            this.clientRequests.Multiline = true;
            this.clientRequests.Name = "clientRequests";
            this.clientRequests.ReadOnly = true;
            this.clientRequests.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.clientRequests.Size = new System.Drawing.Size(398, 129);
            this.clientRequests.TabIndex = 11;
            // 
            // clientLabel
            // 
            this.clientLabel.AutoSize = true;
            this.clientLabel.Location = new System.Drawing.Point(29, 303);
            this.clientLabel.Name = "clientLabel";
            this.clientLabel.Size = new System.Drawing.Size(81, 13);
            this.clientLabel.TabIndex = 10;
            this.clientLabel.Text = "Client Requests";
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 487);
            this.Controls.Add(this.clientRequests);
            this.Controls.Add(this.clientLabel);
            this.Controls.Add(this.serverStatus);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.frameLabel);
            this.Controls.Add(this.frameNo);
            this.Controls.Add(this.PrintRTPHeader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listen);
            this.Controls.Add(this.ServerIPAddress);
            this.Controls.Add(this.port);
            this.Name = "View";
            this.Text = "SE3314B Video Streaming";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox ServerIPAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox PrintRTPHeader;
        private System.Windows.Forms.Label frameLabel;
        private System.Windows.Forms.TextBox frameNo;
        private System.Windows.Forms.Button listen;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.TextBox serverStatus;
        private System.Windows.Forms.TextBox clientRequests;
        private System.Windows.Forms.Label clientLabel;
    }
}

