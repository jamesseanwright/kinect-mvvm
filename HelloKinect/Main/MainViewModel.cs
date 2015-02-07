using HelloKinect.Framework;
using HelloKinect.Head;
using HelloKinect.Sensor;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;

namespace HelloKinect.Main
{
    public class MainViewModel : BaseViewModel
    {
        ISensorService sensorService;

        public MainViewModel(ISensorService sensorService)
        {
            this.sensorService = sensorService;
            this.sensorService.NewFrame += NewFrame;
        }

        private void NewFrame(object sender, IFrame e)
        {
            
        }

        HeadViewModel headViewModel;
        public HeadViewModel HeadViewModel
        {
            get
            {
                if (this.headViewModel == null)
                {
                    this.headViewModel = new HeadViewModel();
                }

                return this.headViewModel;
            }
        }

        IStream infraredData;
        public IStream InfraredData
        {
            get
            {
                return this.infraredData;
            }

            set
            {
                this.infraredData = value;
                RaisePropertyChanged("InfraredData");
            }
        }

        IStream colourData;
        public IStream ColourData
        {
            get
            {
                return this.ColourData;
            }

            set
            {
                this.colourData = value;
                RaisePropertyChanged("ColourData");
            }
        }

        public string Greeting
        {
            get
            {
                return "Kinect v2 and XAML!";
            }
        }
    }
}
