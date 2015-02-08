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
        float x;
        public float X
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

        float y;
        public float Y
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

        float z;
        public float Z
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
    }
}
