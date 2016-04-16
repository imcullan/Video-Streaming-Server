using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamingVideoCullan
{
    class MJPEGVideo
    {
        String filename;
        string pathSource;
        FileStream fsSource = null;

        public MJPEGVideo(String filename)
        {
            this.filename = filename;
            this.pathSource = @"c:\video\" + filename;
            try
            {
                //create file stream to read video file
                 this.fsSource = new FileStream(pathSource, FileMode.Open, FileAccess.Read);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public byte[] getnextframe()
        {
            int length = 0;
            String length_string;

            //create empty 5 byte array to store frame length
            byte[] frame_length = new byte[5];

            //read current frame length
            fsSource.Read(frame_length, 0, 5);

            //transform frame_length to integer
            length_string = Encoding.Default.GetString(frame_length);
            length = Int32.Parse(length_string);

            //create bytearray to store the frame
            byte[] newbytearray = new byte[length];
            fsSource.Read(newbytearray, 0, length);

            //return the frame
            return newbytearray;
        }

        public Boolean EndofFile()
        {
            //check if end of video
            return (fsSource.Position >= fsSource.Length - 1);
        }

        public void ResetVideo()
        {
            //reset video
            this.fsSource.Seek(0, SeekOrigin.Begin);
        }
    }
}
