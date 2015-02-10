using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectMvvm.Sensor
{
    public class FrameEventArgs : EventArgs
    {
        public FrameEventArgs(IFrame frame)
        {
            Frame = frame;
        }

        public IFrame Frame { get; private set; }
    }
}
