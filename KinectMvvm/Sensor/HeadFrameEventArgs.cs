using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectMvvm.Sensor
{
    public class HeadFrameEventArgs : EventArgs
    {
        public HeadFrameEventArgs(HeadFrame frame)
        {
            Frame = frame;
        }

        public HeadFrame Frame { get; private set; }
    }
}
