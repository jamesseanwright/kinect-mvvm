using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloKinect.Sensor
{
    public interface ISensor
    {
        event EventHandler<IFrame> NewColourFrame;
        event EventHandler<IFrame> NewInfraredFrame;
        event EventHandler<IFrame> NewHeadFrame;
    }
}
