using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPreview.Kinect;

namespace HelloKinect.Sensor
{
    public class KinectFrame : IFrame
    {
        public KinectFrame(IDisposable frame, object data)
        {
            Frame = frame;
            Data = data;
        }

        public IDisposable Frame { get; private set; }
        public object Data { get; private set; }
    }
}
