using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectMvvm.Sensor
{
    public interface ISensor
    {
        event EventHandler<ByteFrameEventArgs> NewColourFrame;
        event EventHandler<ByteFrameEventArgs> NewInfraredFrame;
        event EventHandler<HeadFrameEventArgs> NewHeadFrame;
    }
}
