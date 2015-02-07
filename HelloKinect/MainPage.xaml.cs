using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using WindowsPreview.Kinect;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HelloKinect
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        KinectSensor sensor;
        ColorFrameReader colourReader;
        InfraredFrameReader irReader;
        ushort[] irData;
        byte[] irDataConverted;
        WriteableBitmap colourBitmap, irBitmap;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPageLoaded;
        }

        void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            sensor = KinectSensor.GetDefault();
            colourReader = sensor.ColorFrameSource.OpenReader();
            FrameDescription fd = sensor.ColorFrameSource.FrameDescription;
            colourBitmap = new WriteableBitmap(fd.Width, fd.Height);
            colourOutput.Source = colourBitmap;

            colourReader.FrameArrived += ColourFrameArrived;

            irReader = sensor.InfraredFrameSource.OpenReader();
            FrameDescription irFd = sensor.InfraredFrameSource.FrameDescription;
            irData = new ushort[irFd.LengthInPixels];
            irDataConverted = new byte[irFd.LengthInPixels * 4];
            irBitmap = new WriteableBitmap(irFd.Width, irFd.Height);
            irOutput.Source = irBitmap;

            irReader.FrameArrived += IrFrameArrived;

            sensor.Open();
        }

        private void IrFrameArrived(InfraredFrameReader sender, InfraredFrameArrivedEventArgs args)
        {
            using (InfraredFrame irFrame = args.FrameReference.AcquireFrame())
            {
                if (irFrame != null)
                {
                    irFrame.CopyFrameDataToArray(irData);

                    for (int i = 0; i < irData.Length; i++)
                    {
                        byte intensity = (byte)(irData[i] >> 8);

                        irDataConverted[i * 4] = intensity;
                        irDataConverted[i * 4 + 1] = intensity;
                        irDataConverted[i * 4 + 2] = intensity;
                        irDataConverted[i * 4 + 3] = 255;
                    }

                    irDataConverted.CopyTo(irBitmap.PixelBuffer);
                    irBitmap.Invalidate();
                }
            }
        }

        void ColourFrameArrived(ColorFrameReader sender, ColorFrameArrivedEventArgs args)
        {
            using (ColorFrame colourFrame = args.FrameReference.AcquireFrame())
            {
                if (colourFrame != null)
                {
                    colourFrame.CopyConvertedFrameDataToBuffer(colourBitmap.PixelBuffer, ColorImageFormat.Bgra);
                    colourBitmap.Invalidate();
                }
            }
        }
    }
}
