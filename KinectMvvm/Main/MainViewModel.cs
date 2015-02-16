using KinectMvvm.Framework;
using KinectMvvm.Head;
using KinectMvvm.Sensor;
using System.Collections.Generic;
using System;

namespace KinectMvvm.Main
{
    public class MainViewModel : BaseViewModel
    {
        ISensor sensor;

        public MainViewModel(ISensor sensor)
        {
            this.sensor = sensor;
            this.sensor.OnColourFrame.Subscribe(UpdateColourOutput);
            this.sensor.OnHeadFrame.Subscribe(UpdateHeadOutput);
            this.sensor.OnInfraredFrame.Subscribe(UpdateInfraredOutput);
        }

        private void UpdateInfraredOutput(byte[] data)
        {
            InfraredData = data;
        }

        private void UpdateHeadOutput(List<HeadModel> heads)
        {
            if (heads.Count > 0)
            {
                HeadViewModel.Update(heads[0]);
            }
        }

        private void UpdateColourOutput(byte[] data)
        {
            ColourData = data;
        }

        private HeadViewModel headViewModel;
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
