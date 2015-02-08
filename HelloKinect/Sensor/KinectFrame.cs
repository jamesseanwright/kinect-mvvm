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
        public KinectFrame(object data)
        {
            Data = data;
        }

        public object Data { get; private set; }
    }
}