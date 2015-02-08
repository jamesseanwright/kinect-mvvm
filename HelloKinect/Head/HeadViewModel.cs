using HelloKinect.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloKinect.Head
{
    public class HeadViewModel : BaseViewModel
    {
        double x;
        public double X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = value;
                RaisePropertyChanged("X");
            }
        }

        double y;
        public double Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = value;
                RaisePropertyChanged("Y");
            }
        }

        double z;
        public double Z
        {
            get
            {
                return this.z;
            }

            set
            {
                this.z = value;
                RaisePropertyChanged("Z");
            }
        }

        public void Update(Tuple<double, double, double> position)
        {
            X = position.Item1;
            Y = position.Item2;
            Z = position.Item3;
        }
    }
}
