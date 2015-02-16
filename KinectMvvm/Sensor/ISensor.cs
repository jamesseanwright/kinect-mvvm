using System;

namespace KinectMvvm.Sensor
{
    public interface ISensor
    {
        event EventHandler<ByteFrameEventArgs> NewColourFrame;
        event EventHandler<ByteFrameEventArgs> NewInfraredFrame;
        event EventHandler<HeadFrameEventArgs> NewHeadFrame;
    }
}
