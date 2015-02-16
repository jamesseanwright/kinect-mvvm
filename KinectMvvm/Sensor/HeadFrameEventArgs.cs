using System;

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
