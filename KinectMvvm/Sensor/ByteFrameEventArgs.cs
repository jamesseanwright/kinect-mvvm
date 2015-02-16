using System;

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
