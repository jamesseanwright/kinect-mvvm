using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectMvvm.Sensor
{
    public class ByteFrameEventArgs : EventArgs
    {
        public ByteFrameEventArgs(ByteFrame frame)
        {
            Frame = frame;
        }

        public ByteFrame Frame { get; private set; }
    }
}
