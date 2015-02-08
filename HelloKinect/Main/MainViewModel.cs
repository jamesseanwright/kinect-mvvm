using HelloKinect.Framework;
using HelloKinect.Head;
using HelloKinect.Sensor;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace HelloKinect.Main
{
    public class MainViewModel : BaseViewModel
    {
        ISensor sensorService;

        public MainViewModel(ISensor sensorService)
        {
            this.sensorService = sensorService;
            this.sensorService.NewColourFrame += UpdateColourOutput;
            this.sensorService.NewHeadFrame += UpdateHeadOutput;
            this.sensorService.NewInfraredFrame += UpdateInfraredOutput;
        }

        private void UpdateInfraredOutput(object sender, IFrame e)
        {
            InfraredData = (byte[]) e.Data;
        }

        private void UpdateHeadOutput(object sender, IFrame e)
        {
            //throw new NotImplementedException();
        }

        private void UpdateColourOutput(object sender, IFrame e)
        {
            ColourData = (byte[]) e.Data;
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

        byte[] infraredData;
        public byte[] InfraredData
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

        byte[] colourData;
        public byte[] ColourData
        {
            get
            {
                return this.colourData;
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
