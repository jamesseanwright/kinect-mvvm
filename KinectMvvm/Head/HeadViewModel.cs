using KinectMvvm.Framework;
using System;

namespace KinectMvvm.Head
{
    public class HeadViewModel : ViewModel
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }

        public void Update(HeadModel head)
        {
            X = head.X;
            Y = head.Y;
            Z = head.Z;
        }
    }
}
