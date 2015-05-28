using KinectMvvm.Framework;
using KinectMvvm.Head;
using KinectMvvm.Sensor;
using System.Collections.Generic;
using System;
using Windows.UI.Xaml.Navigation;
using KinectMvvm.Dispose;

namespace KinectMvvm.Main
{
    public class MainViewModel : ViewModel
    {
        ISensor sensor;
        INavigator navigator;

        public MainViewModel(ISensor sensor, INavigator navigator)
        {
            this.sensor = sensor;
            this.navigator = navigator;

            this.sensor.OnColourFrame.Subscribe(UpdateColourOutput);
            this.sensor.OnHeadFrame.Subscribe(UpdateHeadOutput);
            this.sensor.OnInfraredFrame.Subscribe(UpdateInfraredOutput);
        }

        void UpdateInfraredOutput(byte[] data)
        {
            InfraredData = data;
        }

        void UpdateHeadOutput(List<HeadModel> heads)
        {
            if (heads.Count > 0)
            {
                HeadViewModel.Update(heads[0]);
            }
        }

        void UpdateColourOutput(byte[] data)
        {
            ColourData = data;
        }

        public override void OnNavigatedFrom()
        {
            
        }

        ActionCommand navigate;
        public ActionCommand Navigate
        {
            get
            {
                if (this.navigate == null)
                {
                    this.navigate = new ActionCommand(() =>
                    {
                        this.navigator.Navigate(typeof (DisposeViewModel));
                    });
                }

                return this.navigate;
            }
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }

        public int ColourFrameWidth
        {
            get
            {
                return 1920;
            }
        }

        public int ColourFrameHeight
        {
            get
            {
                return 1080;
            }
        }

        public int ByteFrameWidth
        {
            get
            {
                return 512;
            }
        }

        public int ByteFrameHeight
        {
            get
            {
                return 424;
            }
        }

        public string Greeting
        {
            get
            {
                return "Kinect v2 and XAML!";
            }
        }

        public string DisposeText
        {
            get
            {
                return "Dispose Me!";
            }
        }
    }
}
