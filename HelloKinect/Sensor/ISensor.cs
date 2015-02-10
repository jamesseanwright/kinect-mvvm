using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloKinect.Sensor
{
    public interface ISensor
    {
        event EventHandler<FrameEventArgs> NewColourFrame;
        event EventHandler<FrameEventArgs> NewInfraredFrame;
        event EventHandler<FrameEventArgs> NewHeadFrame;
    }
}
