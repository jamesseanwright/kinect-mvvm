using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPreview.Kinect;

namespace KinectMvvm.Sensor
{
    public class ByteFrame
    {
        public ByteFrame(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; private set; }
    }
}