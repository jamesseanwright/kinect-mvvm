using System;
using System.Collections.Generic;

namespace KinectMvvm.Sensor
{
    public class HeadFrame
    {
        List<Tuple<double, double, double>> heads;

        public HeadFrame(List<Tuple<double, double, double>> heads)
        {
            this.heads = heads;
        }

        public List<Tuple<double, double, double>> Heads
        {
            get { return this.heads; }
        }
    }
}
